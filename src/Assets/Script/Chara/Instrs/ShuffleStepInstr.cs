



using QS.Api.Executor.Domain;
using UnityEngine;

namespace QS.Chara.Instrs
{
    /// <summary>
    /// �������Ǽ������н�ɫ�沽�ٶ�һ����
    /// </summary>
    sealed class ShuffleStepInstr : IShuffleStepInstr
    {
        public ShuffleStepInstr(Vector2 direction, Vector3 baseforword, Vector3 baseRight) {
            this.Direction = direction;
            this.Baseforword = baseforword;
            this.BaseRight = baseRight;
        }
       public Vector2 Direction { get; }

        public Vector3 BaseRight { get; }

        public Vector3 Baseforword { get; }
    }
}