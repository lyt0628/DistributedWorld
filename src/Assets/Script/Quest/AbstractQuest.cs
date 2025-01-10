


using QS.GameLib.Util;

namespace QS.Quest
{
    abstract class AbstractQuest : IQuest
    {

        protected abstract IQuestStep StepRoot { get; }  

        public bool IsAchieved => ReflectionUtil.IsChildOf<QuestFinish>(CurrentStep);

        public IQuestStep CurrentStep {
            get
            {
                var curStep = StepRoot;
                while(curStep.IsAchieved) curStep = curStep.Next;
                return curStep;
            }
        }
    }


}