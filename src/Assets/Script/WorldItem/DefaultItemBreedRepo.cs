


using QS.Api.WorldItem;
using QS.GameLib.Util;
using QS.WorldItem.Domain;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;

namespace QS.WorldItem
{
    // 就是一個簡單的內存存儲，不要複雜化了
    class DefaultItemBreedRepo : IItemBreedRepo
    {
        readonly List<IWeaponBreed> weaponBreeds = new();
        readonly List<IPropBreed> propBreeds = new();
        public void AddWeaponBreed(IWeaponBreed weaponBreed)
        {
            weaponBreeds.Add(weaponBreed);
        }
        public void AddPropBreed(IPropBreed propBreed)
        {
            propBreeds.Add(propBreed);
        }

        public T GetItemBreed<T>(string name) where T : IItemBreed
        {
            IItemBreed result = default;
            if (ReflectionUtil.IsParentOf<T, IWeaponBreed>())
            {
                result = weaponBreeds.Find(w => w.Name == name);
            }
            else if (ReflectionUtil.IsParentOf<T, IPropBreed>())
            {
                result = propBreeds.Find(w => w.Name == name);
            }
            else
            {
                throw new System.NotImplementedException();
            }
            if(result == null)
            {
                throw new System.Exception();
            }
            return (T)result;

            
        }

        public string[] GetItemNames()
        {
            return weaponBreeds.Select(w=>w.Name)
                .Concat(propBreeds.Select(p=>p.Name))
                .ToArray();
        }

        public IItemBreed GetItemBreed(string name)
        {
            List<IItemBreed> breeds = new();
            breeds.AddRange(weaponBreeds);
            breeds.AddRange(propBreeds);
            return breeds.Find(b => b.Name == name);
        }
    }
}