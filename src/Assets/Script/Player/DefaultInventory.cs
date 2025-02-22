


using QS.Api.WorldItem.Domain;
using QS.GameLib.Util;
using QS.WorldItem;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QS.Player
{
    class DefaultInventory : IInventory
    {
        readonly List<IWeapon> weapons = new();
        readonly List<IProp> props = new();

        public void Add(IItem item)
        {
            switch (item)
            {
                case IWeapon weapon:
                    weapons.Add(weapon);
                    break;
                case IProp prop:
                    props.Add(prop);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public bool Contains(IItem item)
        {
            return item switch
            {
                IWeapon weapon => weapons.Contains(weapon),
                IProp prop => props.Contains(prop),
                _ => throw new NotImplementedException(),
            };
        }

        public IProp[] GetProps()
        {
            return props.ToArray();
        }

        public IWeapon[] GetWeapons()
        {
            return weapons.ToArray();
        }

        public bool Remove(IItem item)
        {
            return item switch
            {
                IWeapon weapon => weapons.Remove(weapon),
                IProp prop =>props.Remove(prop),
                _ => throw new NotImplementedException(),
            };
        }
    }
}