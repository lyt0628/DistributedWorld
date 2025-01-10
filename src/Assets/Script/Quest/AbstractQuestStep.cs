


using QS.GameLib.Pattern.Message;

namespace QS.Quest
{
    abstract class AbstractQuestStep : IQuestStep
    {
        public AbstractQuestStep(IQuestStep nextStep)
        {
            Next = nextStep;
        }

        public IQuestStep Next { get; }

        public bool IsAchieved => achieved;


        bool achieved = false;
        /// <summary>
        /// �΄���ɕr������{���@��������֪ͨ�Ӳ��E���
        /// </summary>
        protected void Achieve()
        {
            achieved = true;
            OnAchieved();
            Next.OnBegin();
        }

        public abstract void OnBegin();
        protected abstract void OnAchieved();

    }
}