using QS.Api.Combat.Domain;
using QS.Api.WorldItem.Domain;

namespace QS.WorldItem.Domain
{
    /// <summary>
    /// ��������� �����~�l�͸��~�l��
    /// ���~�l���OӋ�õģ����~�d����ϴ���޸�
    /// �@Щ�ГQ�����I��߉݋���@߅���� ECS ģʽ����õ��x���� 
    /// </summary>
    class DefaultWeapon
        : BaseItem, IWeapon
    {
        public DefaultWeapon(IWeaponBreed breed, string uuid) : base(breed, uuid)
        {
            MainBuff = breed.MainBuff;
            Exp = 0;
        }

        public float Exp { get; private set; }

        public IBuff MainBuff { get; }

        public void Refine(float exp)
        {
            Exp += exp;
        }


    }
}