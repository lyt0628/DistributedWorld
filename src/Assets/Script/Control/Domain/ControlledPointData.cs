


using QS.Api.Control.Domain;
using UnityEngine;

namespace QS.Control.Domain
{
    class ControlledPointData : IControlledPointData
    {
      

        public Vector3 Position { get; set; }
        public float Horizontal { get; set; }
        public float Vertical { get; set; }
        public bool Jump { get; set; }
        public Vector3 BaseRight { get; set; }
        public Vector3 Baseforword { get; set; }
        public Vector3 BaseUp { get; set; }
    }
}