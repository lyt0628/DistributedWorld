



using QS.GameLib.View;

namespace QS.UI
{

    public interface IUIStack
    {
        void Push(IView view);
        void Pop();
    }
}