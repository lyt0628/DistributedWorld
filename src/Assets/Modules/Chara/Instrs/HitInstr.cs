


using QS.Api.Executor.Domain;
using UnityEngine;

namespace QS.Chara
{
    /// <summary>
    /// ����չʾ�ܻ�Ч����ָ��
    /// </summary>
    public interface IHitInstr : IInstruction
    {
        /// <summary>
        /// ��������Ҳ���ǹ���������ڱ������ߵķ�λ
        /// </summary>
        Vector3 AttackDir { get; set; }
        /// <summary>
        /// ����������
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