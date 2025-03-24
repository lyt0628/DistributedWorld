
using QS.Api.WorldItem.Domain;
using QS.Combat;
using UnityEngine;

namespace QS.WorldItem.Domain
{
    /// <summary>
    /// ��������� �����~�l�͸��~�l��
    /// ���~�l���OӋ�õģ����~�d����ϴ���޸�
    /// �@Щ�ГQ�����I��߉݋���@߅���� ECS ģʽ����õ��x���� 
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