using QS.Api.Common;
using QS.GameLib.Pattern.Message;

namespace QS.GameLib.View
{
    /// <summary>
    /// 在接口設計的時候還是不與具體的Ui實現方式綁定的。
    /// 只是定義的一個UI層次，
    /// 實際上差不多，在是uGUI 中是GameObject樹，
    /// UIToolKit中是 xml樹。
    /// 在UI展示中，不要思考複雜的架構，數據模型按VO就完全OK了
    /// 不要對領域抽象，應當對視圖抽象
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
