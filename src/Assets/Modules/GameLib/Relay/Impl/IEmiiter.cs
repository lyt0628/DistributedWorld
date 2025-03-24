

namespace QS.GameLib.Rx.Relay
{
    public interface IEmitter<T>
    {
        public void Emit(T obj);
    }

}