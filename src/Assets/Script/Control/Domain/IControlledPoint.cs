


using QS.Api.Control.Domain;
using System.Numerics;

namespace QS.Control.Domain
{
    /// <summary>
    /// Entity
    /// </summary>
    interface IControlledPoint 
    {
        string UUID { get; }
        IControlledPointData PointData { get; }
        public float VerticalSpeed { get; set; }
        Quaternion Rotation { get; set; }
    }
}