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
//    控制状态机 = 动画状态机
//    切换控制状态机同时要切换动画状态机，总之就是要切换一些组件
//    技能对应技能表，换掉技能表。武器就换掉模型和技能表。状态机就
//    换状态机和重新绑定动画状态机
//    现在定义多角色的游戏的话，角色必须有定义。定义在 Chara 模块

//    依赖注入完全可以替代工厂的职责，舍弃前面那么多的工厂类

//    角色的构建
//     */
//    [RequireComponent(typeof(Animator))]
//    //[RequireComponent(typeof(SamuraiControlFSM))]
//    public class Paladin : Character
//    {
//        /// <summary>
//        /// 玩家的输入冻结就是指，发送给controlMachine 的指令都是空指令
//        /// </summary>
//        /// 我需要使用到他的状态，所以得保留FSM 接口



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

//            /// 这个指令处理计算，还要一个处理表现的指令
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