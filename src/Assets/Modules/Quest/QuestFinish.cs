using QS.GameLib.Pattern;

namespace QS.Quest
{
    /// <summary>
    /// �Á�ָʾ�΄���ɵ�ֵ���󣨿Ռ���ģʽ��
    /// </summary>
    public sealed class QuestFinish : Sington<QuestFinish>, IQuestStep
    {
        public bool IsCompleted => false;

        public IQuestStep Next => throw new System.InvalidOperationException();

        public string UUID => "QSTaskFinish";

        public void OnBegin() { }
    }
}