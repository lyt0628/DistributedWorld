using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

     private IController inputCtl;
     private IController moveCtl;
    private IController rotateCtl;

    // Start is called before the first frame update
    void Start()
    {
        CCamera = Camera.main;
        CVelocity = Vector3.zero;
        CGameObject = gameObject;

        inputCtl = new InputController();
        moveCtl = new MoveCtl();
        rotateCtl = new RotateCtl();
    }

    // Update is called once per frame
    void Update()
    {
        inputCtl.Control(this);
        //Debug.LogFormat("Character::: CVelocity is :{0}", CVelocity);
        moveCtl.Control(this);
        rotateCtl.Control(this);
    }
}
