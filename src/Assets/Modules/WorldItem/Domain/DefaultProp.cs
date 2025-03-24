using QS.Api.WorldItem.Domain;
using UnityEngine;

namespace QS.WorldItem
{
    class DefaultProp : IProp
    {

        readonly IProp flyweight;

        public DefaultProp(string uuid, IProp flyweight)
        {
            this.uuid = uuid;
            this.flyweight = flyweight;
        }

        public string uuid { get; }

        public bool isDepletable => flyweight.isDepletable;

        public string name => flyweight.name;

        public ItemType type => flyweight.type;

        public Sprite image => flyweight.image;

        public GameObject prefab => flyweight.prefab;

        public string desc => flyweight.desc;

        public void Use()
        {
            Debug.Log($"Item {name} used!!!");
        }

    }
}