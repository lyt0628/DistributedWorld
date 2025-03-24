
using GameLib.DI;
using QS.Api.Common;
using QS.Common;



namespace QS.Skill
{
    public class SkillGlobal : ModuleGlobal<SkillGlobal>
    {
        public SkillGlobal()
        {
            LoadOp = new UnitAsyncOp<IModuleGlobal>(this);
        }
        protected override AsyncOpBase<IModuleGlobal> LoadOp { get; }


        protected override void DoSetupBinding(IDIContext globalContext, IDIContext moduleContext)
        {
            globalContext
                .Bind<SkillTable>()
                .Bind<SkillStageFactory>()
                .Bind<SimpleSkillBuilder>(ScopeFlag.Prototype)
                .Bind<HintPhasedSkillBuilder>(ScopeFlag.Prototype);

            moduleContext
                .Bind<SkillStageFactory>();

        }


        //protected override async UniTask DoInit()
        //{

        //    // 完成了系統內部的聯合，在加載外部資源
        //    //RegisterTomlType();

        //    //await DepsGlobal.Unit.Task;

        //    /// 先不考虑加载时序问题
        //    //var h1 = Addressables.LoadAssetsAsync<TextAsset>("Conf_SimpleSkill", null);
        //    //h1.Completed += LoadSimpleSkills;

        //    //var h2 = Addressables.LoadAssetAsync<TextAsset>("LP_Test");
        //    //h2.Completed += LoadTest;
        //    //await h1.ToUniTask();
        //    //var tasks = new List<UniTask>
        //    //{
        //    //    h1.ToUniTask(),
        //    //    //h2.ToUniTask()
        //    //};
        //    // 用UniTask 可以优雅地保持时序
        //    //return UniTask.WhenAll(tasks);
        //}


        //void LoadTest(AsyncOpHandle<TextAsset> handle)
        //{
        //    var trunk = GetInstance<ITrunkGlobal>();
        //    trunk.Completed.AddListener(() =>
        //    {
        //        lua.DoString(handle.Result.text);
        //    });

        //}
        //void LoadSimpleSkills(AsyncOpHandle<IList<TextAsset>> handle)
        //{
        //    Assert.AreEqual(AsyncOperationStatus.Succeeded, handle.Status,
        //        "Failed to load simple skills configuration!!!");

        //    foreach (var conf in handle.Result)
        //    {
        //        var skillDoc = tomlParser.Parse(conf.text);
        //        var skills = skillDoc.GetArray("Skills");
        //        foreach (var skill in skills)
        //        {
        //            var sk = TomletMain.To<ISkill>(skill);
        //            skillRepo.Add(sk);
        //        }
        //    }


        //}



        //void RegisterTomlType()
        //{
        //    TomletMain.RegisterMapper<ISkillKey>(
        //        key =>
        //        {
        //            var table = new TomlTable();
        //            table.PutValue("No", new TomlString(key.No));
        //            table.PutValue("Name", new TomlString(key.Name));
        //            if (!string.IsNullOrEmpty(key.Patch))
        //            {
        //                table.PutValue("Patch", new TomlString(key.Patch));
        //            }
        //            return table;
        //        },
        //        tomlValue =>
        //        {
        //            if (tomlValue is not TomlTable table)
        //            {
        //                throw new TomlTypeMismatchException(typeof(TomlTable), tomlValue.GetType(), typeof(ISkillKey));
        //            }
        //            var no = table.GetString("No");
        //            var name = table.GetString("Name");
        //            string patch = null;
        //            if (table.TryGetValue("Patch", out var patchValue))
        //            {
        //                patch = patchValue.StringValue;
        //            }
        //            return ISkillKey.New(no, name, patch);
        //        }
        //        );
        //    TomletMain.RegisterMapper<SkillStage>(
        //        stage => new TomlString(stage.ToString()),
        //        tomlValue =>
        //        {
        //            if (tomlValue is not TomlString stageValue)
        //            {
        //                throw new TomlTypeMismatchException(typeof(TomlString), tomlValue.GetType(), typeof(SkillStage));
        //            }

        //            return tomlValue.StringValue switch
        //            {
        //                "Precast" => SkillStage.Precast,
        //                "Casting" => SkillStage.Casting,
        //                "Postcast" => SkillStage.Postcast,
        //                _ => throw new System.Exception()
        //            };
        //        }
        //        );
        //    TomletMain.RegisterMapper<DefaultSimpleSkill>(
        //        sk =>
        //        {
        //            // 技能是配置的，不能序列化 
        //            throw new System.InvalidOperationException("Configuration should be load instead of write!!!");

        //        },
        //        tomlValue =>
        //        {
        //            if (tomlValue is not TomlTable table)
        //            {
        //                throw new TomlTypeMismatchException(typeof(TomlTable), tomlValue.GetType(), typeof(DDSimpleSkill.DefaultSimpleSkill));
        //            }
        //            float postcastTime = 0f;
        //            try
        //            {
        //               postcastTime = table.GetFloat("postcastTime");
        //            }
        //            catch { }

        //            var key = TomletMain.To<ISkillKey>(table.GetValue("Key"));
        //            var handlers = table.GetArray("Handlers").Select(v => v.StringValue).ToArray();

        //            var sk = new DefaultSimpleSkill(key, handlers, postcastTime);
        //            // 每個在C#實現的處理器，自己定義自己的配置方式
        //            foreach (var h in handlers)
        //            {
        //                var handler = simpleSkillSubHandlerRegistry.GetSubHandler(h);
        //                handler.OnParseConfiguration(sk, table);
        //            }

        //            return sk;
        //        });
        //    TomletMain.RegisterMapper<GetPhaseState>(
        //        key => throw new System.Exception(),
        //        tomlValue =>
        //        {
        //            if (tomlValue is not TomlTable table)
        //            {
        //                throw new TomlTypeMismatchException(typeof(TomlTable), tomlValue.GetType(), typeof(GetPhaseState));
        //            }
        //            var key = TomletMain.To<ISkillKey>(table.GetValue("Key"));
        //            var skill = new GetPhaseState(key);
        //            foreach (TomlTable phase in table.GetArray("Phases").Cast<TomlTable>())
        //            {
        //                float comboOffset = float.PositiveInfinity;
        //                try
        //                {
        //                    comboOffset = phase.GetFloat("ComboOffset");
        //                }
        //                catch { }

        //                var sk = TomletMain.To<DefaultSimpleSkill>(phase.GetSubTable("Skill"));
        //                skill.AddPhase(new SkillPhase(sk, comboOffset));
        //            }
        //            return skill;
        //        });
        //    TomletMain.RegisterMapper<ISkill>(
        //        key => throw new System.Exception(),
        //        tomlValue =>
        //        {
        //            if (tomlValue is not TomlTable table)
        //            {
        //                throw new TomlTypeMismatchException(typeof(TomlTable), tomlValue.GetType(), typeof(ISkill));
        //            }
        //            if (table.TryGetValue("Type", out var type))
        //            {
        //                return type.StringValue switch
        //                {
        //                    "Default" => TomletMain.To<DefaultSimpleSkill>(tomlValue),
        //                    "Phased" => TomletMain.To<GetPhaseState>(tomlValue),
        //                    _ => throw new System.Exception()
        //                };
        //            }
        //            else
        //            {
        //                return TomletMain.To<DefaultSimpleSkill>(tomlValue);
        //            }
        //        });
        //}

    }
}
