using QS.Combat;

namespace QS.Api.WorldItem.Domain
{
    /// <summary>
    /// DefaultWeapon is a item, that add buff to character
    /// 
    /// </summary>
    public interface IWeapon : IItem
    {
        float exp { get; }

        void Refine(float exp);

        IBuff mainBuff { get; }
    }

}