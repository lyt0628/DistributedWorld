




using QS.Api.Control.Domain;
using QS.Api.Control.Exception;
using QS.Api.Service;
using QS.Control.Domain;
using QS.GameLib.Rx.Relay;
using QS.GameLib.Util;
using System;
using System.Collections.Generic;

namespace QS.Control.Service
{
    class ControlledPointDataSource : IControlledPointDataSource_tag
    {
        readonly Dictionary<string, ControlledPoint> points = new();
        readonly Dictionary<string, Relay<IControlledPoint>> relays = new();

        public IControlledPointData Create()
        {
            return new ControlledPointData();
        }

        public string New(Relay<IControlledPointData> data)
        {
            var uuid = MathUtil.UUID();
            var p = new ControlledPoint(uuid);
            points[uuid] = p;
            var r = data.Map<IControlledPoint>((d) =>
            {
                var point = points[uuid];
                point.PointData = d;
                return point;
            });
            relays[uuid] = r;

            return uuid;
        }

        public void Remove(string uuid)
        {
            points.Remove(uuid);
            relays.Remove(uuid);
        }

        public Relay<IControlledPoint> Get(string uuid) 
        {
            try
            {

            return relays[uuid];
            }
            catch (Exception)
            {
                throw new DataSourceNotFoundException(uuid, typeof(IControlledPointData).FullName);
            }
        }
    }
}