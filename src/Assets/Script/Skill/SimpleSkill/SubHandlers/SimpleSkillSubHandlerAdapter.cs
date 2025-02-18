
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