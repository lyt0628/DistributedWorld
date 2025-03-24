



using QS.GameLib.View;

namespace QS.UI
{

    /// <summary>
    ///  现在 HUD 分成两层栈管理，一层管理顶级的互斥视图
    ///  一层管视图内部的窗口，子窗口显示时候，Hide父窗口
    /// </summary>
    public interface IUIStack
    {
        void Push(IView view);
        void Pop();
    }
}