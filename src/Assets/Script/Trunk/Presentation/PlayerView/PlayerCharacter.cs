using GameLib.DI;
using Mono.Reflection;
using QS.Api.Character.Service;
using QS.Api.Common;
using QS.Api.Control.Domain;
using QS.Api.Control.Service;
using QS.Api.Data;
using QS.Api.Executor.Domain;
using QS.Api.Executor.Service;
using QS.Api.Skill.Domain;
using QS.Api.Skill.Service;
using QS.Chara.Domain;
using QS.GameLib.Pattern.Message;
using QS.GameLib.Rx.Relay;
using QS.GameLib.Util;
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
    readonly IInstructionHandlerFactory instructionHandlerFactory;
    [Injected]
    readonly ICharaInsrFactory characterInsructionFactory;
    [Injected]
    readonly ICharaAblityFactory charaAbilityFactory;
    [Injected]
    readonly ISkillInstrFactory skillInstrFactory;
    [Injected]
    readonly ISkillAblityFactory skillAblityFactory;

    [Injected]
    readonly IPlayerLocationData playerLocation;

    void Start()
    {
        TrunkGlobal.Instance.DI.Inject(this);

        var animator = GetComponent<Animator>();
        var combat = GetComponent<CombatorBehaviour>();

        AddLast(MathUtil.UUID(), instructionHandlerFactory.Move(this, transform, animator));
        AddLast(MathUtil.UUID(), instructionHandlerFactory.Instantiate(this));
        AddLast(MathUtil.UUID(), charaAbilityFactory.Injured(this, combat));
        var h = skillAblityFactory.Simple(this, "00001", "FireBall");
        h.AddSubHandler(SimpleSkillStage.CastingEnter, () => Debug.Log("CCCXXX"));
        AddLast(MathUtil.UUID(), h);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Execute(instructionFactory.Instantiate("BD", transform));
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            Execute(skillInstrFactory.Simple("00001", "FireBall"));
        }

        Execute(instructionFactory.Move(
            Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), Input.GetButtonDown("Jump"),
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


}
