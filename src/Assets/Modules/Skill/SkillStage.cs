

namespace QS.Api.Skill.Domain
{
    /// <summary>
    /// 一个技能总归就这几个阶段，
    /// 即便是分段技能，做为其某一阶段也是合理的。
    /// </summary>
    public enum SkillStage
    {
        /// <summary>
        /// 技能的前摇动画
        /// </summary>
        Precast,
        /// <summary>
        /// 技能发动，表示开始进行伤害检测
        /// </summary>
        Casting,
        /// <summary>
        /// 技能后摇，表示伤害计算结束
        /// </summary>
        Postcast,
        /// <summary>
        /// 表示技能结束，连续技不可接上，并且进入技能僵直阶段
        /// </summary>
        Shutdown,
        /// <summary>
        /// 已经回复，表示动画可以流畅的接上一般控制动画了
        /// </summary>
        Recoveried
    }

}
