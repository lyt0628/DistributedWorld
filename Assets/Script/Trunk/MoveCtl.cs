using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/*
 * Resibonsible for Control position of Character by given state
 */
public class MoveCtl :  IController
{

    public void Control(IControllable controlable)
    {
        var animator = controlable.CGameObject.GetComponent<Animator>();

        var velocity = controlable.CVelocity ;
        animator.SetFloat("Speed", velocity.magnitude);

 
        //Debug.Log(velocity.magnitude);
        controlable.CGameObject.transform.position += velocity;
    }
}
