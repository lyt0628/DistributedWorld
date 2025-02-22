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

            //    // �@Ҳ��׃���c����횵÷��b���� 
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