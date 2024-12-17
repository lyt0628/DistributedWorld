


using QS.Skill.Domain;

namespace QS.Api.Skill.Domain
{
    /// <summary>
    /// The Unique Key for a Skill
    /// </summary>
    public interface ISkillKey
    {
        /// <summary>
        /// The predefined No of skill.
        /// </summary>
        string No { get; }

        /// <summary>
        /// The name of skill
        /// </summary>
        string Name { get; }

        string Patch { get; }

        public static ISkillKey New(string no, string name, string patch)
        {
            return new SkillKey(no, name, patch);
        }

        public static ISkillKey New(string no, string name)
        {
            return new SkillKey(no, name);
        }

    }
}