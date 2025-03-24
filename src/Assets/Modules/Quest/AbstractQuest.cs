namespace QS.Quest
{
    abstract class AbstractQuest : IQuest
    {

        protected abstract IQuestStep StepRoot { get; }

        public bool IsAchieved => CurrentStep is QuestFinish;

        public IQuestStep CurrentStep
        {
            get
            {
                var curStep = StepRoot;
                while (curStep.IsCompleted) curStep = curStep.Next;
                return curStep;
            }
        }
    }


}