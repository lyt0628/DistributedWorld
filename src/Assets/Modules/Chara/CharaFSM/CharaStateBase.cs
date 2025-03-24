

using QS.Chara.Abilities;
using QS.Common.FSM;

namespace QS.Chara
{
    public abstract class CharaStateBase : IState<CharaState>
    {
        public abstract CharaState State { get; }
        public CharaControlTemplate ControlFSM { get; }

        protected CharaStateBase(CharaControlTemplate controlFSM)
        {
            this.ControlFSM = controlFSM;

        }

        /// <summary>
        /// 子状态自己选择更新时间，InPlace动画选择Update，
        /// 根骨骼动画选择OnAnimationMove.
        /// 至于IK，那是不可重用的部分，具体的状态机自己处理
        /// </summary>
        public virtual ProcessTime ProcessTime => ProcessTime.Update;

        public virtual ITransition<CharaState>[] Transitions => ITransition<CharaState>.Empty;

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Process()
        {
            ControlFSM.ApplyDefaultDosplacement();
            ControlFSM.ApplyDefaultRotation();
        }
    }
}