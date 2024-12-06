


using UnityEngine;

namespace QS.Api.Control.Domain
{
    /// <summary>
    /// Value Object
    /// </summary>
    public interface IControlledPointData
    {
        Vector3 Position { get; set; }
        float Horizontal { get; set; }
        float Vertical { get; set; }
        bool Jump { get; set; }
        Vector3 BaseRight { get; set; }
        Vector3 Baseforword { get; set; }
        Vector3 BaseUp { get; set; }


    }
}
