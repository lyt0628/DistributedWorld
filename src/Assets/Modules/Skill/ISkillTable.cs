

using QS.Api.Executor.Domain;
using QS.Api.Skill.Domain;
using QS.Common;
using QS.Common.FSM;

namespace QS.Skill
{
    /// <summary>
    /// 技能表, 比如说，Katana状态下的技能，和Bow状态下的技能，这是完全不一样的。
    /// </summary>
    public interface ISkillTable : IHandler
    {
        void AddSkill(IFSM<SkillStage> skill);
        void RemoveSkill(IFSM<SkillStage> skill);
    }
}

