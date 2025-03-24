using QS.Api.WorldItem.Domain;
using QS.Combat;
using Tomlet.Attributes;
using UnityEngine;

namespace QS.WorldItem
{
    class WeaponFlyweight : IWeapon
    {
        public WeaponFlyweight(IBuff mainBuff, string name, string image, string prefab, string desc)
        {
            this.mainBuff = mainBuff;
            this.name = name;
            this.imageAddress = image;
            this.prefabAddress = prefab;
            this.desc = desc;
        }

        public IBuff mainBuff { get; private set; }

        public string name { get; private set; }
        public ItemType type => ItemType.Weapon;

        [TomlProperty(mapFrom: "image")]
        internal string imageAddress { get; private set; }
        [TomlNonSerialized]
        public Sprite image { get; internal set; }
        [TomlProperty(mapFrom: "prefab")]
        internal string prefabAddress { get; private set; }
        [TomlNonSerialized]
        public GameObject prefab { get; internal set; }
        public string desc { get; private set; }

        public float exp => throw new System.InvalidOperationException();
        public string uuid => throw new System.InvalidOperationException();

        public void Refine(float exp) => throw new System.InvalidOperationException();
    }
}