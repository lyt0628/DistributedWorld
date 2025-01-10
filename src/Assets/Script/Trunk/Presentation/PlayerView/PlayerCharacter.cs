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
 *  世界的原始生命形态寻找到了不同的能量来源,形成了截然不同的生命.
 *  生物, 借助物理的能力生存, 人类是一种生物
 *  术灵, 借助法则的能力生存, 法则是不可抵抗的,一些术灵吞噬了破坏性的法则, 
 *  本身也极具攻击性, 是这个世界人类的主要灾害之一
 *  虫, 接近生命的本源, 是及其原始的生命形态, 拥有不可知的力量, 具有毁灭意识的虫,
 *  是世界最大的灾难.
 *  法则与生命的力量雄厚, 所以虫与术灵一般都没有发展出特别高的智能,但 这些生命本身就是
 *  奇迹的象征, 也存在几乎与人类无异的虫与术灵存在, 会思考的他们, 对于哲学的问题似乎特别关心
 *  
 *  虫是不可视的, 只有生命形态被虫入侵的生物才能看到虫,
 *  
 *  我们所处的世界只是这浩瀚世界的一部分, 在国度的边界, 是无尽的虚空, 
 *  那的附近是不可踏入的禁区.
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
        // 這個狀態是異步的東西，得通過全局事件管線來通知
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
