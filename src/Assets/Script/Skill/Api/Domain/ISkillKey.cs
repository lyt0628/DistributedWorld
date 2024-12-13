


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

    }
}