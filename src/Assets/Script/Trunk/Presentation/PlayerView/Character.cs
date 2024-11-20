using GameLib.DI;
using GameLib.Impl;
using QS.API;
using QS.API.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Chapter One : 繁星的第一区域
 */
public class Character : MonoBehaviour
{


    [Injected]
    readonly IPlayerControllService playerControllService;

    [Injected]
    readonly IPlayerCharacterData playerCharacter;

    void Awake()
    {
        var ctx = GameManager.Instance.GlobalDIContext;

        _ = ctx.BindInstance("Player", gameObject)
           .BindInstance("PlayerTransform", transform);
    }


    void Start()
    {
        GameManager.Instance.GlobalDIContext.Inject(this);
        playerCharacter.ActivedCharacter = gameObject;

    }

    // UpdateIfNeed is called once per frame
    void Update()
    {

        var translationDTO = playerControllService.GetTranslation();
        var rotation = playerControllService.GetRotation();


        var animator = GetComponent<Animator>();
        animator.SetFloat("Speed", translationDTO.Speed);
        animator.SetBool("Jumping", translationDTO.Jumping);

        transform.position += translationDTO.Displacement;

        transform.rotation = rotation;

    }
}
