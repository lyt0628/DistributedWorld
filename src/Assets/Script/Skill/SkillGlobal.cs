using Cysharp.Threading.Tasks;
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
using QS.GameLib.Rx.Relay;
using QS.Skill.Conf;
using QS.Skill.Domain.Instruction;
using QS.Skill.Handler;
using QS.Skill.Service;
using QS.Skill.SimpleSkill;
using QS.Skill.Skills;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        [Injected(Name = QS.Api.Common.DINames.Lua_GameLogic)]
        readonly Lua lua;

        public SkillGlobal()
        {
            ExecutorGlobal.Instance.ProvideBinding(DI);
            CharaGlobal.Instance.ProvideBinding(DI);

            DI
                .BindExternalInstance(DepsGlobal.Instance.GetInstance<TomlParser>())
                // Lua 环境应该由上层模块注入
                // 游戏逻辑使用独立的一个Lua环境
                // 就现在而言，有 技能脚本 和 道具脚本 两个使用场景
                // 但只用一个环境， 技能和道具只会定义自己的东西
                // 需要全局环境吗，比如说玩家的信息？这种就没办法在
                // 各自的模块中实现的，虽然各个模块都能加载Lua脚本，但是
                // Lua 脚本本身因该认为是 Trunk 层的，因为作为C#使用的话，也就应该定义在Trunk层
                //.BindExternalInstance(DepsGlobal.Instance.GetInstance<Lua>(QS.Api.Deps.DINames.Lua_Skill))
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
                .Bind<ShuffleStep>();
        }

   
        protected override IDIContext DIContext => DI;

        public override void ProvideBinding(IDIContext context)
        {
            context
                .BindExternalInstance(DI.GetInstance<ISkillRepo>())
                .BindExternalInstance(DI.GetInstance<ISkillAblityFactory>())
                .BindExternalInstance(DI.GetInstance<ISkillInstrFactory>());
        }


        public override async void Initialize()
        {
            DI.Inject(this);

            // 完成了系統內部的聯合，在加載外部資源
            RegisterTomlType();

            /// 先不考虑加载时序问题
            var h1 = Addressables.LoadAssetsAsync<TextAsset>("Conf_SimpleSkill", null);
            h1.Completed += LoadSimpleSkills;

            var h2 = Addressables.LoadAssetAsync<TextAsset>("LP_Test");
            h2.Completed += LoadTest;

            var tasks = new List<UniTask>
            {
                h1.ToUniTask(),
                h2.ToUniTask()
            };
            // 用UniTask 可以优雅地保持时序
            await UniTask.WhenAll(tasks);
            base.Initialize();
        }
        void LoadTest(AsyncOperationHandle<TextAsset> handle)
        {
            var trunk = DI.GetInstance<ITrunkGlobal>();
            trunk.OnReady.AddListener(() =>
            {
                lua.DoString(handle.Result.text);
            });
            
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
