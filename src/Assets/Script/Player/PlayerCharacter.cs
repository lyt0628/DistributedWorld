using GameLib.DI;
using QS.Api;
using QS.Api.Chara.Service;
using QS.Api.Combat.Domain;
using QS.Api.Common;
using QS.Api.Common.Util.Detector;

using QS.Api.Executor.Domain;
using QS.Api.Executor.Service;
using QS.Api.Skill;
using QS.Api.Skill.Domain;
using QS.Chara.Abilities;
using QS.Chara.Domain;
using QS.Combat.Domain;
using QS.Common.Util;
using QS.Executor;
using QS.GameLib.Pattern.Message;
using QS.GameLib.Rx.Relay;
using QS.GameLib.Util;
using QS.Player;
using QS.PlayerControl;
using QS.Skill;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace QS.Player
{

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
        readonly IInstructionFactory instrFactory;
        [Injected]
        readonly IHandlerFactory instructionHandlerFactory;
        [Injected]
        readonly ICharaInsrFactory characterInsructionFactory;
        [Injected]
        readonly ICharaAblityFactory charaAbilityFactory;
        [Injected]
        readonly ISkillRepo skillRepo;
        [Injected]
        readonly ISkillAblityFactory skillAblityFactory;
        [Injected]
        readonly IPlayerData playerChara;
        [Injected]
        PlayerControls playerControls;

        ICharaConrolAbility charaConrolAbility;
        bool _frozen = false;
        readonly LongPress dash = new(.2f);
        readonly Click roll = new(.2f);

        Vector3 Forward3p
        {
            get
            {
                var f = Camera.main.transform.forward;
                f.y = 0;
                return f.normalized;
            }
        }
        Vector3 Right3P
        {
            get
            {
                var f = Camera.main.transform.right;
                f.y = 0;
                return f.normalized;
            }
        }


        public override bool Frozen { 
            get => _frozen;
            set 
            {
                _frozen = value;
                if (_frozen) 
                {
                    charaConrolAbility.Enabled = false;
                }
                else
                {
                    charaConrolAbility.Enabled = true;
                }
                    
            }
        }

        void Start()
        {
            PlayerGlobal.Instance.DI.Inject(this);
           
            SkillGlobal.Instance.OnReady.AddListener(() =>
            {
                AddLast(MathUtil.UUID(),
                    skillAblityFactory.Create(this, skillRepo.GetSkill("00002")));

                // ShuffleStep
                //AddLast(MathUtil.UUID(),
                //    skillAblityFactory.Create(this, skillRepo.GetSkill("00003")));
            });

            var animator = GetComponent<Animator>();
            //var combat = GetComponent<CombatorBehaviour>();
            var combator = new DefaultCombator();

            charaConrolAbility = charaAbilityFactory.CharaControl(this);

            AddLast(MathUtil.UUID(), instructionHandlerFactory.Filter(this));
            AddLast(MathUtil.UUID(), charaConrolAbility);
            AddLast(MathUtil.UUID(), charaAbilityFactory.ShuffleStep(this));
            AddLast(MathUtil.UUID(), instructionHandlerFactory.Combat(this, combator));
            //AddLast(MathUtil.UUID(), instructionHandlerFactory.Instantiate(this));
            //AddLast(MathUtil.UUID(), instructionHandlerFactory.Combat(this, combat));

            playerChara.ActivedCharacter = this;

            //var h = Addressables.LoadAssetAsync<GameObject>("RustSword");
            //h.Completed += (h) =>
            //{
            //    var go = GameObject.Instantiate(h.Result);

            //    // 這也是變化點，必須得封裝起來 
            //    var originalMesh = transform.Find("Mesh").GetComponent<SkinnedMeshRenderer>();
            //    var mesh = go.transform.Find("RustSword");
            //    var skin = mesh.GetComponent<SkinnedMeshRenderer>();
            //    var skeleton = go.transform.Find("Skeleton");

                
            //    AnimationUtil.RebindSkeleton(gameObject.transform, originalMesh,
            //                                "Skeleton", skeleton.transform, skin.bones);

            //    mesh.transform.parent = transform;
            //    GetComponent<Animator>().Rebind();
            //};

        }


        void Update()
        {
            //if (Input.GetKey(KeyCode.LeftControl))
            //{
            //    Execute(characterInsructionFactory.ShuffleStep(
            //        new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")),
            //        transform.forward, transform.right));
            //}
            //else
            //{
               
                Execute(characterInsructionFactory.CharaConrol(
                    Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"),
                    dash.Input(playerControls.Player.Dash.IsPressed()), playerControls.Player.Jump.triggered,
                    roll.Input(playerControls.Player.Dash.IsPressed()),
                    Right3P, Forward3p, Vector3.up));
            //}
            //if (Input.GetMouseButtonDown(1))
            //{
            //    Execute(instrFactory.Injured(10,10));
            //}
            
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

}