

using System;

namespace QS.Combat
{
    /// <summary>
    /// ��ʶһ�����ܣ�û�еȼ�֮��ĸ���
    /// </summary>
    public class SkillCode
    {
        public SkillCode(int id, string name)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public int Id { get; }
        public string Name { get; }

    }
}