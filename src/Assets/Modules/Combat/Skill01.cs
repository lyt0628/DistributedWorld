


namespace QS.Combat
{
    /// <summary>
    /// һ�ż��ܣ���ʾ�ṥ��
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