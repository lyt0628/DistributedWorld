
using QS.Api.WorldItem.Domain;
using QS.Combat;
using UnityEngine;

namespace QS.WorldItem.Domain
{
    /// <summary>
    /// 武器經典的 有主詞條和副詞條，
    /// 主詞條是設計好的，副詞絛可以洗練修改
    /// 這些切換都是領域邏輯，這邊按照 ECS 模式是最好的選擇了 
    /// </summary>
    class DefaultWeapon : IWeapon
    {
        readonly IWeapon flyweight;

        public DefaultWeapon(string uuid, IWeapon flyweight)
        {
            this.flyweight = flyweight;
            this.uuid = uuid;
        }

        public DefaultWeapon(string uuid, IWeapon flyweight, float exp) : this(uuid, flyweight)
        {
            this.exp = exp;
        }

        public float exp { get; private set; } = 0f;

        public IBuff mainBuff => flyweight.mainBuff;

        public string uuid { get; }

        public string name => flyweight.name;

        public ItemType type => flyweight.type;

        public Sprite image => flyweight.image;

        public GameObject prefab => flyweight.prefab;

        public string desc => flyweight.desc;

        public void Refine(float exp)
        {
            this.exp += exp;
        }
    }
}