

using QS.GameLib.Pattern.Message;

namespace QS.Quest
{
    class DDMsgQuestStep : MsgQuestStep
    {
        public DDMsgQuestStep(string uuid, IMessager messager, string msg, int count, IQuestStep nextStep) : base(uuid, messager, msg, count, nextStep)
        {
        }

        public override void OnBegin()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnAchieved()
        {
            throw new System.NotImplementedException();
        }
    }
}