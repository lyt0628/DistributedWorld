

namespace GameLib
{
    interface IIterable<T>
    {
        bool HasNext();
        T Next();
    }
}