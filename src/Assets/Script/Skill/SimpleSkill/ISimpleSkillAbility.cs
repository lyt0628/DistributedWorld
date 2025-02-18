

using QS.Api.Executor.Domain;
using QS.GameLib.Pattern.Pipeline;
using QS.Skill.SimpleSkill;
using System;

namespace QS.Api.Skill.Domain
{
    /// <summary>
    /// ���������ļ��Y���x�ģ�SimpleSkill�������@�NһЩ����
    /// 1. �|�l�Ӯ���DefaultSimpleSkill �ǻ�춄Ӯ����{�ģ��@�������ǻ��A������̎��������
    /// 2. �ڼ��ܵĲ�ͬ�A�Ό������A�u�w�����d�c��
    /// 3. �ڼ��ܵĲ�ͬ�A�β�����Ч
    /// 4. �ڼ��ܵĲ�ͬ�A���{��Lua�_��
    /// 5. 
    /// �@Щ�����������ṩһ��̎��������
    /// </summary>
     interface ISimpleSkillAbility : IInstructionHandler
    {
        ISimpleSkill Skill { get; }
        SimpleSkillStage CurrentStage { get; }
        void Cast();
        void Cancel();
        void AddSubHandler(ISimpleSkillSubHandler subHandler);
        T GetSubHandler<T>() where T : ISimpleSkillSubHandler;

    }
}