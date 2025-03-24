using QS.Api.Executor.Domain;
using UnityEngine;


namespace QS.Chara
{
    /// <summary>
    /// ��Ȼ��ֵ���ͣ����Ǵ���ʮ��Ƶ���������ô����ޣ�ֱ���ṩ���ýӿڸ��ʺϸ���
    /// </summary>
    public interface IMoveInstr : IInstruction
    {
        public Vector2 value { get; set; }

        public bool run { get; set; }
    }
    /// <summary>
    /// ��Щָ����ͨ�õ�ָ�Ҳ����˵�������ж�����Դ������ǵĴ�����
    /// �� struct ����ֱ�Ӵ��ݸ���������ȫ
    /// </summary>
    public struct MoveInstr : IMoveInstr
    {

        public Vector2 value { get; set; }

        public bool run { get; set; }
    }
}