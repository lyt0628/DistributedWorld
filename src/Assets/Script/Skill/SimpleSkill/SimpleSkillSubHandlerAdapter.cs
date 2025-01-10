
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
        public virtual void OnCastingExit(Character chara, ISimpleSkillHandler handler)
        {
        }

        public virtual void OnCastingEnter(Character chara, ISimpleSkillHandler handler)
        {
        }

        public virtual void OnPostcastEnter(Character chara, ISimpleSkillHandler handler)
        {
        }

        public virtual void OnPostcastExit(Character chara, ISimpleSkillHandler handler)
        {
        }

        public virtual void OnPrecastEnter(Character chara, ISimpleSkillHandler handler)
        {
        }

        public virtual void OnPrecastExit(Character chara, ISimpleSkillHandler handler)
        {
        }

        public virtual void OnParseConfiguration(ISimpleSkill skill, TomlTable skillTable)
        {
        }

        public virtual void PreLoad(Character chara, ISimpleSkillHandler handler)
        {
          
        }
    }
}   