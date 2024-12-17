using QS.Api.Skill.Domain;
using QS.Api.Skill.Predefine;

namespace QS.Skill.Conf
{
    class SimpleSkillAnimCfg : ISimpleSkillAnimCfg
    {
        public string GetMsg(ISkillKey key, SimpleSkillStage stage)
        {
            return string.Format(ISimpleSkillAnimCfg.MSG_Format, key.No, key.Name, stage);
        }

        public string GetTrigger(ISkillKey key)
        {
            return string.Format(ISimpleSkillAnimCfg.Trigger_Format, key.No, key.Name);
        }
    }
}