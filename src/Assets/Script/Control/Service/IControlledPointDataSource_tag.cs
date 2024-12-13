

using QS.Api.Control.Service;
using QS.Control.Domain;
using QS.GameLib.Rx.Relay;

namespace QS.Control.Service
{
    interface IControlledPointDataSource_tag : IControlledPointDataSource
    {
        public Relay<IControlledPoint> Get(string uuid);
    }
}