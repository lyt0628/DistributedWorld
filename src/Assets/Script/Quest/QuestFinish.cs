



using QS.GameLib.Pattern.Message;

namespace QS.Quest
{
    /// <summary>
    /// �Á�ָʾ�΄���ɵ�ֵ���󣨿Ռ���ģʽ��
    /// </summary>
    public sealed class QuestFinish : IQuestStep
    {
        public bool IsAchieved => false;

        public IQuestStep Next => throw new System.InvalidOperationException();


        public void OnBegin(){}
    }
}