namespace QS.Agent
{
    /// <summary>
    /// AI 的行为被抽象为一个 行动。
    /// 行动有ID， 但是是聚合根AI角色下的一个ID
    /// </summary>
    public interface IAIAction
    {
        /// <summary>
        /// 行动总时长
        /// </summary>
        float Span { get; }
        /// <summary>
        /// 行动的优先级
        /// </summary>
        int Priority { get; }
        /// <summary>
        /// 到了即将被执行时候，行动还是否有效
        /// </summary>
        bool IsValid { get; }
        /// <summary>
        /// Action 被创建的时间 
        /// </summary>
        float CreatedTime { get; }
        /// <summary>
        /// Action 等待执行的最大时间
        /// </summary>
        float MaxWaitTime { get; }
        /// <summary>
        /// 行动开始被执行的时间
        /// </summary>
        float ExecutedTime { get; }
        int ExecutedFrame { get; }
        /// <summary>
        /// 行动是否已经开始执行
        /// </summary>
        bool HasExecuted { get; }



        /// <summary>
        /// 行动进入，触发行动。一般是发送技能指令
        /// </summary>
        void Enter();
        /// <summary>
        /// 行动持续，会在此发送移动指令
        /// </summary>
        void Process();
        /// <summary>
        /// 行动结束
        /// </summary>
        void Exit();

        public static IAIAction Unit { get; } = new UnitAction();

        class UnitAction : IAIAction
        {
            public float Span => 0f;
            public int Priority => int.MaxValue;
            public bool IsValid => false;
            public float CreatedTime => 0f;
            public float MaxWaitTime => 0f;
            public float ExecutedTime => float.MinValue;
            public bool HasExecuted => true;
            public int ExecutedFrame => int.MaxValue;
            public void Enter()
            {
            }

            public void Exit()
            {
            }

            public void Process()
            {
            }
        }
    }
}