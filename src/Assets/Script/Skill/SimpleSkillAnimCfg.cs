using QS.Api.Skill.Domain;
using QS.Api.Skill.Predefine;
using UnityEngine;


namespace QS.Skill.Conf
{
    class SimpleSkillAnimCfg : ISimpleSkillAnimCfg
    {
        public string GetMsg(ISkillKey key, SimpleSkillStage stage)
        {
            var msg = string.Format(ISimpleSkillAnimCfg.MSG_Format, key.No, key.Name, stage);
            Debug.Log(msg);
            return msg;
        }

        public string GetAnimTrigger(ISkillKey key)
        {
            return string.Format(ISimpleSkillAnimCfg.Trigger_Format, key.No, key.Name);
        }
    }
}