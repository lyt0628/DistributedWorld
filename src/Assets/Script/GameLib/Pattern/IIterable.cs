namespace QS.GameLib.Pattern
{
    interface IIterable<T>
    {
        bool HasNext();
        T Next();
    }
}