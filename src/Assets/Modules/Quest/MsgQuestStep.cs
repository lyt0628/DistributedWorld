

using QS.GameLib.Pattern.Message;

namespace QS.Quest
{
    /// <summary>
    /// ����һ�N��Ϣ�������ܵ�ָ���Δ����@����Ϣ�����J���@���Ӳ��E��ɡ�
    /// ����¼�������ͨ�������ɣ����ڿ���3��
    /// ����
    /// �ɼ�
    /// ���ܹ���
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