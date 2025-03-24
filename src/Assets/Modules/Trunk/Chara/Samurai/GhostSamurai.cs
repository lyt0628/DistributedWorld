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
    ����״̬�� = ����״̬��
    �л�����״̬��ͬʱҪ�л�����״̬������֮����Ҫ�л�һЩ���
    ���ܶ�Ӧ���ܱ��������ܱ������ͻ���ģ�ͺͼ��ܱ�״̬����
    ��״̬�������°󶨶���״̬��
    ���ڶ�����ɫ����Ϸ�Ļ�����ɫ�����ж��塣������ Chara ģ��

    ����ע����ȫ�������������ְ������ǰ����ô��Ĺ�����

    ��ɫ�Ĺ���
     */
    [RequireComponent(typeof(Animator))]
    //[RequireComponent(typeof(SamuraiControlFSM))]
    public class GhostSamurai : Character
    {
        /// <summary>
        /// ��ҵ����붳�����ָ�����͸�controlMachine ��ָ��ǿ�ָ��
        /// </summary>
        /// ����Ҫʹ�õ�����״̬�����Եñ���FSM �ӿ�
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

            /// ���ָ�����㣬��Ҫһ��������ֵ�ָ��
            attackedInstr = combatInstrs.AttackedInstr(Combator, skillFactory.Skill01());

            var wepaon = GameObjectUtil.FindChild(transform, "ATK_Weapon");
            spanDetector = detectorFactory.Collide(wepaon.GetComponent<Collider>(), CollideStage.Enter);
            // �ҿɲ��뼼�ܺ�״̬���������һ�𣬻���������˵������Ҫ�������ܱ�
            // ����Ҫ�Ѵ��� Bow ��Skill��Ȼ��� Katana�� Skill ȡ��
            // ���ȣ��û������Instr�ǵ�һָ��ᱻӳ��Ϊ��ͬ�Ŀɱ������Instr����BowShoot��Katana Attack֮���ָ��
            // ��ʹ�������账������Skill������Եģ����Chara���ϱ�����Դ����������͵�ӳ�����
            // ������Խ��������ػ��߱���ж��֮��ġ�
            // ����������ֱ��ָ�����=>ֱ��ָ��=>״̬��صľ���ָ�� ���ת��Ӧ����Playerģ�鸺��
            // ���Chara����ѱ�Ҫ����Ϣ��������
            // ��ɫ�ֶ�̬װ��Ľ�ɫ��������Ϸ���ǣ�����Ӳ����Ľ�ɫ����Boss������Samurai��ʹ�ú�һ��ģʽ����
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