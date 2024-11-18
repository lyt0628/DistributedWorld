using GameLib.DI;
using GameLib.Impl;
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

    [Injected]
    IPlayerControllService playerControllService;

    [Injected]
    IPlayerInputData PlayerInput {  get; set; }

    void Awake()
    {
        var ctx = GameManager.Instance.GlobalDIContext;

        ctx.BindInstance("Player", gameObject)
           .BindInstance("PlayerTransform",transform)
           .BindInstance(this);

        var pm = GameManager.Instance.GetManager<IPlayerManager>();
        pm.RegisterCharacter(gameObject);

    }


    // Start is called before the first frame update
    void Start()
    {

        GameManager.Instance.GlobalDIContext.GetInstance<Character>(GetType());
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


        //Debug.Log(CharacterLocation.Location);
        inputCtl.Control(this);
        groundedCtl.Control(this);


        var translationDTO = playerControllService.GetTranslation();
        //Debug.LogWarning(translationDTO);
        var animator = GetComponent<Animator>();
        animator.SetFloat("Speed", translationDTO.Speed);
        animator.SetBool("Jumping", translationDTO.Jumping);
        transform.position += translationDTO.Displacement;
        //moveCtl.Control(this);
        rotateCtl.Control(this);
    }
}
