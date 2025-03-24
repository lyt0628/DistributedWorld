


using QS.Skill.Domain;

namespace QS.Api.Skill.Domain
{
    /// <summary>
    /// The Unique address for a Skill
    /// �F�ڼ���߀�]�и������YԴ�Y�ϣ������I���ӣ��@���YԴ��ԓ�ɼ��ܱ���@ȡ
    /// ���ܱ����ṩ���F�ڣ��҂���Ҫ�_�J���ܵ�����Ҫʲ�N�YԴ��
    /// �҂��ļ���ϵ�y���ɄӮ��ӵģ����ԄӮ�����ҕ�������YԴ��
    /// ��Ч��Prefab������ARPG�Пo���@�׷N�����Ȳ����]ͬ�ڄӮ���ͬ�ڄӮ���������һ���~���A�Σ�
    /// ǰ���ߣ����ҵ�λ�Ì������ͺ��ˣ������ҵ���ײ�w���������ϲ��������ɡ��@Щ߉݋
    /// �����Լ�̎�������ҵİ�Chara�o������
    /// Ȼ�ᣬ�����أ�������ʹ�ý^���ǌ�춼��ܵĹ�������˼��ܑ�ԓ��ه���wģ�K��
    /// 
    /// Skill ����Chara �ϵ�������
    /// </summary>
    public interface ISkillKey
    {
        /// <summary>
        /// The predefined No of Skill.
        /// </summary>
        string No { get; }

        /// <summary>
        /// The name of Skill
        /// </summary>
        string Name { get; }

        string Patch { get; }

        public static ISkillKey New(string no, string name, string patch)
        {
            return new SkillKey(no, name, patch);
        }

        public static ISkillKey New(string no, string name)
        {
            return new SkillKey(no, name);
        }

    }
}