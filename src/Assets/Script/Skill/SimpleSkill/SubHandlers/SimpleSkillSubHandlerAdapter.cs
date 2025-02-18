
using QS.Api.Skill.Domain;
using QS.Chara.Domain;
using QS.Skill.SimpleSkill;
using Tomlet.Models;

namespace QS.Skill.Domain.Handler
{
    /// <summary>
    /// 當技能腳本使用C#實現的時候，允許它使用 Toml 
    /// 進行配置
    /// 對外只要暴露怎麼使用即可
    /// </summary>
     abstract class SimpleSkillSubHandlerAdapter 
        : ISimpleSkillSubHandler
    {

        public virtual void OnCasting(Character chara, ISimpleSkillAbility handler)
        {
        }

        public virtual void OnPostcast(Character chara, ISimpleSkillAbility handler)
        {
        }

        public virtual void OnPrecast(Character chara, ISimpleSkillAbility handler)
        {
        }

        public virtual void OnParseConfiguration(ISimpleSkill skill, TomlTable skillTable)
        {
        }

        public virtual void PreLoad(Character chara, ISimpleSkillAbility handler)
        {
          
        }

        public virtual void OnInstructed(Character chara, ISimpleSkillAbility handler)
        {
        }

        public virtual void OnCancel(Character chara, ISimpleSkillAbility handler)
        {
        }

        public virtual void OnShutdown(Character chara, ISimpleSkillAbility handler)
        {
           
        }
    }
}   