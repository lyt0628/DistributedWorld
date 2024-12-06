using QS.Api.Control.Domain;
using QS.Control.Domain;
using QS.GameLib.Rx.Relay;

namespace QS.Api.Control.Service
{
    public interface IControlledPointDataSource
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">Relay to nenerate ControlledPointData</param>
        /// <returns>UUID  of controlled point</returns>
        string New(Relay<IControlledPointData> data);

        void Remove(string uuid);

        IControlledPointData Create();
    }
}
