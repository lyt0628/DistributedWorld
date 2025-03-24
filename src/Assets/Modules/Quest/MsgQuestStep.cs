

using QS.GameLib.Pattern.Message;

namespace QS.Quest
{
    /// <summary>
    /// 接受一種消息，當接受到指定次數的這個消息，就認爲這個子步驟完成。
    /// 大多事件都可以通过这个完成，现在考虑3种
    /// 交互
    /// 采集
    /// 击败怪物
    /// </summary>
    abstract class MsgQuestStep : AbstractQuestStep
    {
        readonly string msg;
        readonly int count;
        int cnt = 0;
        readonly IMessager messager;
        public MsgQuestStep(string uuid, IMessager messager, string msg, int count, IQuestStep nextStep) : base(uuid, nextStep)
        {
            this.msg = msg;
            this.count = count;
            this.messager = messager;

            messager.AddListener(msg, OnMessage);
        }

        void OnMessage(IMessage _)
        {
            cnt++;
            if (cnt == count)
            {
                Achieve();
                messager.RemoveListener(msg, OnMessage);
            }
        }
    }
}