
using QS.Api.WorldItem.Domain;
using QS.GameLib.Util;
using Tomlet.Attributes;
using UnityEngine;

namespace QS.WorldItem
{

    class DefaultMaterial : IMaterial
    {
        public DefaultMaterial(string uuid, string name, string image, string prefab, string desc)
        {
            this.uuid = uuid;
            this.name = name;

            this.imageAddress = image;
            this.prefabAddress = prefab;
            this.desc = desc;
        }

        public DefaultMaterial()
        {

            uuid = MathUtil.UUID();
        }

        public string uuid { get; private set; }
        public string name { get; private set; }

        public ItemType type => ItemType.Material;

        [TomlProperty(mapFrom: "image")]
        internal string imageAddress { get; set; }
        [TomlNonSerialized]
        public Sprite image { get; internal set; }
        [TomlProperty(mapFrom: "prefab")]
        internal string prefabAddress { get; set; }
        [TomlNonSerialized]
        public GameObject prefab { get; internal set; }

        public string desc { get; private set; }
    }
}