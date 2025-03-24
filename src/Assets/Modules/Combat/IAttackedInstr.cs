

using QS.Api.Executor.Domain;
using System;

namespace QS.Combat
{
    public interface IAttackedInstr : IInstruction
    {

    }
    public readonly struct AttackedInstr : IAttackedInstr
    {
        public AttackedInstr(CombatorBehaviour attacker, ISkill skill)
        {
            Attacker = attacker != null ? attacker : throw new ArgumentNullException(nameof(attacker));
            Skill = skill ?? throw new ArgumentNullException(nameof(skill));
        }

        /// <summary>
        /// 攻击者的信息，这个攻击者应该持有 CombatorBehaviour 组件
        /// </summary>
        public CombatorBehaviour Attacker { get; }

        public ISkill Skill { get; }
    }
}