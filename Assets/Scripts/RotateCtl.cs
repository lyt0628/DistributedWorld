using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCtl : IController
{

    public void Control(IControllable controlable)
    {
        if(controlable.CVelocity.magnitude == 0f) { return; }

        var camera = controlable.CCamera.transform;
        var moveVec = controlable.CVelocity;
        moveVec.y = 0f;
        moveVec = moveVec.normalized;
        

        Quaternion tmp = camera.rotation;
        camera.eulerAngles = new Vector3(0, camera.eulerAngles.y, 0);
        moveVec = camera.TransformDirection(moveVec);
        camera.rotation = tmp;

        controlable.CGameObject.transform.rotation = Quaternion.LookRotation(moveVec);
 
    }
}
