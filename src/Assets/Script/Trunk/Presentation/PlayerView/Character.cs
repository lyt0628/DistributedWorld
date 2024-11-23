using GameLib.DI;
using QS.Api.Service;
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
