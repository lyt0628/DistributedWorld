

using Tomlet.Models;

namespace QS.Skill.SimpleSkill
{
    /// <summary>
    /// ��C#�Ќ��F����̎�����Џ������ļ��xȡ�����ę���,
    /// �@�Y�cTomlet ����ˣ��c�ⲿ��������ˣ�
    /// �mȻ��ϲ�g�����������ļ��ĸ�ʽ������Ҳ��
    /// ��׃��һ���֣�
    /// </summary>
     interface ISimpleSkillConfigurationParser
    {
        void OnParseConfiguration(ISimpleSkill skill, TomlTable skillTable);

    }
}