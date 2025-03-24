using QS.Api.Executor.Domain;
using UnityEngine;

namespace QS.Chara.Instrs
{
    /// <summary>
    /// �������Ǽ������н�ɫ�沽�ٶ�һ����
    /// 
    /// ��ɫӦ��ʱ���ܵ����ƣ������Ǿ�ֹ Ҳ��Ҫ��һ���տ���
    /// </summary>
    public interface IShuffleStepInstr : IInstruction
    {

        Vector2 Direction { get; }
        Vector3 BaseRight { get; }
        Vector3 Baseforword { get; }

    }
}