using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Responsibing for controling the rotation of user by given state
 */
public class RotateCtl : IController
{

    public void Control(IControllable controlable)
    {
        var transform = controlable.CGameObject.transform;
        if(controlable.CVelocity.magnitude == 0f) { return; }

        var camera = controlable.CCamera.transform;
        var moveVec = controlable.CVelocity;
        moveVec.y = 0f;
        moveVec = moveVec.normalized;


        //Quaternion tmp = camera.rotation;
        //camera.eulerAngles = new Vector3(0, camera.eulerAngles.y, 0);
        //moveVec = camera.TransformDirection(moveVec);
        var targetRotation = Quaternion.LookRotation(moveVec);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 
                                                5f * Time.deltaTime);
 
    }
}
