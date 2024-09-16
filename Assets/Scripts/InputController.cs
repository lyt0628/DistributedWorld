using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : IController
{

    public float velocityFactor = 100f;
    public void Control(IControllable controlable)
    {
        var camera = controlable.CCamera.transform;
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        Vector3 baseRight = camera.right;
        
        Vector3 baseForward = camera.forward;
        baseForward = new Vector3(baseForward.x, 0, baseForward.z);
        baseForward = baseForward.normalized;

        Vector3 moveVec = hor * baseRight + ver * baseForward;
    
        controlable.CVelocity =  velocityFactor * moveVec ;
    }

}
