



using QS.GameLib.View;

namespace QS.UI
{

    /// <summary>
    ///  ���� HUD �ֳ�����ջ����һ��������Ļ�����ͼ
    ///  һ�����ͼ�ڲ��Ĵ��ڣ��Ӵ�����ʾʱ��Hide������
    /// </summary>
    public interface IUIStack
    {
        void Push(IView view);
        void Pop();
    }
}