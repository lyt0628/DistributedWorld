

namespace QS.Agent
{
    /// <summary>
    /// AI 状态
    /// </summary>
    public enum AgentState
    {
        /// <summary>
        /// 没有进入战斗状态，自由活动
        /// </summary>
        Free,
        /// <summary>
        /// 主动状态，盯上了玩家，准备靠近进行攻击
        /// </summary>
        Focus,
        /// <summary>
        /// 主动状态，靠近了玩家，跟玩家拉扯
        /// </summary>
        Around,
        /// <summary>
        /// 主动状态，活跃的进行攻击
        /// </summary>
        Active,
        /// <summary>
        /// 被动状态，察觉到玩家攻击，进入防御
        /// </summary>
        Defence,
        /// <summary>
        /// 察觉到玩家附近有危险，想要远离玩家
        /// </summary>
        FarAway,
        /// <summary>
        /// 放水的阶段，允许玩家跑路，或者一开始让几招
        /// </summary>
        Pending,
    }
}