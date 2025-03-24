using QS.Api.Executor.Domain;

namespace QS.Skill
{
    public struct KatanaLightAttackInstr : IInstruction { }

    //    public class SKLightAttack : HintPhasedSkill
    //    {
    //        /// <summary>
    //        /// DD 系列为了方便会有一些约定， 比如 技能内触发的 PhasedSkill 的下一阶段trigger是固定为
    //        /// Combo
    //        /// </summary>
    //        private const string NEXT_TRIGGER = "Combo";
    //        private const string SKILL_TRIGGER = "SK_LightAttack";
    //        readonly IState<SkillStage> casingStage;
    //        readonly IState<SkillStage> precastBegin;
    //        readonly IState<SkillStage> precastCombo;
    //        readonly IState<SkillStage> postcastState;
    //        readonly IState<SkillStage> shutdownState;

    //        [Injected]
    //        readonly ICombatInstrFacotry combatInstrs;
    //        [Injected]
    //        readonly ISkillFactory skillFactory;
    //        [Injected]
    //        readonly IDetectorFactory detectorFactory;


    //        readonly IInstruction instr;

    //        readonly ISpanDetector spanDetector;

    //        public SKLightAttack(Character chara) : base(chara)
    //        {
    //            CharaGlobal.Instance.Inject(this);




    //            /// 这个指令处理计算，还要一个处理表现的指令
    //            instr = combatInstrs.AttackedInstr(chara.Combator, skillFactory.Skill01());

    //            var wepaon = GameObjectUtil.FindChild(Chara.transform, "ATK_Weapon");
    //            spanDetector = detectorFactory.Collide(wepaon.GetComponent<Collider>(), CollideStage.Enter);


    //            precastBegin = new DDMsgSwithSkillState(this, SkillStage.Precast, SKILL_TRIGGER, onEnter: () => Chara.ControlFSM.SwitchTo(CharaState.RootMotion));
    //            precastCombo = new DDMsgSwithSkillState(this, SkillStage.Precast, NEXT_TRIGGER, onEnter: () => Chara.ControlFSM.SwitchTo(CharaState.RootMotion));
    //            casingStage = new DDDetectSkillState(this, SkillStage.Casting,
    //                detectorProvider: () => spanDetector,
    //                beforeDetectCB: () => spanDetector.Enable(),
    //                onEndDetectCB: (objs) =>
    //                {
    //                    spanDetector.Disable();
    //                    foreach (var target in objs)
    //                    {
    //                        if (target.TryGetComponent<CombatorBehaviour>(out var combator))
    //                        {
    //                            combator.TryHande(instr);
    //                        }
    //                    }
    //                });
    //            postcastState = new DDMsgSwithSkillState(this, SkillStage.Postcast);
    //            shutdownState = new DDMsgSwithSkillState(this, SkillStage.Shutdown,
    //                                                    onExit: () => Chara.ControlFSM.SwitchTo(CharaState.Idle));
    //        }

    //        public override int PhaseCount { get; } = 6;

    //        public override bool CanHandle(IInstruction instruction) => instruction is KatanaLightAttackInstr;

    //        protected override bool CanSwitchPhaseOnPlayerHint(int currentSkill)
    //        {
    //            if (CurrentState is SkillStage.Shutdown or SkillStage.Recoveried) return false;

    //            return Chara.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.1;
    //        }

    //        protected override IState<SkillStage> GetPhaseState(int currentPhase, SkillStage stage)
    //        {
    //            if (stage is SkillStage.Casting)
    //            {
    //                return casingStage;
    //            }
    //            if (stage is SkillStage.Shutdown)
    //            {
    //                return shutdownState;
    //            }
    //            if (stage is SkillStage.Recoveried)
    //            {
    //                return IState<SkillStage>.Unit;
    //            }
    //            if (stage is SkillStage.Postcast)
    //            {
    //                return postcastState;
    //            };
    //            if (stage is SkillStage.Precast)
    //            {
    //                if (currentPhase == 0) return precastBegin;
    //                else return precastCombo;
    //            }

    //            throw new System.Exception();
    //        }
    //    }



}