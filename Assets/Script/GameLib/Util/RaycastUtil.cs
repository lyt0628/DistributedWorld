using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


namespace GameLib
{
    static class RaycastUtil
    {
       public static bool IsNearestColliderFartherThan(Vector3 position, Vector3 direction, float distance)
        {
            if (Physics.Raycast(position, direction, out RaycastHit hit))
            {
                Debug.Log("RaycastUtil ::: NearsetCollider distance is : " + hit.distance);
                if (hit.distance > distance)
                {
                    return true;
                }
                else { return false; }
            }

            return true; // No Collider Found 
        }

        public static bool IsNearestColliderCloserThan(Vector3 position, Vector3 direction, float distance)
        {
            return ! IsNearestColliderCloserThan(position, direction, distance);        
        }

        public static bool IsNearestColliderCloserThan(Vector3 position, Vector3 direction, float distance, out RaycastHit casthit)
        {
            
            if (Physics.Raycast(position, direction, out RaycastHit hit))
            {
                if (hit.distance < distance)
                {
                    casthit = hit;
                    return true;
                }
            }
            casthit = default;
            return false;
         }



       public static float GetNearestColliderDistance(Vector3 position, Vector3 direction)
        {
            if (Physics.Raycast(position, direction, out RaycastHit hit))
            {
                return hit.distance;
            }
            return 0f;
        }
    }
}