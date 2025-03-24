


using QS.Api.WorldItem;
using QS.WorldItem.Domain;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace QS.WorldItem
{
    // ����һ�����εăȴ�惦����Ҫ�}�s����
    class DefaultItemBreedRepo : IItemBreedRepo
    {
        readonly List<IWeaponBreed> weaponBreeds = new();

        public void AddWeaponBreed(IWeaponBreed weaponBreed)
        {
            weaponBreeds.Add(weaponBreed);
        }

        public IWeaponBreed GetWeaponBreed(string name)
        {
      
            var result = weaponBreeds.Find(weaponBreed => weaponBreed.Name == name);
            Assert.IsNotNull(result, $"Item not found : {name} ");
            return result;
        }
    }
}