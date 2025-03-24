

using QS.Common;
using QS.GameLib.Pattern.Message;

namespace QS.Chara
{
    /// <summary>
    /// 单位死亡时，向全局发送的死亡消息
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