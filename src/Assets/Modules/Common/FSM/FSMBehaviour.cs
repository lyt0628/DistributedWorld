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
    /// �Լ��õĶ�����Ϊ��ͺ�. 
    /// ���״̬�������Ϊ���ڴ���һЩָ���״̬��
    /// </summary>
    /// <typeparam name="TState"></typeparam>

    public abstract class FSMBehaviour<TState> : MonoBehaviour, IHandler, IFSM<TState>
        where TState : struct, Enum
    {

        public bool Transiting { get; private set; } = false;
        public ITransition<TState> ActiveTransition { get; private set; }

        /// <summary>
        /// ǿ��ִ��һ��Ǩ��
        /// </summary>
        public void ForceTransite(ITransition<TState> transition)
        {
            TransiteTo(transition);
        }
        /// <summary>
        /// ��Ҫʹ���вι���
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
        /// ʵ��ִ��״̬Ǩ�Ƶ�������һ
        /// �����ῼ��״̬Ǩ��
        /// </summary>
        /// <param name="to"></param>
        public virtual void SwitchTo(TState to)
        {
            // ����״̬����
            GetState(CurrentState).Exit();
            CurrentState = to;
            GetState(to).Enter();
        }

        /// <summary>
        /// û�������ԭ����������Mono������ֻ��Update���������ֱ��
        /// ͨ�� LifeCycle ���ṩ�������ȱ�����޷����������������
        /// ����̳���Mono������ֱ��׼��һ��������
        /// Update ����ִ�У�������ִ��״̬Ǩ��
        /// </summary>
        protected virtual void Update()
        {
            /// ���״̬Ǩ��һ����Update �׶Σ����� Ǩ�ƵĶ�����LateUpdate �׶�ִ�к�״̬��һ��ǿ���
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

            // ��ǰ��״̬�����˳�
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
        /// �������ÿ֡��Update����״̬��Ǩ��
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