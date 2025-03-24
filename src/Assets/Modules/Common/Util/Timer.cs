
using GameLib.DI;
using QS.Api;
using UnityEngine;
using UnityEngine.Events;

namespace QS.Common.Util
{

    [Scope(Value = ScopeFlag.Prototype)]
    class Timer : ITimer
    {

        public bool Executing { get; private set; } = false;
        public UnityEvent OnTick { get; } = new();
        float m_StartTime = 0f;
        float m_accTime = 0f;
        float m_Delay = 0f;
        float m_Interval = 0f;
        [Injected]
        readonly ILifecycleProivder life;

        bool started = false;


        public void Set(float interval = 1, float delay = 0)
        {
            life.UpdateAction.AddListener(Tick);
            m_StartTime = Time.time;
            m_Delay = delay;
            m_Interval = interval;
        }

        public void Clear()
        {
            started = false;
            life.UpdateAction.RemoveListener(Tick);
        }

        void Tick()
        {
            m_accTime += Time.deltaTime;
            if (!started)
            {
                if (Time.time - m_StartTime > m_Delay)
                {
                    started = true;
                    OnTick?.Invoke();
                    m_accTime = Time.time - m_StartTime;
                }
            }
            else
            {
                if (m_accTime > m_Interval)
                {
                    m_accTime = m_accTime - m_Interval;
                    OnTick?.Invoke();
                }
            }
        }
    }
}