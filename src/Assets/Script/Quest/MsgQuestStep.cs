

using QS.GameLib.Pattern.Message;

namespace QS.Quest
{
    /// <summary>
    /// 接受一種消息，當接受到指定次數的這個消息，就認爲這個子步驟完成。
    /// </summary>
    abstract class MsgQuestStep : AbstractQuestStep
    {
        readonly string msg;
        readonly int count;
        int cnt = 0;
        readonly IMessager messager;
        public MsgQuestStep(IMessager messager, string msg, int count ,IQuestStep nextStep) : base(nextStep)
        {
            this.msg = msg;
            this.count = count;
            this.messager = messager;

            messager.AddListener(msg, OnMessage);
        }

        void OnMessage(IMessage _)
        {
            cnt++;
            if (cnt == count) {
                Achieve();
                messager.RemoveListener(msg, OnMessage);
            }
        }
    }
}