


namespace QS.Combat
{
    /// <summary>
    /// 一号技能，表示轻攻击
    /// </summary>
    class Skill01 : ISkill
    {
        public Skill01()
        {
            Code = new SkillCode(001, "LightAttack");
        }

        public SkillCode Code { get; }
    }
}