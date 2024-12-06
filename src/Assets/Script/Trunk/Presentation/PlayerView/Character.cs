using GameLib.DI;
using QS.Api.Control.Domain;
using QS.Api.Control.Service;
using QS.Api.Data;
using QS.Api.Service;
using QS.GameLib.Rx.Relay;
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
public class Character : MonoBehaviour
{


    [Injected]
    readonly IPlayerControllService playerControllService;

    [Injected]
    readonly IControlledPointDataSource controlledPointDataSource;
    [Injected]
    readonly IControlledPointService controlPointService;
    [Injected]
    readonly IPlayerLocationData playerLocation;

    [Injected]
    readonly IPlayerCharacterData playerCharacter;

    IMotion tMotion;
    void Start()
    {
        TrunkGlobal.Instance.DI.Inject(this);
        playerCharacter.ActivedCharacter = gameObject;
        var animator = GetComponent<Animator>();

        //playerControllService
        //    .GetTranslation()
        //    .Subscrib((t) =>
        //    {
        //        var animator = GetComponent<Animator>();
        //        animator.SetFloat("Speed", t.Speed);
        //        animator.SetBool("Jumping", t.Jumping);

        //        transform.position += t.Displacement;
        //    });
        var data = controlledPointDataSource.Create();
        var dataRelay = Relay<IControlledPointData>
            .Tick(() =>
            {
                data.Position = transform.position;
                data.Horizontal = Input.GetAxis("Horizontal");
                data.Vertical = Input.GetAxis("Vertical");
                data.Jump = Input.GetButtonDown("Jump");
                data.BaseRight = playerLocation.Right;
                data.Baseforword = playerLocation.Forward;
                data.BaseUp = Vector3.up;
                return data;
            }, out tMotion);
        var uuid = controlledPointDataSource.New(dataRelay);
        var tRelay =  controlPointService.GetTranslation(uuid);
        tRelay.Subscrib((t) =>
        {
            animator.SetFloat("Speed", t.Speed);
            transform.position += t.Displacement;
        });

        playerControllService
            .GetRotation()
            .Subscrib((r) =>transform.rotation = r);
       
    }

    private void Update()
    {
        tMotion.Set();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var rigid = hit.collider.attachedRigidbody;
        if (rigid != null && !rigid.isKinematic)
        {
            rigid.velocity = hit.moveDirection * 3.0f;
        }
    }
}
