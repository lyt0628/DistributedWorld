using QS.GameLib.Pattern.Message;

namespace QS.Api
{
    /// <summary>
    /// 游戏管理器, 作为全局服务进行提供, 不保证整个生命周期内都可用
    /// 不是MonoBehaviour但是会给他分配生命周期
    /// 一般来说,也只有那些有生命周期的会主动来这边寻求服务
    /// </summary>
    public interface IGameManager : IMessagerProvider
    {
        ManagerStatus Status { get; }

        void Startup();
        void Update();
    }

}