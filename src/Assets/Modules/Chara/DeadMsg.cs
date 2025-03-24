

using QS.Common;
using QS.GameLib.Pattern.Message;

namespace QS.Chara
{
    /// <summary>
    /// ��λ����ʱ����ȫ�ַ��͵�������Ϣ
    /// </summary>
    public class DeadMsg : IMessage
    {
        public GameUnit Sender { get; }

        public DeadMsg(GameUnit sender)
        {
            Sender = sender;
        }
    }
}