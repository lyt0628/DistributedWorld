using GameLib.DI;
using QS.Api.Common.Util.Detector;
using QS.Api.Executor.Domain;
using QS.Api.Skill.Domain;
using QS.Chara;
using QS.Chara.Abilities;
using QS.Chara.Domain;
using QS.Combat;
using QS.Common;
using QS.Common.FSM;
using QS.Common.Util;
using QS.GameLib.Util;
using QS.Skill;
using QS.Trunk.Chara.Samurai;
using UnityEngine;

namespace QS.Player
{

    /*
    控制状态机 = 动画状态机
    切换控制状态机同时要切换动画状态机，总之就是要切换一些组件
    技能对应技能表，换掉技能表。武器就换掉模型和技能表。状态机就
    换状态机和重新绑定动画状态机
    现在定义多角色的游戏的话，角色必须有定义。定义在 Chara 模块

    依赖注入完全可以替代工厂的职责，舍弃前面那么多的工厂类

    角色的构建
     */
    [RequireComponent(typeof(Animator))]
    //[RequireComponent(typeof(SamuraiControlFSM))]
    public class GhostSamurai : Character
    {
        /// <summary>
        /// 玩家的输入冻结就是指，发送给controlMachine 的指令都是空指令
        /// </summary>
        /// 我需要使用到他的状态，所以得保留FSM 接口
        public int currentWeapon = 1;

        [Injected]
        readonly IPlayer player;

        [Injected]
        readonly IHintPhasedSkillBuilder phasedSkillBuilder;
        [Injected]
        readonly ICombatInstrFacotry combatInstrs;
        [Injected]
        readonly ISkillFactory skillFactory;
        [Injected]
        readonly IDetectorFactory detectorFactory;


        IInstruction attackedInstr;
        [Injected]
        readonly IHitInstr hitInstr;

        ISpanDetector spanDetector;

        public override GameUnit Unit => m_Unit;
        GameUnit m_Unit;

        protected override void Start()
        {
            CharaGlobal.Instance.Inject(this);

            base.Start();
            m_Unit = new GameUnit(MathUtil.UUID(), UnitScope.Local);

            /// 这个指令处理计算，还要一个处理表现的指令
            attackedInstr = combatInstrs.AttackedInstr(Combator, skillFactory.Skill01());

            var wepaon = GameObjectUtil.FindChild(transform, "ATK_Weapon");
            spanDetector = detectorFactory.Collide(wepaon.GetComponent<Collider>(), CollideStage.Enter);
            // 我可不想技能和状态控制耦合在一起，还是那样子说，必须要设立技能表
            // 现在要把创建 Bow 的Skill，然后把 Katana的 Skill 取消
            // 首先，用户输入的Instr是单一指令，会被映射为不同的可被处理的Instr像是BowShoot，Katana Attack之类的指令
            // 这使得我无需处理具体的Skill，但相对的，这个Chara身上必须可以处理所有类型的映射项集。
            // 或许可以进行懒加载或者报错，卸载之类的。
            // 玩家输入的是直接指令，输入=>直接指令=>状态相关的具体指令 这个转换应该由Player模块负责
            // 因此Chara必须把必要的信息公布出来
            // 角色分动态装配的角色，像是游戏主角，还有硬编码的角色像是Boss，现在Samurai先使用后一个模式来做
            var katanaLightAttack = phasedSkillBuilder
                                .Begin(this,
                                       canHandleFunc: (i) =>
                                       {
                                           if (i is not KatanaLightAttackInstr) return false;

                                           if (ControlFSM.CurrentState is CharaState.Idle or CharaState.Walking or CharaState.Runing or CharaState.RootMotion)
                                           {
                                               return true;
                                           }
                                           return false;
                                       },
                                       out var katanaLT_stageFacotry)
                                .NewPhase(0.1f)
                                .Precast(katanaLT_stageFacotry.MsgSwitch("SK_LightAttack",
                                        onEnter: () => ControlFSM.SwitchTo(CharaState.RootMotion),
                                        interruptable: true,
                                        onInterruptCB: () =>
                                        {
                                            ControlFSM.SwitchTo(CharaState.Idle);
                                        }))
                                .Casting(katanaLT_stageFacotry.Detect(
                                        detectorProvider: () => spanDetector,
                                        beforeDetectCB: () => spanDetector.Enable(),
                                        onEndDetectCB: (_) => spanDetector.Disable(),
                                        onDetectedPerFrameCB: (_, objs) =>
                                        {
                                            foreach (var target in objs)
                                            {
                                                if (!target.TryGetComponent<Character>(out var executor))
                                                {
                                                    continue;
                                                }
                                                executor.Execute(attackedInstr);

                                                var positionDelta = wepaon.position - target.transform.position;
                                                var localDir = target.transform.InverseTransformDirection(positionDelta);
                                                hitInstr.AttackDir = localDir;
                                                hitInstr.AttackForce = 0.3f;
                                                hitInstr.HitStopTime = .3f;
                                                executor.Execute(hitInstr);

                                            }
                                        },
                                        onInterruptCB: () =>
                                        {
                                            spanDetector.Disable();
                                            ControlFSM.SwitchTo(CharaState.Idle);
                                        }))
                                .Postcast(katanaLT_stageFacotry.MsgSwitch(
                                    interruptable: true,
                                    onInterruptCB: () =>
                                    {
                                        ControlFSM.SwitchTo(CharaState.Idle);
                                    }))
                                .Shutdown(katanaLT_stageFacotry.MsgSwitch(
                                    interruptable: true,
                                    onExit: () => ControlFSM.SwitchTo(CharaState.Idle),
                                    onInterruptCB: () =>
                                    {
                                        ControlFSM.SwitchTo(CharaState.Idle);
                                    }))
                                .Recoveried(katanaLT_stageFacotry.Void())
 
                                .NewPhase(0.1f)
                                 .Precast(katanaLT_stageFacotry.MsgSwitch("Combo",
                                        onEnter: () => ControlFSM.SwitchTo(CharaState.RootMotion),
                                        interruptable: true,
                                        onInterruptCB: () =>
                                        {
                                            ControlFSM.SwitchTo(CharaState.Idle);
                                        }))
                                .Casting(katanaLT_stageFacotry.Detect(
                                        detectorProvider: () => spanDetector,
                                        beforeDetectCB: () => spanDetector.Enable(),
                                        onEndDetectCB: (_) => spanDetector.Disable(),
                                        onDetectedPerFrameCB: (_, objs) =>
                                        {
                                            foreach (var target in objs)
                                            {
                                                if (!target.TryGetComponent<Character>(out var executor))
                                                {
                                                    continue;
                                                }
                                                executor.Execute(attackedInstr);

                                                var positionDelta = wepaon.position - target.transform.position;
                                                var localDir = target.transform.InverseTransformDirection(positionDelta);
                                                hitInstr.AttackDir = localDir;
                                                hitInstr.AttackForce = 0.3f;
                                                hitInstr.HitStopTime = .3f;
                                                executor.Execute(hitInstr);

                                            }
                                        },
                                        onInterruptCB: () =>
                                        {
                                            spanDetector.Disable();
                                            ControlFSM.SwitchTo(CharaState.Idle);
                                        }))
                                .Postcast(katanaLT_stageFacotry.MsgSwitch(
                                    interruptable: true,
                                    onInterruptCB: () =>
                                    {
                                        ControlFSM.SwitchTo(CharaState.Idle);
                                    }))
                                .Shutdown(katanaLT_stageFacotry.MsgSwitch(
                                    onExit: () => ControlFSM.SwitchTo(CharaState.Idle),
                                    interruptable: true,
                                    onInterruptCB: () =>
                                    {
                                        ControlFSM.SwitchTo(CharaState.Idle);
                                    }))
                                .Recoveried(katanaLT_stageFacotry.Void())

                                .NewPhase(0.1f)
                                 .Precast(katanaLT_stageFacotry.MsgSwitch("Combo",
                                        onEnter: () => ControlFSM.SwitchTo(CharaState.RootMotion),
                                        interruptable: true,
                                        onInterruptCB: () =>
                                        {
                                            ControlFSM.SwitchTo(CharaState.Idle);
                                        }))
                                .Casting(katanaLT_stageFacotry.Detect(
                                        detectorProvider: () => spanDetector,
                                        beforeDetectCB: () => spanDetector.Enable(),
                                        onEndDetectCB: (_) => spanDetector.Disable(),
                                        onDetectedPerFrameCB: (_, objs) =>
                                        {
                                            foreach (var target in objs)
                                            {
                                                if (!target.TryGetComponent<Character>(out var executor))
                                                {
                                                    continue;
                                                }
                                                executor.Execute(attackedInstr);

                                                var positionDelta = wepaon.position - target.transform.position;
                                                var localDir = target.transform.InverseTransformDirection(positionDelta);
                                                hitInstr.AttackDir = localDir;
                                                hitInstr.AttackForce = 0.3f;
                                                hitInstr.HitStopTime = .3f;
                                                executor.Execute(hitInstr);

                                            }
                                        },
                                        onInterruptCB: () =>
                                        {
                                            spanDetector.Disable();
                                            ControlFSM.SwitchTo(CharaState.Idle);
                                        }))
                                .Postcast(katanaLT_stageFacotry.MsgSwitch(
                                    interruptable: true,
                                    onInterruptCB: () =>
                                    {
                                        ControlFSM.SwitchTo(CharaState.Idle);
                                    }))
                                .Shutdown(katanaLT_stageFacotry.MsgSwitch(
                                    onExit: () => ControlFSM.SwitchTo(CharaState.Idle),
                                    interruptable: true,
                                    onInterruptCB: () =>
                                    {
                                        ControlFSM.SwitchTo(CharaState.Idle);
                                    }))
                                .Recoveried(katanaLT_stageFacotry.Void())

                                .NewPhase(0.1f)
                                           .Precast(katanaLT_stageFacotry.MsgSwitch("Combo",
                                        onEnter: () => ControlFSM.SwitchTo(CharaState.RootMotion),
                                        interruptable: true,
                                        onInterruptCB: () =>
                                        {
                                            ControlFSM.SwitchTo(CharaState.Idle);
                                        }))
                                .Casting(katanaLT_stageFacotry.Detect(
                                        detectorProvider: () => spanDetector,
                                        beforeDetectCB: () => spanDetector.Enable(),
                                        onEndDetectCB: (_) => spanDetector.Disable(),
                                        onDetectedPerFrameCB: (_, objs) =>
                                        {
                                            foreach (var target in objs)
                                            {
                                                if (!target.TryGetComponent<Character>(out var executor))
                                                {
                                                    continue;
                                                }
                                                executor.Execute(attackedInstr);

                                                var positionDelta = wepaon.position - target.transform.position;
                                                var localDir = target.transform.InverseTransformDirection(positionDelta);
                                                hitInstr.AttackDir = localDir;
                                                hitInstr.AttackForce = 0.3f;
                                                hitInstr.HitStopTime = .3f;
                                                executor.Execute(hitInstr);

                                            }
                                        },
                                        onInterruptCB: () =>
                                        {
                                            spanDetector.Disable();
                                            ControlFSM.SwitchTo(CharaState.Idle);
                                        }))
                                .Postcast(katanaLT_stageFacotry.MsgSwitch(
                                    interruptable: true,
                                    onInterruptCB: () =>
                                    {
                                        ControlFSM.SwitchTo(CharaState.Idle);
                                    }))
                                .Shutdown(katanaLT_stageFacotry.MsgSwitch(
                                    onExit: () => ControlFSM.SwitchTo(CharaState.Idle),
                                    interruptable: true,
                                    onInterruptCB: () =>
                                    {
                                        ControlFSM.SwitchTo(CharaState.Idle);
                                    }))
                                .Recoveried(katanaLT_stageFacotry.Void())

                                .NewPhase(0.1f)
                                                              .Precast(katanaLT_stageFacotry.MsgSwitch("Combo",
                                        onEnter: () => ControlFSM.SwitchTo(CharaState.RootMotion),
                                        interruptable: true,
                                        onInterruptCB: () =>
                                        {
                                            ControlFSM.SwitchTo(CharaState.Idle);
                                        }))
                                .Casting(katanaLT_stageFacotry.Detect(
                                        detectorProvider: () => spanDetector,
                                        beforeDetectCB: () => spanDetector.Enable(),
                                        onEndDetectCB: (_) => spanDetector.Disable(),
                                        onDetectedPerFrameCB: (_, objs) =>
                                        {
                                            foreach (var target in objs)
                                            {
                                                if (!target.TryGetComponent<Character>(out var executor))
                                                {
                                                    continue;
                                                }
                                                executor.Execute(attackedInstr);

                                                var positionDelta = wepaon.position - target.transform.position;
                                                var localDir = target.transform.InverseTransformDirection(positionDelta);
                                                hitInstr.AttackDir = localDir;
                                                hitInstr.AttackForce = 0.3f;
                                                hitInstr.HitStopTime = .3f;
                                                executor.Execute(hitInstr);

                                            }
                                        },
                                        interruptable: true,
                                        onInterruptCB: () =>
                                        {
                                            spanDetector.Disable();
                                            ControlFSM.SwitchTo(CharaState.Idle);
                                        }))
                                .Postcast(katanaLT_stageFacotry.MsgSwitch(
                                    interruptable: true,
                                    onInterruptCB: () =>
                                    {
                                        ControlFSM.SwitchTo(CharaState.Idle);
                                    }))
                                .Shutdown(katanaLT_stageFacotry.MsgSwitch(
                                    onExit: () => ControlFSM.SwitchTo(CharaState.Idle),
                                    interruptable: true,
                                    onInterruptCB: () =>
                                    {
                                        ControlFSM.SwitchTo(CharaState.Idle);
                                    }))
                                .Recoveried(katanaLT_stageFacotry.Void())
                                .NewPhase(0.1f)
                                                              .Precast(katanaLT_stageFacotry.MsgSwitch("Combo",
                                        onEnter: () => ControlFSM.SwitchTo(CharaState.RootMotion),
                                        interruptable: true,
                                        onInterruptCB: () =>
                                        {
                                            ControlFSM.SwitchTo(CharaState.Idle);
                                        }))
                                .Casting(katanaLT_stageFacotry.Detect(
                                        detectorProvider: () => spanDetector,
                                        beforeDetectCB: () => spanDetector.Enable(),
                                        onEndDetectCB: (_) => spanDetector.Disable(),
                                        onDetectedPerFrameCB: (_, objs) =>
                                        {
                                            foreach (var target in objs)
                                            {
                                                if (!target.TryGetComponent<Character>(out var executor))
                                                {
                                                    continue;
                                                }
                                                executor.Execute(attackedInstr);

                                                var positionDelta = wepaon.position - target.transform.position;
                                                var localDir = target.transform.InverseTransformDirection(positionDelta);
                                                hitInstr.AttackDir = localDir;
                                                hitInstr.AttackForce = 0.3f;
                                                hitInstr.HitStopTime = .3f;
                                                executor.Execute(hitInstr);

                                            }
                                        },
                                        onInterruptCB: () =>
                                        {
                                            spanDetector.Disable();
                                            ControlFSM.SwitchTo(CharaState.Idle);
                                        }))
                                .Postcast(katanaLT_stageFacotry.MsgSwitch(
                                    interruptable: true,
                                    onInterruptCB: () =>
                                    {
                                        ControlFSM.SwitchTo(CharaState.Idle);
                                    }))
                                .Shutdown(katanaLT_stageFacotry.MsgSwitch(
                                    onExit: () => ControlFSM.SwitchTo(CharaState.Idle),
                                    interruptable: true,
                                    onInterruptCB: () =>
                                    {
                                        ControlFSM.SwitchTo(CharaState.Idle);
                                    }))
                                .Recoveried(katanaLT_stageFacotry.Void())
                                .Build();


            var bowLightAttack = phasedSkillBuilder
                                .Begin(this,
                                       canHandleFunc: (i) =>
                                       {
                                           if (i is not BowLightAttackInstr) return false;

                                           if (ControlFSM.CurrentState is CharaState.Idle or CharaState.Walking or CharaState.Runing or CharaState.RootMotion)
                                           {
                                               return true;
                                           }
                                           return false;
                                       },
                                       out var bowLT_stageFacotry)
                                .NewPhase(0.1f)
                                .Precast(katanaLT_stageFacotry.MsgSwitch("BowShoot",
                                        onEnter: () => ControlFSM.SwitchTo(CharaState.RootMotion),
                                        interruptable: true,
                                        onInterruptCB: () =>
                                        {
                                            ControlFSM.SwitchTo(CharaState.Idle);
                                        }))
                                .Casting(katanaLT_stageFacotry.Detect(
                                        detectorProvider: () => spanDetector,
                                        beforeDetectCB: () => spanDetector.Enable(),
                                        onEndDetectCB: (_) => spanDetector.Disable(),
                                        onDetectedPerFrameCB: (_, objs) =>
                                        {
                                            foreach (var target in objs)
                                            {
                                                if (!target.TryGetComponent<Character>(out var executor))
                                                {
                                                    continue;
                                                }
                                                executor.Execute(attackedInstr);

                                                var positionDelta = wepaon.position - target.transform.position;
                                                var localDir = target.transform.InverseTransformDirection(positionDelta);
                                                hitInstr.AttackDir = localDir;
                                                hitInstr.AttackForce = 0.3f;
                                                hitInstr.HitStopTime = .3f;
                                                executor.Execute(hitInstr);

                                            }
                                        },
                                        onInterruptCB: () =>
                                        {
                                            spanDetector.Disable();
                                            ControlFSM.SwitchTo(CharaState.Idle);
                                        }))
                                .Postcast(katanaLT_stageFacotry.MsgSwitch(
                                    interruptable: true,
                                    onInterruptCB: () =>
                                    {
                                        ControlFSM.SwitchTo(CharaState.Idle);
                                    }))
                                .Shutdown(katanaLT_stageFacotry.MsgSwitch(
                                    interruptable: true,
                                    onExit: () => ControlFSM.SwitchTo(CharaState.Idle),
                                    onInterruptCB: () =>
                                    {
                                        ControlFSM.SwitchTo(CharaState.Idle);
                                    }))
                                .Recoveried(katanaLT_stageFacotry.Void())
                .Build();
            HandlerGroup.Add(katanaLightAttack);
            HandlerGroup.Add(bowLightAttack);
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