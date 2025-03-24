

namespace QS.Combat
{
    /// <summary>
    /// 技能，这个技能是战斗意义上的技能，不做储存表现使用
    /// </summary>
    public interface ISkill
    {
        SkillCode Code { get; }
    }
}