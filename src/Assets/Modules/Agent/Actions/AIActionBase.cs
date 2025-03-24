


using UnityEngine;

namespace QS.Agent
{
    public abstract class AIActionBase : IAIAction
    {
        protected AIActionBase()
        {
            CreatedTime = Time.time;
        }

        public float CreatedTime { get; }
        public virtual float MaxWaitTime => float.PositiveInfinity;
        public float ExecutedTime { get; private set; }
        public bool HasExecuted { get; private set; } = false;
        public virtual bool IsValid => true;
        public abstract float Span { get; }
        public abstract int Priority { get; }

        public int ExecutedFrame { get; private set; } = 0;

        public virtual void Enter()
        {
            ExecutedTime = Time.time;
            HasExecuted = true;
        }

        public virtual void Exit()
        {

        }
        public virtual void Process()
        {
            ExecutedFrame++;
        }
    }
}