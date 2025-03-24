

using QS.Api.WorldItem.Domain;
using Tomlet.Attributes;
using UnityEngine;

namespace QS.WorldItem
{
    public class PropFlyWeight : IProp
    {
        public PropFlyWeight(bool isDepletable, string name, string image, string prefab, string desc)
        {
            this.isDepletable = isDepletable;
            this.name = name;
            this.imageAddress = image;
            this.prefabAddress = prefab;
            this.desc = desc;
        }


        public ItemType type => ItemType.Prop;

        public bool isDepletable { get; private set; }

        public string name { get; private set; }

        [TomlProperty(mapFrom: "image")]
        internal string imageAddress { get; set; }
        [TomlNonSerialized]
        public Sprite image { get; internal set; }
        [TomlProperty(mapFrom: "prefab")]
        internal string prefabAddress { get; set; }
        [TomlNonSerialized]
        public GameObject prefab { get; internal set; }

        public string desc { get; private set; }
        public string uuid => throw new System.InvalidOperationException();
        public void Use() => throw new System.InvalidOperationException();
    }
}