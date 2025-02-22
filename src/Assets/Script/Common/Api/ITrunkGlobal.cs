

using UnityEngine.Events;

namespace QS.Api.Common
{
    /// <summary>
    /// 唯一一個可以接觸到 Unity 世界的Global，所有模塊的功能出口
    /// </summary>
    public interface ITrunkGlobal : IInstanceProvider
    {
        /// <summary>
        /// 当整个游戏都加载完毕，进入游戏欢迎界面
        /// </summary>
        UnityEvent OnReady { get; }
    }
}