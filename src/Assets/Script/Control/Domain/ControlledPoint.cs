

using QS.Api.Control.Domain;
using System.Numerics;

namespace QS.Control.Domain
{
    class ControlledPoint : IControlledPoint
    {
        public ControlledPoint(string uuid) 
        { 
            this.uuid = uuid;
        }

        readonly string uuid;
        public string UUID => uuid;

        public IControlledPointData PointData { get; set; }

        public float VerticalSpeed { get; set; } = 0f;
        public Quaternion Rotation { get; set; } = Quaternion.Identity;
    }
}