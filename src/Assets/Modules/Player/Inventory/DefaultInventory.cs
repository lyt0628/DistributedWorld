


using QS.Api.WorldItem.Domain;
using QS.WorldItem;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QS.Player
{
    /// <summary>
    /// 一个对物品的引用表示一个物体，对一个物体的多次引用表示持有多个，这个数据的物体
    /// </summary>
    class DefaultInventory : IInventory
    {
        readonly List<IWeapon> weapons = new();
        readonly List<IProp> props = new();
        readonly List<INote> notes = new();
        readonly List<IMaterial> materials = new();

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
                case INote note:
                    notes.Add(note);
                    break;
                case IMaterial mat:
                    materials.Add(mat);
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

        public IItem Get(string uuid)
        {
            var items = new List<IItem>();
            items.AddRange(weapons);
            items.AddRange(props);
            items.AddRange(notes);
            items.AddRange(materials);
            return items.First(i => i.uuid == uuid);
        }

        public IMaterial[] GetMaterials()
        {
            return materials.ToArray();
        }

        public INote[] GetNotes()
        {
            return notes.ToArray();
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
                IProp prop => props.Remove(prop),
                _ => throw new NotImplementedException(),
            };
        }
    }
}