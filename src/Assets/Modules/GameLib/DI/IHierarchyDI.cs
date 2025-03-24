

namespace GameLib.DI
{
    public interface IHierarchyDI
    {
        IDIContext GetParent();
        IDIContext SetParent(IDIContext dIContext);

    }


}
