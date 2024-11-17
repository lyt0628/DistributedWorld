using QS.API;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Chapter One : 繁星的第一区域
 */
public class Character : MonoBehaviour, IControllable
{
    /*
     * CVelocity of Character
     */
    public Vector3 CVelocity { get; set; }


    /*
     *  camera
     */
    public Camera CCamera{ get; set; }


    public GameObject CGameObject{ get; set; }
    public bool IsGrounded { get; set; }

    private IController inputCtl;
    private IController moveCtl;
    private IController rotateCtl;
    private IController groundedCtl;

    // Start is called before the first frame update
    void Start()
    {
        CCamera = Camera.main;
        CVelocity = Vector3.zero;
        CGameObject = gameObject;

        inputCtl = new InputController();
        groundedCtl = new GroundedCtl();

        moveCtl = new MoveCtl();
        rotateCtl = new RotateCtl();
    }

    // UpdateIfNeed is called once per frame
    void Update()
    {
        inputCtl.Control(this);
        groundedCtl.Control(this);

        moveCtl.Control(this);
        rotateCtl.Control(this);
    }
}
