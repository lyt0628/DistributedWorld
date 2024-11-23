using GameLib.Pattern.Message;

namespace QS.Api
{
    public interface IGameManager : IMessagerProvider
    {
        ManagerStatus Status { get; }

        void Startup();
        void Update();
    }

}