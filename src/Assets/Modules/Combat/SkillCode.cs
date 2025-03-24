

using System;

namespace QS.Combat
{
    /// <summary>
    /// 标识一个技能，没有等级之类的概念
    /// </summary>
    public class SkillCode
    {
        public SkillCode(int id, string name)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public int Id { get; }
        public string Name { get; }

    }
}