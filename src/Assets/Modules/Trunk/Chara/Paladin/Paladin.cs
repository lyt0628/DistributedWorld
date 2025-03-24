//using GameLib.DI;
//using QS.Api.Common.Util.Detector;
//using QS.Api.Executor.Domain;
//using QS.Api.Skill.Domain;
//using QS.Chara;
//using QS.Chara.Abilities;
//using QS.Chara.Domain;
//using QS.Combat;
//using QS.Common;
//using QS.Common.FSM;
//using QS.Common.Util;
//using QS.Skill;
//using UnityEngine;

//namespace QS.Player
//{

//    /*
//    ����״̬�� = ����״̬��
//    �л�����״̬��ͬʱҪ�л�����״̬������֮����Ҫ�л�һЩ���
//    ���ܶ�Ӧ���ܱ��������ܱ������ͻ���ģ�ͺͼ��ܱ�״̬����
//    ��״̬�������°󶨶���״̬��
//    ���ڶ�����ɫ����Ϸ�Ļ�����ɫ�����ж��塣������ Chara ģ��

//    ����ע����ȫ�������������ְ������ǰ����ô��Ĺ�����

//    ��ɫ�Ĺ���
//     */
//    [RequireComponent(typeof(Animator))]
//    //[RequireComponent(typeof(SamuraiControlFSM))]
//    public class Paladin : Character
//    {
//        /// <summary>
//        /// ��ҵ����붳�����ָ�����͸�controlMachine ��ָ��ǿ�ָ��
//        /// </summary>
//        /// ����Ҫʹ�õ�����״̬�����Եñ���FSM �ӿ�



//        IFSM<SkillStage> lightAttack;

//        [Injected]
//        readonly IHintPhasedSkillBuilder phasedSkillBuilder;
//        [Injected]
//        readonly ICombatInstrFacotry combatInstrs;
//        [Injected]
//        readonly ISkillFactory skillFactory;
//        [Injected]
//        readonly IDetectorFactory detectorFactory;
//        [Injected]
//        readonly IHitInstr hitInstr;

//        IInstruction attackedInstr;

//        ISpanDetector spanDetector;
//        protected override void Start()
//        {
//            TrunkGlobal.Instance.Context.Inject(this);
//            base.Start();

//            /// ���ָ�����㣬��Ҫһ��������ֵ�ָ��
//            attackedInstr = combatInstrs.AttackedInstr(GetComponent<CombatorBehaviour>(), skillFactory.Skill01());

//            var wepaon = GameObjectUtil.FindChild(transform, "ATK_Weapon");
//            spanDetector = detectorFactory.Collide(wepaon.GetComponent<Collider>(), CollideStage.Enter);

//            //lightAttack = new SKLightAttack(this);
//            lightAttack = phasedSkillBuilder
//                                .Begin(this,
//                                       (i) => i is ISKLightAttackInstr,
//                                       out var stageFacotry)
//                                .NewPhase(0.0f)
//                                .Precast(stageFacotry.MsgSwitch("SK_LightAttack",
//                                                        onEnter: () => ControlFSM.SwitchTo(CharaState.RootMotion)))
//                                .Casting(stageFacotry.Detect(
//                                        detectorProvider: () => spanDetector,
//                                        onBeginDetectCB: () => spanDetector.Enable(),
//                                        onEndDetectCB: (objs) =>
//                                        {
//                                            spanDetector.Disable();
//                                            foreach (var target in objs)
//                                            {
//                                                if (!target.TryGetComponent<IExecutor>(out var executor))
//                                                {
//                                                    continue;
//                                                }
//                                                executor.Execute(attackedInstr);

//                                                var positionDelta = transform.position - target.transform.position;
//                                                var localDir = target.transform.InverseTransformDirection(positionDelta);
//                                                hitInstr.AttackDir = localDir;
//                                                hitInstr.AttackForce = 1.0f;
//                                                executor.Execute(hitInstr);
//                                            }
//                                        }))
//                                .Postcast(stageFacotry.MsgSwitch())
//                                .Shutdown(stageFacotry.MsgSwitch(onExit: () => ControlFSM.SwitchTo(CharaState.Idle)))
//                                .Recoveried(stageFacotry.Void())


//                                .NewPhase(0.0f)
//                                .Precast(stageFacotry.MsgSwitch("Combo",
//                                                        onEnter: () => ControlFSM.SwitchTo(CharaState.RootMotion)))
//                                .Casting(stageFacotry.Detect(
//                                        detectorProvider: () => spanDetector,
//                                        onBeginDetectCB: () => spanDetector.Enable(),
//                                        onEndDetectCB: (_) => spanDetector.Disable(),
//                                        onDetectedPerFrameCB: (_, objs) =>
//                                        {
//                                            foreach (var target in objs)
//                                            {
//                                                if (!target.TryGetComponent<IExecutor>(out var executor))
//                                                {
//                                                    continue;
//                                                }
//                                                executor.Execute(attackedInstr);

//                                                var positionDelta = transform.position - target.transform.position;
//                                                var localDir = target.transform.InverseTransformDirection(positionDelta);
//                                                hitInstr.AttackDir = localDir;
//                                                hitInstr.AttackForce = 0.1f;
//                                                hitInstr.HitStopTime = .2f;
//                                                executor.Execute(hitInstr);
//                                            }
//                                        }))
//                                .Postcast(stageFacotry.MsgSwitch())
//                                .Shutdown(stageFacotry.MsgSwitch(onExit: () => ControlFSM.SwitchTo(CharaState.Idle)))
//                                .Recoveried(stageFacotry.Void())



//                                .Build();
//            AddHandler(lightAttack);
//        }



//        void OnControllerColliderHit(ControllerColliderHit hit)
//        {
//            var rigid = hit.collider.attachedRigidbody;
//            if (rigid != null && !rigid.isKinematic)
//            {
//                rigid.velocity = hit.moveDirection * 3.0f;
//            }
//        }
//    }

//}