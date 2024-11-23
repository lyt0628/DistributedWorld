


using GameLib.DI;
using QS.Api.Data;
using QS.Api.Service;
using QS.Domain.Item;
using System;

namespace QS.Impl.Data
{
    public class WeaponRefineService : IWeaponRefineService
    {
        [Injected]
        readonly IInventoryData inventory;

        public void Refine(IItem weapon, float exp)
        {

            if (weapon == null)
            {
                new InvalidOperationException("Weapon to be refined cannot be null");
            }
            else if (weapon is not IWeapon)
            {
                new InvalidOperationException("Item to be refined is not a Weapon: " + weapon); ;
            }
            IWeapon w = weapon as IWeapon;
            if (weapon.UUID == "")
            {
                // 没有在背包的物体是不能被精炼的
                inventory.Remove(weapon);
                w = weapon.Clone() as IWeapon;
            }

            w.Refine(exp);

            if (weapon.UUID == "")
            {
                inventory.Add(w);
            }
        }
    }
}