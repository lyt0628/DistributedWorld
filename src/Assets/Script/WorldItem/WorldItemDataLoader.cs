using GameLib.DI;
using QS.GameLib.Util;
using QS.WorldItem.Repo.ActiveRecord;

namespace QS.Impl.Data.Loader
{
    /// <summary>
    /// ���ݼ�����, ��������Ϸһ��ʼ����
    /// WorldItemBreed��WorldItem
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

            // ������������
            var wbr = weaponBreedRepo.Create();
            wbr.Name = "Rust Sword";
            wbr.Img = "Rust.png";
            wbr.Prefab = "Rust.prefab";
            wbr.Description = "Rust Sword";
            wbr.Save();

            // ����ԭ������
            var wb = weaponBreedRepo.Find((r) => r.Name == "Rust Sword");
            var wr = weaponRepo.Create();
            wr.UUID = MathUtil.UUID();
            wr.Exp = 0;
            wr.Breed = wb.Unwrap();

            wr.Save();
        }

    }
}
