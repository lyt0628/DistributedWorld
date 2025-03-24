

using QS.Api.WorldItem.Domain;
using QS.Common;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace QS.WorldItem
{
    class WeaponFlyweightLoadOp : AsyncOpBase<IWeapon>
    {
        readonly WeaponFlyweight weapon;
        public WeaponFlyweightLoadOp(WeaponFlyweight weapon)
        {
            this.weapon = weapon;
        }

        protected override async void Execute()
        {
            var loadImg = Addressables.LoadAssetAsync<Sprite>(weapon.imageAddress);
            await loadImg.Task;
            weapon.image = loadImg.Result;
            Complete(weapon);
        }
    }
}