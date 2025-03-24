
using QS.Api.Skill.Domain;
using QS.Chara.Domain;
using QS.Skill.SimpleSkill;
using Tomlet.Models;

namespace QS.Skill.Domain.Handler
{
    /// <summary>
    /// �������_��ʹ��C#���F�ĕr�����S��ʹ�� Toml 
    /// �M������
    /// ����ֻҪ��¶���Nʹ�ü���
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