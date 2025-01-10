using GameLib.DI;
using QS.Api;
using QS.Api.Chara.Service;
using QS.Api.Common;
using QS.Api.Common.Util.Detector;
using QS.Api.Control.Domain;
using QS.Api.Control.Service;
using QS.Api.Data;
using QS.Api.Executor.Domain;
using QS.Api.Executor.Service;
using QS.Api.Skill;
using QS.Api.Skill.Domain;
using QS.Chara.Domain;
using QS.Executor;
using QS.GameLib.Pattern.Message;
using QS.GameLib.Rx.Relay;
using QS.GameLib.Util;
using QS.Skill;
using System;
using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

/*
 *  �����ԭʼ������̬Ѱ�ҵ��˲�ͬ��������Դ,�γ��˽�Ȼ��ͬ������.
 *  ����, �����������������, ������һ������
 *  ����, �����������������, �����ǲ��ɵֿ���,һЩ�����������ƻ��Եķ���, 
 *  ����Ҳ���߹�����, ����������������Ҫ�ֺ�֮һ
 *  ��, �ӽ������ı�Դ, �Ǽ���ԭʼ��������̬, ӵ�в���֪������, ���л�����ʶ�ĳ�,
 *  ��������������.
 *  �����������������ۺ�, ���Գ�������һ�㶼û�з�չ���ر�ߵ�����,�� ��Щ�����������
 *  �漣������, Ҳ���ڼ�������������ĳ����������, ��˼��������, ������ѧ�������ƺ��ر����
 *  
 *  ���ǲ����ӵ�, ֻ��������̬�������ֵ�������ܿ�����,
 *  
 *  ��������������ֻ�����������һ����, �ڹ��ȵı߽�, ���޾������, 
 *  �ǵĸ����ǲ���̤��Ľ���.
 */
[RequireComponent(typeof(Animator))]
public class PlayerCharacter : Character
{
    [Injected]
    readonly IInstructionFactory instructionFactory;
    [Injected]
    readonly IHandlerFactory instructionHandlerFactory;
    [Injected]
    readonly ICharaInsrFactory characterInsructionFactory;
    [Injected]
    readonly ICharaAblityFactory charaAbilityFactory;
    [Injected]
    readonly ISkillRepo skillRepo;
    [Injected]
    readonly ISkillInstrFactory skillInstrFactory;
    [Injected]
    readonly ISkillAblityFactory skillAblityFactory;


    [Injected]
    readonly IPlayerLocationData playerLocation;
    [Injected]
    readonly IDetectorFactory detectorFactory;

    void Start()
    {
        TrunkGlobal.Instance.DI.Inject(this);

        var animator = GetComponent<Animator>();
        var combat = GetComponent<CombatorBehaviour>();


        var handler = instructionHandlerFactory.Filter(this);
        AddLast(MathUtil.UUID(), handler);
        AddLast(MathUtil.UUID(), charaAbilityFactory.Translate(this, transform, animator));
        AddLast(MathUtil.UUID(), instructionHandlerFactory.Instantiate(this));
        AddLast(MathUtil.UUID(), instructionHandlerFactory.Injured(this, combat));


    }
    bool flg = false;


    void Update()
    {
        // �@����B�Ǯ����Ė|������ͨ�^ȫ���¼��ܾ���֪ͨ
        if (!flg && Input.GetMouseButtonDown(0)
            && SkillGlobal.Instance.ResourceStatus == ResourceInitStatus.Started)
        {
            var sk = skillRepo.GetSkill("00001");
            AddLast(MathUtil.UUID(), skillAblityFactory.Create(this, sk));

            flg = true;
        }
        if ( Input.GetMouseButtonDown(1))
        {
            var sk = skillRepo.GetSkill("00001");
            var skInstr = skillInstrFactory.Create(sk);
            Execute(skInstr);
            Messager.Boardcast("SK00001_FireBall_CastingEnter", Msg0.Instance);
            StartCoroutine(MsgDelay());
     
        }

        //if (Input.GetKeyDown(KeyCode.J))
        //{
        //    Execute(instructionFactory.Instantiate("BD", transform));
        //}
        //if(Input.GetKeyDown(KeyCode.F))
        //{
        //    Execute(skillInstrFactory.Create("00001", "FireBall"));
        //    var animator = GetComponent<Animator>();
        //    animator.SetTrigger("Attack");

        //}
        if (Input.GetMouseButton(0))
        {
            var animator = GetComponent<Animator>();
            animator.SetTrigger("Punch");
        }

        Execute(characterInsructionFactory.Translate(
            Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 
            Input.GetKey(KeyCode.LeftShift) ,Input.GetButtonDown("Jump"),
            playerLocation.Right, playerLocation.Forward, Vector3.up));
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var rigid = hit.collider.attachedRigidbody;
        if (rigid != null && !rigid.isKinematic)
        {
            rigid.velocity = hit.moveDirection * 3.0f;
        }
    }




    IEnumerator MsgDelay()
    {
        yield return new WaitForSeconds(1);
        Messager.Boardcast("SK00001_FireBall_CastingExit", Msg0.Instance);
    }
}
