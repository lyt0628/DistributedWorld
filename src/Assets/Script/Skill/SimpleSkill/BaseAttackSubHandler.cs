

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
using static UnityEngine.GraphicsBuffer;
using QS.Api.Common.Util.Detector;
using UnityEngine;
using System.Linq;
using QS.Api;


namespace QS.Skill.SimpleSkill
{

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


        public override void OnCastingEnter(Character chara, ISimpleSkillHandler handler)
        {
            life.Request(Lifecycles.Update, DoAttack);   
        }

        public override void OnCastingExit(Character chara, ISimpleSkillHandler handler)
        {
            life.Cancel(Lifecycles.Update, DoAttack);
        }



        private void DoAttack()
        {
            var targets = HitDetector.Detect();
            if (targets.Any()) {
                Debug.Log("Attack!!!");
            }
            foreach (var target in targets)
            {
                Debug.Log(target.name);
                if (target.TryGetComponent<IExecutor>(out var executor))
                {
                    executor.Execute(instr);
                }
            };
        }
    }
}