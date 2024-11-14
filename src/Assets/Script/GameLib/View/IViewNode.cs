

namespace GameLib.View
{
    public interface IViewNode : IView
    {
        void Add(IViewNode view);   

    }
}