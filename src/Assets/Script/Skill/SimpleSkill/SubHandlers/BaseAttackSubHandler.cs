

using GameLib.DI;
using QS.Api.Chara;
using QS.Api.Chara.Service;
using QS.Api.Combat.Domain;
using QS.Api.Executor.Domain;
using QS.Api.Executor.Service;
using QS.Api.Skill.Domain;
using QS.Chara.Domain;
using QS.Skill.Domain.Handler;
using System.Collections.Generic;
using Tomlet;
using Tomlet.Models;
using QS.Api.Common.Util.Detector;
using UnityEngine;
using System.Linq;
using QS.Api;
using QS.Api.Executor.Domain.Instruction;


namespace QS.Skill.SimpleSkill
{
    /// <summary>
    /// 這一層級的攻擊類決定，攻擊的方式，攻擊的邏輯，
    /// 當然還有提供一些拓展的接口，生命週期之類的
    /// </summary>
    abstract class BaseAttackSubHandler : SimpleSkillSubHandlerAdapter
    {
        public const string baseAttackReourceKey = nameof(BaseAttackSubHandler);
        [Injected]
        readonly IInstructionFactory instrFactory;
        [Injected]
        readonly ILifecycleProivder life;
        IInstruction instr;

        protected abstract IDetector HitDetector { get; }

        public override void OnParseConfiguration(ISimpleSkill skill, TomlTable skillTable)
        {
            var attackTable = skillTable.GetSubTable("Attack");
            skill.ResourceMap[baseAttackReourceKey] = TomletMain.To<IAttack>(attackTable);

            var atk = skill.ResourceMap[baseAttackReourceKey] as IAttack;
            instr = instrFactory.Injured(atk);
        }

        public override void OnPrecast(Character chara, ISimpleSkillAbility handler)
        {
            chara.Frozen = true;
        }

        public override void OnCasting(Character chara, ISimpleSkillAbility handler)
        {
            life.UpdateAction.AddListener(DoAttack);
        }
        public override void OnPostcast(Character chara, ISimpleSkillAbility handler)
        {
            life.UpdateAction.RemoveListener(DoAttack);
        }
        public override void OnShutdown(Character chara, ISimpleSkillAbility handler)
        {
            chara.Frozen = false;
        }
        public override void OnCancel(Character chara, ISimpleSkillAbility handler)
        {
            chara.Frozen = false;
        }
        private void DoAttack()
        {
            var targets = HitDetector.Detect();
            Debug.Log($"Attacked Count >>> {targets.Count()}");
            foreach (var target in targets)
            {
                //Debug.Log(target.name);
                if (target.TryGetComponent<IExecutor>(out var executor))
                {
                    executor.Execute(instr);
                }
            };
        }
    }
}