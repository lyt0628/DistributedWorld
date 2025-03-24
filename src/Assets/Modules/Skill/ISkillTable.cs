

using QS.Api.Executor.Domain;
using QS.Api.Skill.Domain;
using QS.Common;
using QS.Common.FSM;

namespace QS.Skill
{
    /// <summary>
    /// ���ܱ�, ����˵��Katana״̬�µļ��ܣ���Bow״̬�µļ��ܣ�������ȫ��һ���ġ�
    /// </summary>
    public interface ISkillTable : IHandler
    {
        void AddSkill(IFSM<SkillStage> skill);
        void RemoveSkill(IFSM<SkillStage> skill);
    }
}

