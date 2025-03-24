using GameLib.DI;



namespace QS.Common
{
    public interface IBindingProvider
    {
        void ProvideBinding(IDIContext context);

    }

}