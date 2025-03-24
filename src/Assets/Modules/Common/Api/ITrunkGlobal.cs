

using GameLib.DI;

namespace QS.Api.Common
{
    /// <summary>
    /// 唯一一個可以接觸到 Unity 世界的Global，所有模塊的功能出口
    /// 我不喜欢大而全的框架，每个小小的组件和在一起才能起最大效应
    /// </summary>
    public interface ITrunkGlobal
    {

        IDIContext Context { get; }
    }
}