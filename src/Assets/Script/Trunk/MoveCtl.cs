using GameLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/*
 * Resibonsible for Control position of Character by given state
 */
public class MoveCtl :  IController
{
    private float gravity = -10f  * 3;
    private readonly float initVertSpeed = 10 * 2f;


    // States 
    private bool jumping = false;
    private float vertSpeed = 0f;


    public void Control(IControllable controlable)
    {
        var animator = controlable.CGameObject.GetComponent<Animator>();
        var velocity = controlable.CVelocity * Time.deltaTime;
        var position = controlable.CGameObject.transform.position;


        animator.SetFloat("Speed", velocity.magnitude);
        animator.SetFloat("Speed", 1);

        jumping = !controlable.IsGrounded;

        if (!jumping && Input.GetButtonDown("Jump"))
        {
            jumping = true;
            animator.SetTrigger("Jump");
            vertSpeed = initVertSpeed;
        }
        if (jumping)
        {
            vertSpeed += 5 * gravity * Time.deltaTime;
        }

        velocity.y = vertSpeed * Time.deltaTime;


        var lastCollider = RaycastUtil.GetNearestColliderDistance(position, Vector3.down);
        if (lastCollider != 0 && lastCollider < -velocity.y)
        {
            velocity.y = -lastCollider + 1f;
            jumping = false;
            vertSpeed = 0f;
        }
        if (RaycastUtil.IsNearestColliderCloserThan(position, velocity.normalized, 1f, out var casthit))
        {
              velocity = Vector3.zero;
        }

        animator.SetFloat("vertSpeed", velocity.y);
        controlable.CGameObject.transform.position += velocity;
    }
}
