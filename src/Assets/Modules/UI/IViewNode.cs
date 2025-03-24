namespace QS.GameLib.View
{
    public interface IViewNode : IView
    {
        void Add(IViewNode view);
        bool IsRoot { get; }


    }
}