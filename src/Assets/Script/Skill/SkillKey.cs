

using QS.Api.Skill.Domain;

namespace QS.Skill.Domain
{
    class SkillKey : ISkillKey
    {
        public SkillKey(string no, string name) 
        {
            No = no;
            Name = name;
            Patch = "";
        }

        public SkillKey(string no, string name, string patch) 
        {
            No = no;
            Name = name;
            Patch = patch;
        }

        public string No { get; }

        public string Name { get; }

        public string Patch { get; }
        public override int GetHashCode()
        {
            return No.GetHashCode();
        }
        // 實際上，用來判斷是否相等的只使用 NO
        // 其他的要不要加再看情況
        public override bool Equals(object obj)
        {
            if (obj is ISkillKey key)
            {
                return No == key.No;
            }
            return false;
        }
    }
}