using GameLib.DI;
using QS.Api.Data;
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

    [Injected]
    readonly IPlayerCharacterData playerCharacter;

    //[Injected]
    //PlayerItemRepository playerItemRepoFacade;

    void Awake()
    {
        var ctx = TrunkGlobal.Instance.DI;

        _ = ctx.BindInstance("Player", gameObject)
           .BindInstance("PlayerTransform", transform);

    }


    void Start()
    {
        var ctx = TrunkGlobal.Instance.DI;
         ctx.Inject(this);
        playerCharacter.ActivedCharacter = gameObject;

        var tRelay = playerControllService.GetPlayerTranslation();
        tRelay.Subscrib((t) =>
        {
            var animator = GetComponent<Animator>();
            animator.SetFloat("Speed", t.Speed);
            animator.SetBool("Jumping", t.Jumping);

            transform.position += t.Displacement;
        });

        /// 不能从这个来, 这个类是接口类, 我查询数据是为了创建
        /// 是实现的目的, 因该依赖于其他数据入口
        /// 活动记录很优雅, 但是不够方便, 
        /// 如果是为了展示数据,比如向View的方向, 
        /// 我得传输 VO, 因为我不能把领域方法也一起暴露给View,
        /// View的数据多变, 不应该 和 Domain 耦合, 一个简单的 map足矣,
        /// 大部分情况下, 除了 数据入口需要知道确切的类型, 其他都只要
        /// 知道IItem,来保持引用就好
        /// 而服务层需要知道 Domain,这个Domain只暴露出只读属性和领域方法
        /// 所以数据入口的出形式有三种, 外观就是要实现这三种出口
        /// IItem - 为了保持引用
        /// Domain - 为了实现服务
        /// VO - 为了视图展示
        /// 
        /// 数据入口内 Wrap和Unwrap传输数据都是使用实际接口,
        /// 这时候 领域方法的封装失效了, 
        /// 我通过 tag接口, 实现 get/set/method 的分离, 
        /// 使得一个类可以应对 活动记录-读写属性, Domain-只读属性,领域方法
        /// 这两种应用场景, 但是 我无法 在增加 复杂度 让他承担VO的封装工作了
        /// 因此VO要独立做, MVC的回调由活动记录承担, 可以通知具体的记录,
        /// 这个监听的工作,由一个服务类来装载
        /// 我得为他创建一层外观Facade
        /// 服务程序从世界物体数据查询到目标物体
        //var w =  worldItemData.Find("Rust Sword");
        //Debug.Log(w.Name);
        //Debug.Log(w.Description);
        //Debug.Log(w.Type);

        //var w = weaponRepo
        //     .Find(w => w.Breed.Name == "Rust Sword")
        //     .Unwrap()
        //     .Clone();

        //// 通过接口拿到的就是只读的
        //var  s = worldItems.GetItemProto<IWeapon>("Rust Sword");
        //// 通过实例类拿到的 就是可写的, 只要, 将这部分程序集独立出来, 对外就只会暴露
        //// 只读接口, 实现完美封装, 实现层很多地方都需要读写Domain
        //// Trunk层, 则只应该有 只读Domain
        //var t = worldItems.GetItemProto<Weapon>("Rust Sword");
        //Debug.Log(s);
        //Debug.Log(t);
        ///// 将其添加到玩家仓库
        //var iItem = inventoryItemRepo.Create();
        //iItem.Wrap(w);

        //iItem.Save();



        //playerItemRepoFacade.AddItem("Rust Sword");
        //var ws = playerItemRepoFacade.GetItems<IWeapon>();
        //foreach (var w in ws)
        //{
        //    // 虽然这个暴露是不得已的,但是, 主干层有时候也需要直接与领域模型接触
        //    // 我不可能把所有的服务都放到Impl层
        //    w.Refine(10);
        //    //playerItemRepoFacade.UpdateItem(w);
        //    Debug.Log(w.Name);
        //    Debug.Log(w.Exp);
        //}

    }

    // UpdateIfNeed is called once per frame
    void Update()
    {
        //var translationDTO = playerControllService.GetTranslation();
        var rotation = playerControllService.GetRotation();


  

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
