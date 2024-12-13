

namespace QS.Api.Skill.Domain
{
    /// <summary>
    /// Skill ����Ϊ����Դ���屻����Ϊ��ȫ����ص�����ʵ��, ��WorldItem��ȫ��ͬ,
    /// SkillAsset, �����õ�,Ԥ�����, ��WorldItem һ��, �ŵ�����ĵ��������й���, 
    /// ��˲��طŵ� Skill ���ڲ�����, ��Ϊ����ͨ�� <see cref="ISkillKey"/> �����ݲ�
    /// ���в���, ����ÿ����Դ��ʲô, �ű��Լ�֪��, �����Ԥ��Լ���õ�
    /// </summary>
    public interface ISkillAsset
    {
        ISkillKey Key { get; }
    }
}