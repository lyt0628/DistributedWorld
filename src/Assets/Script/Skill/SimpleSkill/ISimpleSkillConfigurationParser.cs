

using Tomlet.Models;

namespace QS.Skill.SimpleSkill
{
    /// <summary>
    /// 在C#中實現的子處理器有從配置文件讀取數據的權利,
    /// 這裏與Tomlet 耦合了，與外部構件耦合了，
    /// 雖然不喜歡，但是配置文件的格式，本身也是
    /// 不變的一部分，
    /// </summary>
     interface ISimpleSkillConfigurationParser
    {
        void OnParseConfiguration(ISimpleSkill skill, TomlTable skillTable);

    }
}