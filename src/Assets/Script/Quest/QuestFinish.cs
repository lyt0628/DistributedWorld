



using QS.GameLib.Pattern.Message;

namespace QS.Quest
{
    /// <summary>
    /// 用來指示任務完成的值對象（空對象模式）
    /// </summary>
    public sealed class QuestFinish : IQuestStep
    {
        public bool IsAchieved => false;

        public IQuestStep Next => throw new System.InvalidOperationException();


        public void OnBegin(){}
    }
}