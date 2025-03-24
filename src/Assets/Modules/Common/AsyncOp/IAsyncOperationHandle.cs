using System;
using System.Threading.Tasks;

namespace QS.Api.Common
{
    /// <summary>
    /// 加载资源？具体而言是
    ///只有懒加载没有部分加载
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAsyncOpHandle<T> : IAsyncOpHandle
    {
        new Task<T> Task { get; }
        T Result { get; }

        new event Action<IAsyncOpHandle<T>> OnCompleted;
    }
    public interface IAsyncOpHandle
    {
        bool IsDone { get; }
        Task Task { get; }

        event Action OnCompleted;
    }

}