

namespace QS.Common
{
    public interface IHandlerGroup : IHandler
    {
        void Add(IHandler skill);
        void Remove(IHandler skill);
        bool Contains(IHandler handler);
    }
}