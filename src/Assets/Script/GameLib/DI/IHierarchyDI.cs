

namespace GameLib.DI
{
    public interface IHierarchyDI
    {
        IDIContext GetParent();
        void SetParent(IDIContext dIContext);

    }


}
