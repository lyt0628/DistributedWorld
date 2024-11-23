


using QS.Api.Data;
using QS.Domain.Combat;
using QS.Domain.Item;

namespace QS.Impl.Data
{
    public class WorldItemData : IWorldItemData
    {
        private readonly IItem item1;
        public WorldItemData()
        {

            var ib = new ItemBreed
            {
                Name = "Rust Sword",
                Prefab = "Sword.prefab",
                Description = "Rust Sword",
                Img = "RustSword.png"
            };
            var wb = new SimpleWeaponBread(ib)
            {
                MainBuff = new AttackBuff(0.3f, 0)
            };

            item1 = new SimpleWeaon(GameConstants.PROTO_ITEM, wb);
        }

        public IItem Find(string name)
        {
            return item1;
        }
    }
}
