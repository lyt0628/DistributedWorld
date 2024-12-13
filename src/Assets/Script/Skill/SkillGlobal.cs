using GameLib.DI;
using NLua;
using QS.Api.Common;
using QS.Api.Skill;
using QS.Api.Skill.Predefine;
using QS.Api.Skill.Service;
using QS.Skill.Conf;
using QS.Skill.Domain.Instruction;
using QS.Skill.Service;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace QS.Skill
{
    public class SkillGlobal : ModuleGlobal<SkillGlobal>
    {
        internal IDIContext DI = IDIContext.New();
        public SkillGlobal()
        {

            DI
                .BindExternalInstance(new SimpleSkillAnimCfg())
                .BindExternalInstance(new SkillInstrFactory())
                .BindExternalInstance(new SkillAblityFactory())
                .BindExternalInstance(DepsGlobal.Instance.GetInstance<Lua>(Api.Deps.DINames.Lua_Skill));
            var m = DI.GetInstance<ISimpleSkillAnimCfg>();
        }

        protected override IDIContext DIContext => DI;

        public override void ProvideBinding(IDIContext context)
        {
            context
                .BindExternalInstance(DI.GetInstance<ISkillAblityFactory>())
                .BindExternalInstance(DI.GetInstance<ISkillInstrFactory>());

        }
    }
}
