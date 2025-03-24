

using QS.GameLib.Pattern.Message;

namespace QS.Quest
{
    /// <summary>
    /// ����һ�N��Ϣ�������ܵ�ָ���Δ����@����Ϣ�����J���@���Ӳ��E��ɡ�
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