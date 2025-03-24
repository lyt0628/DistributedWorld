namespace QS.Quest
{
    abstract class AbstractQuestStep : IQuestStep
    {
        public AbstractQuestStep(string uuid, IQuestStep nextStep)
        {
            Next = nextStep;
            UUID = uuid;
        }

        public AbstractQuestStep(string uuid)
        {
            Next = QuestFinish.Instance;
            UUID = uuid;
        }

        public IQuestStep Next { get; }

        public bool IsCompleted => achieved;

        public string UUID { get; }


        bool achieved = false;
        /// <summary>
        /// 任務完成時，子類調用這個方法，通知子步驟完成
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