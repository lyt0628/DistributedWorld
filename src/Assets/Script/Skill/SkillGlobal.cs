using GameLib.DI;
using NLua;
using QS.Api;
using QS.Api.Combat.Domain;
using QS.Api.Common;
using QS.Api.Common.Util.Detector;
using QS.Api.Skill;
using QS.Api.Skill.Domain;
using QS.Chara;
using QS.Common;
using QS.Executor;
using QS.Skill.Conf;
using QS.Skill.Domain.Instruction;
using QS.Skill.Handler;
using QS.Skill.Service;
using QS.Skill.SimpleSkill;
using QS.Skill.Skills;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tomlet;
using Tomlet.Exceptions;
using Tomlet.Models;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UIElements;


namespace QS.Skill
{
    public class SkillGlobal : ModuleGlobal<SkillGlobal>
    {
        internal IDIContext DI = IDIContext.New();
        [Injected]
        readonly TomlParser tomlParser;
        [Injected]
        readonly DefaultSkillRepo skillRepo;
        [Injected]
        readonly ISubHandlerRegistry simpleSkillSubHandlerRegistry;
        public SkillGlobal()
        {
            ExecutorGlobal.Instance.ProvideBinding(DI);
            CharaGlobal.Instance.ProvideBinding(DI);

            DI
                .BindExternalInstance(DepsGlobal.Instance.GetInstance<TomlParser>())
                .BindExternalInstance(CommonGlobal.Instance.GetInstance<IDetectorFactory>())
                .BindExternalInstance(CommonGlobal.Instance.GetInstance<ILifecycleProivder>())
                .BindExternalInstance(new SimpleSkillAnimCfg())
                .BindExternalInstance(new SkillInstrFactory())
                .Bind<DefaultSubHandlerRegistry>()
                .Bind<SkillAblityFactory>()
                .Bind<MountSubHandler>()
                .Bind<CollideAttackSubHandler>()
                .Bind<ScriptableSubHandler>()
                .BindExternalInstance(new DefaultSkillRepo())
                .Bind<ShuffleStep>()
                .BindExternalInstance(DepsGlobal.Instance.GetInstance<Lua>(Api.Deps.DINames.Lua_Skill));
            DI.Inject(this);

            // 完成了系統內部的聯合，在加載外部資源
            RegisterTomlType();


        }

   
        protected override IDIContext DIContext => DI;

        public override void ProvideBinding(IDIContext context)
        {
            context
                .BindExternalInstance(DI.GetInstance<ISkillRepo>())
                .BindExternalInstance(DI.GetInstance<ISkillAblityFactory>())
                .BindExternalInstance(DI.GetInstance<ISkillInstrFactory>());

        }

        public override void Initialize()
        {
            var handle = Addressables.LoadAssetsAsync<TextAsset>("SimpleSkillConf", null);
            handle.Completed += LoadSimpleSkills;
        }
        
        void LoadSimpleSkills(AsyncOperationHandle<IList<TextAsset>> handle)
        {
            Assert.AreEqual(AsyncOperationStatus.Succeeded, handle.Status,
                "Failed to load simple skills configuration!!!");

            foreach (var conf in handle.Result)
            {
                var skillDoc = tomlParser.Parse(conf.text);
                var skills = skillDoc.GetArray("Skills");
                foreach (var skill in skills)
                {
                    var sk = TomletMain.To<ISkill>(skill);
                    skillRepo.AddSkill(sk);
                }
            }

            base.Initialize();
        }



        void RegisterTomlType()
        {
            TomletMain.RegisterMapper<ISkillKey>(
                key =>
                {
                    var table = new TomlTable();
                    table.PutValue("No", new TomlString(key.No));
                    table.PutValue("Name", new TomlString(key.Name));
                    if (!string.IsNullOrEmpty(key.Patch))
                    {
                        table.PutValue("Patch", new TomlString(key.Patch));
                    }
                    return table;
                },
                tomlValue =>
                {
                    if (tomlValue is not TomlTable table)
                    {
                        throw new TomlTypeMismatchException(typeof(TomlTable), tomlValue.GetType(), typeof(ISkillKey));
                    }
                    var no = table.GetString("No");
                    var name = table.GetString("Name");
                    string patch = null;
                    if (table.TryGetValue("Patch", out var patchValue))
                    {
                        patch = patchValue.StringValue;
                    }
                    return ISkillKey.New(no, name, patch);
                }
                );
            TomletMain.RegisterMapper<SimpleSkillStage>(
                stage => new TomlString(stage.ToString()),
                tomlValue =>
                {
                    if (tomlValue is not TomlString stageValue)
                    {
                        throw new TomlTypeMismatchException(typeof(TomlString), tomlValue.GetType(), typeof(SimpleSkillStage));
                    }

                    return tomlValue.StringValue switch
                    {
                        "Precast" => SimpleSkillStage.Precast,
                        "Casting" => SimpleSkillStage.Casting,
                        "Postcast" => SimpleSkillStage.Postcast,
                        _ => throw new System.Exception()
                    };
                }
                );
            TomletMain.RegisterMapper<DefaultSimpleSkill>(
                sk =>
                {
                    // 技能是配置的，不能序列化 
                    throw new System.InvalidOperationException("Configuration should be load instead of write!!!");
               
                },
                tomlValue =>
                {
                    if (tomlValue is not TomlTable table)
                    {
                        throw new TomlTypeMismatchException(typeof(TomlTable), tomlValue.GetType(), typeof(SimpleSkill.DefaultSimpleSkill));
                    }
                    var key = TomletMain.To<ISkillKey>(table.GetValue("Key"));
                    var handlers = table.GetArray("Handlers").Select(v => v.StringValue).ToArray();
                    var sk = new DefaultSimpleSkill(key, handlers);
                    // 每個在C#實現的處理器，自己定義自己的配置方式
                    foreach (var h in handlers)
                    {
                        var handler = simpleSkillSubHandlerRegistry.GetSubHandler(h);
                        handler.OnParseConfiguration(sk, table);
                    }

                    return sk;
                });
            TomletMain.RegisterMapper<PhasedSimpleSkill>(
                key =>throw new System.Exception(),
                tomlValue =>
                {
                    if (tomlValue is not TomlTable table)
                    {
                        throw new TomlTypeMismatchException(typeof(TomlTable), tomlValue.GetType(), typeof(PhasedSimpleSkill));
                    }
                    var key = TomletMain.To<ISkillKey>(table.GetValue("Key"));
                    var skill = new PhasedSimpleSkill(key);
                    foreach (var phase in table.GetArray("Phases"))
                    {
                        skill.AddPhase(TomletMain.To<DefaultSimpleSkill>(phase));
                    }
                    return skill;
                });
            TomletMain.RegisterMapper<ISkill>(
                key => throw new System.Exception(),
                tomlValue =>
                {
                    if (tomlValue is not TomlTable table)
                    {
                        throw new TomlTypeMismatchException(typeof(TomlTable), tomlValue.GetType(), typeof(ISkill));
                    }
                    if(table.TryGetValue("Type", out var type))
                    {
                        return type.StringValue switch
                        {
                            "Default" => TomletMain.To<DefaultSimpleSkill>(tomlValue),
                            "Phased" => TomletMain.To<PhasedSimpleSkill>(tomlValue),
                            _ => throw new System.Exception()
                        };
                    }
                    else
                    {
                        return TomletMain.To<DefaultSimpleSkill>(tomlValue);
                    } 
                });
        }

    }
}
