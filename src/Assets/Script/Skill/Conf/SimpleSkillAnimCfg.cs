using QS.Api.Skill.Domain;
using QS.Api.Skill.Predefine;

namespace QS.Skill.Conf
{
    class SimpleSkillAnimCfg : ISimpleSkillAnimCfg
    {
        public string GetMsg(string no, string name, SimpleSkillStage stage)
        {
            return string.Format(ISimpleSkillAnimCfg.MSG_Format, no, name, stage);
        }

        public string GetTrigger(string no, string name)
        {
            return string.Format(ISimpleSkillAnimCfg.Trigger_Format, no, name);
        }
    }
}