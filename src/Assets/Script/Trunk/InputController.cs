using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Get input from end device, and convert it into states of game of character
 */
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


        //moveVec = new Vector3(0, 0, 1f);

        //Quaternion tmp = camera.rotation;
        //camera.eulerAngles = new Vector3(0, camera.eulerAngles.y, 0);
        //moveVec = camera.TransformDirection(moveVec);
        //camera.rotation = tmp;

        //Debug.Log(moveVec);
        //Debug.Log(camera.forward);

        controlable.CVelocity =  velocityFactor * moveVec ;
    }

}

