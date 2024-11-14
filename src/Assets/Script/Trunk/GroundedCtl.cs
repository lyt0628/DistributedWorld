


using GameLib;
using UnityEngine;

class GroundedCtl : IController
{
    public void Control(IControllable controlable)
    {
        var transform = controlable.CGameObject.transform;
        var collider = controlable.CGameObject.GetComponent<CapsuleCollider>();
        if (collider == null)
        {
            Debug.LogError("Controlable must be attached with CapsuleCollier");
            return;
        }

        controlable.IsGrounded = ! RaycastUtil.IsNearestColliderFartherThan(transform.position, Vector3.down , 
                                                    (collider.height + collider.radius) / 1.9f);

        //Debug.Log("GroundedCtl ::: Character is grounded??? :" + controlable.IsGrounded);
    }
}