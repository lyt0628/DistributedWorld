using QS.Api.Executor.Domain;
using System;
using UnityEngine;

namespace QS.Common.FSM
{
    public enum ProcessTime
    {
        Update,
        LateUpdate,
        FixedUpdate,
        AnimationMove,
        UpdateAndAnimationMove
    }
    /// <summary>
    /// 自己用的东西作为类就好. 
    /// 这个状态机被理解为用于处理一些指令的状态机
    /// </summary>
    /// <typeparam name="TState"></typeparam>

    public abstract class FSMBehaviour<TState> : MonoBehaviour, IHandler, IFSM<TState>
        where TState : struct, Enum
    {

        public bool Transiting { get; private set; } = false;
        public ITransition<TState> ActiveTransition { get; private set; }

        /// <summary>
        /// 强制执行一段迁移
        /// </summary>
        public void ForceTransite(ITransition<TState> transition)
        {
            TransiteTo(transition);
        }
        /// <summary>
        /// 不要使用有参构造
        /// </summary>
        protected FSMBehaviour()
        {
            CurrentState = DefaultState;
        }

        public virtual ProcessTime ProcessTime { get; } = ProcessTime.Update;
        public ProcessTime CurrentProcessTime { get; private set; }

        public abstract TState DefaultState { get; }
        public TState CurrentState { get; private set; }
        public abstract IState<TState> GetState(TState state);

        /// <summary>
        /// 实际执行状态迁移的是这个家伙，
        /// 它不会考虑状态迁移
        /// </summary>
        /// <param name="to"></param>
        public virtual void SwitchTo(TState to)
        {
            // 允许状态重入
            GetState(CurrentState).Exit();
            CurrentState = to;
            GetState(to).Enter();
        }

        /// <summary>
        /// 没法构造的原因是依赖于Mono，现在只有Update这个依赖，直接
        /// 通过 LifeCycle 来提供活动能力，缺点是无法跟随根骨骼动画，
        /// 比起继承自Mono，不如直接准备一个根骨骼
        /// Update 最早执行，在里面执行状态迁移
        /// </summary>
        protected virtual void Update()
        {
            /// 检查状态迁移一致在Update 阶段，但是 迁移的动作在LateUpdate 阶段执行和状态是一样强大的
            if (!Transiting) CheckTransition();


            if ((ProcessTime is not ProcessTime.Update
                    && ProcessTime is not ProcessTime.UpdateAndAnimationMove)) return;
            CurrentProcessTime = ProcessTime.Update;


            DoProcess();


        }

        private void TransitionState()
        {
            if (ActiveTransition.Transite())
            {
                Transiting = false;
                CurrentState = ActiveTransition.Target;
                GetState(ActiveTransition.Target).Enter();
            }
        }

        private void CheckTransition()
        {
            var curState = GetState(CurrentState);

            var transitions = curState.Transitions;
            foreach (var transition in transitions)
            {
                //Debug.Log("Check Transition");
                if (transition.Condition())
                {
                    //Debug.Log("State Transition!");
                    TransiteTo(transition);
                    break;
                }
            }
        }

        private void TransiteTo(ITransition<TState> transition)
        {
            Transiting = true;
            ActiveTransition = transition;

            // 当前的状态立即退出
            GetState(CurrentState).Exit();
            ActiveTransition.Begin();
        }

        void LateUpdate()
        {
            if (ProcessTime is not ProcessTime.LateUpdate) return;
            CurrentProcessTime = ProcessTime.LateUpdate;


            DoProcess();
        }

        private void OnAnimatorMove()
        {
            if ((ProcessTime is not ProcessTime.AnimationMove
                    && ProcessTime is not ProcessTime.UpdateAndAnimationMove)) return;
            CurrentProcessTime = ProcessTime.AnimationMove;


            DoProcess();
        }

        protected virtual void DoProcess()
        {
            if (Transiting)
            {
                TransitionState();
            }
            else
            {

                GetState(CurrentState).Process();
            }

        }

        public abstract bool CanHandle(IInstruction instruction);
        /// <summary>
        /// 在这里和每帧的Update处理状态的迁移
        /// </summary>
        /// <param name="instruction"></param>
        public abstract void Handle(IInstruction instruction);

        public bool TryHande(IInstruction instruction)
        {
            if (CanHandle(instruction))
            {
                Handle(instruction);
                return true;
            }
            return false;
        }

    }

}