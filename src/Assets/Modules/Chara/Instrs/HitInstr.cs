


using QS.Api.Executor.Domain;
using UnityEngine;

namespace QS.Chara
{
    /// <summary>
    /// 用于展示受击效果的指令
    /// </summary>
    public interface IHitInstr : IInstruction
    {
        /// <summary>
        /// 攻击方向，也就是攻击者相对于被攻击者的方位
        /// </summary>
        Vector3 AttackDir { get; set; }
        /// <summary>
        /// 攻击的力度
        /// </summary>
        float AttackForce { get; set; }
        float HitStopTime { get; set; }
    }

    struct HitInstr : IHitInstr
    {
        public Vector3 AttackDir { get; set; }
        public float AttackForce { get; set; }
        public float HitStopTime { get; set; }
    }
}