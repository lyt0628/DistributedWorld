

namespace QS.Api.Skill.Domain
{
    /// <summary>
    /// Skill 的行为和资源定义被定义为完全不相关的两个实体, 与WorldItem完全不同,
    /// SkillAsset, 是配置的,预定义的, 和WorldItem 一样, 放到程序的第三方进行管理, 
    /// 因此不必放到 Skill 的内部定义, 行为程序通过 <see cref="ISkillKey"/> 到数据层
    /// 进行查找, 具体每个资源是什么, 脚本自己知道, 这个是预先约定好的
    /// </summary>
    public interface ISkillAsset
    {
        ISkillKey Key { get; }
    }
}