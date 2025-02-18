using QS.Api.Common;
using QS.GameLib.Pattern.Message;

namespace QS.GameLib.View
{
    /// <summary>
    /// �ڽӿ��OӋ�ĕr��߀�ǲ��c���w��Ui���F��ʽ�����ġ�
    /// ֻ�Ƕ��x��һ��UI�ӴΣ�
    /// ���H�ϲ�࣬����uGUI ����GameObject�䣬
    /// UIToolKit���� xml�䡣
    /// ��UIչʾ�У���Ҫ˼���}�s�ļܘ�������ģ�Ͱ�VO����ȫOK��
    /// ��Ҫ���I����󣬑�����ҕ�D����
    /// </summary>
    public interface IView : IMessagerProvider, IResourceInitializer
    {
        bool IsVisible { get; }
        bool IsResident { get; }
      

        /*
         * UpdateIfNeed Per Frame
         */
        void Show();

        void Preload();
        void OnUpdate();
        void OnModelChanged();

        /*
         * From Front to background
         */
        void Hide();

        /*
         * Release Resources
         */
        void Shutdown();

        void OnActive();
        void OnDeactive();
    }

}
