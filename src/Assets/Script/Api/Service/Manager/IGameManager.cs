

using GameLib;

namespace QS.API
{
    interface IGameManager : IMessagerProvider
    {
        ManagerStatus Status { get; }

        void Startup();
        void Update();
    }

}