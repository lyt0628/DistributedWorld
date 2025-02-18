

namespace QS.GameLib.Rx.Relay
{
    /// <summary>
    /// 同步的情况下, 有时也需要一些简单的背压, 0-1, 开启或者关闭这样子的操作
    /// 做不到一启用就恢复最新数据, ??可以的只要立即调用他的Resume就可以立即更新到最新值了
    /// </summary>
    public interface IDisposable
    {
     
        bool Disposed { get; set; }
        bool Paused { get; set; }
    }
}