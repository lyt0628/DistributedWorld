using QS.Api.Combat.Domain;

namespace QS.WorldItem.Domain
{
    /// <summary>
    /// The weapon Defferent fields with item.
    /// and used as a flyWeight definition 
    /// 
    /// 即便是不同武器实例也有相同的属性因此需要Breed，如果没有的话？？？
    /// No，不大可能这样子，至少UI属性是共通的，
    /// 即便升级后需要更换UI，这也应当是而外的领域模型了
    /// 比如说一个ItemRankBreed 的包装类
    /// </summary>
    public interface IWeaponBreed : IItemBreed
    {
        IBuff MainBuff { get; }
    }
}