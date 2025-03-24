

namespace QS.Agent
{
    public static class AgentConstants
    {

        /// <summary>
        /// 行动立即退出，不执行下一行动
        /// </summary>
        public const float ACT_SPAN_IMMEDIATE = 0f;
        /// <summary>
        /// 行动立即退出，退出后立即执行下一个行动
        /// </summary>
        public const float ACT_SPAN_NONBLOCK = -1f;

    }
}