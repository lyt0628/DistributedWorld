using QS.Api.Combat.Domain;
using QS.Api.WorldItem.Domain;

namespace QS.WorldItem.Domain
{
    /// <summary>
    /// 武器經典的 有主詞條和副詞條，
    /// 主詞條是設計好的，副詞絛可以洗練修改
    /// 這些切換都是領域邏輯，這邊按照 ECS 模式是最好的選擇了 
    /// </summary>
    class DefaultWeapon
        : BaseItem, IWeapon
    {
        public DefaultWeapon(IWeaponBreed breed, string uuid) : base(breed, uuid)
        {
            MainBuff = breed.MainBuff;
            Exp = 0;
        }

        public float Exp { get; private set; }

        public IBuff MainBuff { get; }

        public void Refine(float exp)
        {
            Exp += exp;
        }


    }
}