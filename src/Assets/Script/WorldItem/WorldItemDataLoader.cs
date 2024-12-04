using GameLib.DI;
using QS.GameLib.Util;
using QS.WorldItem.Repo.ActiveRecord;

namespace QS.Impl.Data.Loader
{
    /// <summary>
    /// 数据加载器, 负责在游戏一开始加载
    /// WorldItemBreed和WorldItem
    /// </summary>
    internal class WorldItemDataLoader
    {
        readonly WeaponBreedRepo weaponBreedRepo;
        readonly WorldWeaponRepo weaponRepo;

        [Injected]
        public WorldItemDataLoader(WeaponBreedRepo wbRepo,
                            WorldWeaponRepo wRepo)
        {
            weaponBreedRepo = wbRepo;
            weaponRepo = wRepo;

            // 导入物体类型
            var wbr = weaponBreedRepo.Create();
            wbr.Name = "Rust Sword";
            wbr.Img = "Rust.png";
            wbr.Prefab = "Rust.prefab";
            wbr.Description = "Rust Sword";
            wbr.Save();

            // 导入原型物体
            var wb = weaponBreedRepo.Find((r) => r.Name == "Rust Sword");
            var wr = weaponRepo.Create();
            wr.UUID = MathUtil.UUID();
            wr.Exp = 0;
            wr.Breed = wb.Unwrap();

            wr.Save();
        }

    }
}
