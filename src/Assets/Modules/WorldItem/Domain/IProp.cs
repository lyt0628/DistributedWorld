

using QS.Api.WorldItem.Domain;

namespace QS.WorldItem
{
    /// <summary>
    /// 道具总是可使用的，具体的详细设置后面再细化
    /// </summary>
    public interface IProp : IItem
    {
        bool isDepletable { get; }
        void Use();
    }
}