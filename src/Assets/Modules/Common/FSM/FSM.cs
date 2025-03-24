

using QS.Api;
using QS.Api.Executor.Domain;
using System;

namespace QS.Common.FSM
{
    public abstract class FSM<TState> : IFSM<TState> where TState : struct, Enum
    {
        protected FSM()
        {
            CurrentState = DefaultState;
            var life = CommonGlobal.Instance.GetInstance<ILifecycleProivder>();
            life.UpdateAction.AddListener(OnProcess);
        }


        ~FSM()
        {
            var life = CommonGlobal.Instance.GetInstance<ILifecycleProivder>();
            life.UpdateAction.RemoveListener(OnProcess);
        }

        public abstract TState DefaultState { get; }

        public TState CurrentState { get; protected set; }

        public bool Transiting { get; private set; } = false;

        public ITransition<TState> ActiveTransition { get; protected set; }

        /// <summary>
        /// 非Behaviour的FSM 只支持一个生命周期，Update
        /// </summary>
        void OnProcess()
        {
            if (!Transiting) CheckTransition();
            if (Transiting) TransitionState();

            if(!Transiting)
            {
                GetState(CurrentState).Process();
            }
            
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
                    Transiting = true;
                    ActiveTransition = transition;

                    // 当前的状态立即退出
                    GetState(CurrentState).Exit();
                    ActiveTransition.Begin();
                    break;
                }
            }
        }



        public abstract IState<TState> GetState(TState state);

        public virtual void SwitchTo(TState to)
        {
            GetState(CurrentState).Exit();
            CurrentState = to;
            GetState(to).Enter();
        }

        public abstract bool CanHandle(IInstruction instruction);
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