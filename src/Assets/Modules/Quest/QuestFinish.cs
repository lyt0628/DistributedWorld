using QS.GameLib.Pattern;

namespace QS.Quest
{
    /// <summary>
    /// 用來指示任務完成的值對象（空對象模式）
    /// </summary>
    public sealed class QuestFinish : Sington<QuestFinish>, IQuestStep
    {
        public bool IsCompleted => false;

        public IQuestStep Next => throw new System.InvalidOperationException();

        public string UUID => "QSTaskFinish";

        public void OnBegin() { }
    }
}