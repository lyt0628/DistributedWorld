namespace QS.Agent
{
    /// <summary>
    /// AI ����Ϊ������Ϊһ�� �ж���
    /// �ж���ID�� �����Ǿۺϸ�AI��ɫ�µ�һ��ID
    /// </summary>
    public interface IAIAction
    {
        /// <summary>
        /// �ж���ʱ��
        /// </summary>
        float Span { get; }
        /// <summary>
        /// �ж������ȼ�
        /// </summary>
        int Priority { get; }
        /// <summary>
        /// ���˼�����ִ��ʱ���ж����Ƿ���Ч
        /// </summary>
        bool IsValid { get; }
        /// <summary>
        /// Action ��������ʱ�� 
        /// </summary>
        float CreatedTime { get; }
        /// <summary>
        /// Action �ȴ�ִ�е����ʱ��
        /// </summary>
        float MaxWaitTime { get; }
        /// <summary>
        /// �ж���ʼ��ִ�е�ʱ��
        /// </summary>
        float ExecutedTime { get; }
        int ExecutedFrame { get; }
        /// <summary>
        /// �ж��Ƿ��Ѿ���ʼִ��
        /// </summary>
        bool HasExecuted { get; }



        /// <summary>
        /// �ж����룬�����ж���һ���Ƿ��ͼ���ָ��
        /// </summary>
        void Enter();
        /// <summary>
        /// �ж����������ڴ˷����ƶ�ָ��
        /// </summary>
        void Process();
        /// <summary>
        /// �ж�����
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