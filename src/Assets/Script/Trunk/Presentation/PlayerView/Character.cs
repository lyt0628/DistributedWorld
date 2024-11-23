using GameLib.DI;
using QS.Api.Service;
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

    void Awake()
    {
        var ctx = GameManager.Instance.GlobalDIContext;

        _ = ctx.BindInstance("Player", gameObject)
           .BindInstance("PlayerTransform", transform);
    }


    void Start()
    {
        GameManager.Instance.GlobalDIContext.Inject(this);
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
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var rigid = hit.collider.attachedRigidbody;
        if (rigid != null && !rigid.isKinematic)
        {
            rigid.velocity = hit.moveDirection * 3.0f;
        }
    }
}
