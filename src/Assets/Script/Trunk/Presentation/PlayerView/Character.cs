using GameLib.DI;
using QS.Api.Service;
using QS.Domain.Item;
using QS.Impl.Data.Gateway.Facade;
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
    PlayerItemRepofacade playerItemRepoFacade;

    void Awake()
    {
        var ctx = TrunkGlobal.Instance.GlobalDIContext;

        _ = ctx.BindInstance("Player", gameObject)
           .BindInstance("PlayerTransform", transform);
    }


    void Start()
    {
        TrunkGlobal.Instance.GlobalDIContext.Inject(this);


        /// ���ܴ������, ������ǽӿ���, �Ҳ�ѯ������Ϊ�˴���
        /// ��ʵ�ֵ�Ŀ��, ��������������������
        /// ���¼������, ���ǲ�������, 
        /// �����Ϊ��չʾ����,������View�ķ���, 
        /// �ҵô��� VO, ��Ϊ�Ҳ��ܰ����򷽷�Ҳһ��¶��View,
        /// View�����ݶ��, ��Ӧ�� �� Domain ���, һ���򵥵� map����,
        /// �󲿷������, ���� ���������Ҫ֪��ȷ�е�����, ������ֻҪ
        /// ֪��IItem,���������þͺ�
        /// ���������Ҫ֪�� Domain,���Domainֻ��¶��ֻ�����Ժ����򷽷�
        /// ����������ڵĳ���ʽ������, ��۾���Ҫʵ�������ֳ���
        /// IItem - Ϊ�˱�������
        /// Domain - Ϊ��ʵ�ַ���
        /// VO - Ϊ����ͼչʾ
        /// 
        /// ��������� Wrap��Unwrap�������ݶ���ʹ��ʵ�ʽӿ�,
        /// ��ʱ�� ���򷽷��ķ�װʧЧ��, 
        /// ��ͨ�� tag�ӿ�, ʵ�� get/set/method �ķ���, 
        /// ʹ��һ�������Ӧ�� ���¼-��д����, Domain-ֻ������,���򷽷�
        /// ������Ӧ�ó���, ���� ���޷� ������ ���Ӷ� �����е�VO�ķ�װ������
        /// ���VOҪ������, MVC�Ļص��ɻ��¼�е�, ����֪ͨ����ļ�¼,
        /// ��������Ĺ���,��һ����������װ��
        /// �ҵ�Ϊ������һ�����Facade
        /// �������������������ݲ�ѯ��Ŀ������
        //var w =  worldItemData.Find("Rust Sword");
        //Debug.Log(w.Name);
        //Debug.Log(w.Description);
        //Debug.Log(w.Type);

        //var w = weaponRepo
        //     .Find(w => w.Breed.Name == "Rust Sword")
        //     .Unwrap()
        //     .Clone();

        //// ͨ���ӿ��õ��ľ���ֻ����
        //var  s = worldItems.GetItemProto<IWeapon>("Rust Sword");
        //// ͨ��ʵ�����õ��� ���ǿ�д��, ֻҪ, ���ⲿ�ֳ��򼯶�������, �����ֻ�ᱩ¶
        //// ֻ���ӿ�, ʵ��������װ, ʵ�ֲ�ܶ�ط�����Ҫ��дDomain
        //// Trunk��, ��ֻӦ���� ֻ��Domain
        //var t = worldItems.GetItemProto<Weapon>("Rust Sword");
        //Debug.Log(s);
        //Debug.Log(t);
        ///// ������ӵ���Ҳֿ�
        //var iItem = inventoryItemRepo.Create();
        //iItem.Wrap(w);

        //iItem.Save();



        playerItemRepoFacade.AddItem("Rust Sword");
        var ws = playerItemRepoFacade.GetItems<IWeapon>();
        foreach (var w in ws)
        {
            // ��Ȼ�����¶�ǲ����ѵ�,����, ���ɲ���ʱ��Ҳ��Ҫֱ��������ģ�ͽӴ�
            // �Ҳ����ܰ����еķ��񶼷ŵ�Impl��
            w.Refine(10);
            //playerItemRepoFacade.UpdateItem(w);
            Debug.Log(w.Name);
            Debug.Log(w.Exp);
        }

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
