using GameLib;
using GameLib.Uitl.RayCast;
using GameLib.Util.Raycast;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



/*
 * Resibonsible for Control position of Character by given state
 */
public class MoveCtl :  IController
{
    private float gravity = -10f;
    private readonly float initVertSpeed = 10f;


    // States 
    private bool jumping = false;
    private float vertSpeed = 0f;


    public void Control(IControllable controlable)
    {
        var animator = controlable.CGameObject.GetComponent<Animator>();
        var transform = controlable.CGameObject.transform;
        var velocity = controlable.CVelocity;

        var horizontalDisp = velocity * Time.deltaTime;
        var verticalDisp = 0f;
        var position = controlable.CGameObject.transform.position;
        var capsuleCollider = controlable.CGameObject.GetComponent<CapsuleCollider>();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            velocity *= 4;
            horizontalDisp = velocity * Time.deltaTime;
        }


        animator.SetFloat("Speed", velocity.magnitude);
        //Debug.Log("Character speed is:" + velocity.magnitude);


        if (!controlable.IsGrounded) {
            vertSpeed += gravity * Time.deltaTime;
        }
        
        if (!jumping && Input.GetButtonDown("Jump"))
        {
            jumping = true;
            animator.SetTrigger("Jump");
            vertSpeed = initVertSpeed;
        }
        verticalDisp = vertSpeed * Time.deltaTime;

        if(RaycastHelper
            .Of(CastedObject
                    .Ray(position, Vector3.down)
                    .IgnoreTrigger())
            .IsCloserThan(-verticalDisp))
        {
            vertSpeed = 0f;
            verticalDisp = 0f;
            jumping = false;
        }


        if (capsuleCollider != null) {
            Vector3 point1 = position;
            Vector3 point2 = point1 + capsuleCollider.height * transform.up;
            float redius = capsuleCollider.radius;

            var castedCapsule = CastedObject
                .Capsule(point1, point2, redius, horizontalDisp.normalized)
                .IgnoreTrigger();

            if(RaycastHelper
                .Of(castedCapsule)
                .IsCloserThan(0.01f))
            {
                    horizontalDisp = Vector3.zero;
            }
        }else
        {
            Debug.Log("CapsuleCoolider is null");
        }

        //if (RaycastUtil.IsNearestColliderCloserThan(position, disp.normalized, 0.01f, out var casthit))
        //{

        //      disp = Vector3.zero;
        //}

        var disp = new Vector3(horizontalDisp.x, verticalDisp, horizontalDisp.z);

        transform.position += disp;
    }
}
