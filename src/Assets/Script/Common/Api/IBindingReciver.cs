

using GameLib.DI;

namespace QS.Common
{
    /// <summary>
    /// 接受整個上下文太太大了，接受單個實例還是可以接受的
    /// </summary>
    public interface IBindingReciver
    {
        void ReviceBinding(object instance);

    }
}