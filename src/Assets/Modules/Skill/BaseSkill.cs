

using QS.Api.Executor.Domain;
using QS.Api.Skill.Domain;
using QS.Chara.Domain;
using QS.Common.FSM;
using System;
using UnityEngine.Events;

namespace QS.Skill
{

    /// <summary>
    /// 这个类的作用是处理技能的生命周期,技能是有限状态机，并且指顺序执行的
    /// 技能类作为技能状态的主要沙盒功能来源
    /// 
    /// 仅仅只是继承自FSM 就更灵活，但是能力就稍稍不足够，依靠闭包的上值来解决一部分吧
    /// </summary>
    public abstract class BaseSkill : FSM<SkillStage>
    {
        protected BaseSkill(Character chara)
        {
            Chara = chara != null ? chara : throw new ArgumentNullException(nameof(chara));
            Chara.ControlFSM.BeforeHit.AddListener(OnInterrupt);
            Chara.OnAnimMsg.AddListener(OnAnimMsg);
        }

        void OnAnimMsg(string msg)
        {
            if(GetState(CurrentState) is BaseSkillState skillState)
            {
                skillState.OnAnimMsg(msg);
            }
        }

        public Character Chara { get; }
        public bool Casting => CurrentState != SkillStage.Recoveried;

        /// <summary>
        /// 当宿主想要触发技能时候的回调
        /// </summary>
        public UnityEvent OnTryCast { get; } = new();

        public override SkillStage DefaultState => SkillStage.Recoveried;

        public virtual void OnInterrupt()
        {
            if (!Casting) return;
            if(GetState(CurrentState) is not BaseSkillState skillState) return;
            if (!skillState.Interrutable) return;


            /// 状态是具体的执行者，中断的消除应当由状态来处理
            skillState.OnInterrupt();
            SwitchTo(SkillStage.Recoveried);
        }


        protected virtual void OnPlayerHint() { }
        public override void Handle(IInstruction instruction)
        {
            OnPlayerHint();
            OnTryCast.Invoke();
            if (!Casting)
            {
                SwitchTo(SkillStage.Precast);
            }
        }

        public void NextStage()
        {
            var nextStage = CurrentState switch
            {
                SkillStage.Precast => SkillStage.Casting,
                SkillStage.Casting => SkillStage.Postcast,
                SkillStage.Postcast => SkillStage.Shutdown,
                SkillStage.Shutdown => SkillStage.Recoveried,
                _ => SkillStage.Recoveried,
            };
            SwitchTo(nextStage);
        }
    }

}
