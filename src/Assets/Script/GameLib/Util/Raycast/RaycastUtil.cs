using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


namespace GameLib.Uitl.RayCast
{
    static class RaycastUtil
    {

       public static float GetNearestColliderDistance(Vector3 position, Vector3 direction)
        {
            if (Physics.Raycast(position, direction, out RaycastHit hit))
            {
                return hit.distance;
            }
            return float.PositiveInfinity;
        }
    }
}