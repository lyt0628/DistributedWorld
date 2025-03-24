using GameLib.DI;
using QS.Api.WorldItem.Domain;
using QS.Api.WorldItem.Service;
using QS.GameLib.Util;
using QS.WorldItem;
using QS.WorldItem.Domain;
using QS.WorldItem.Repo.ActiveRecord;



namespace QS.WorldItem.Service
{

    /// <summary>
    /// �кܶ���������,����һ�������, ���� ��Щ����Ļ��¼ֱ�ӱ�¶
    /// ��ȥ, 
    /// </summary>
    public class WorldItemService : IWorldItemService

    {

        [Injected]
        readonly WorldWeaponRepo weaponRepo;


        public T GetItemProto<T>(string name) where T : class, IItem
        {
            if (ReflectionUtil.IsParentOf<Weapon, T>())
            {
                return GetWeaponProto(name) as T;
            }
            throw new System.NotImplementedException();
        }

        Item GetWeaponProto(string name)
        {
            var i = weaponRepo
                    .Find(w => w.Breed.Name == name)
                    .Unwrap()
                    .Clone();
            i.UUID = MathUtil.UUID();
            // �õ�һ������ԭ��,�͸�������һ���µ�UUID��ʾ������
            // �����һ������,����Ĳ��ɸ�������, ��ֱ�ӷ��� ԭ�ͼ���
            // ���ɸ�������, ����Ӻ���޷��ı���, ��Ӧֻ��¶��ֻ���ӿ�
            // �����ڳ����ڲ�, ����, û��
            return i;
        }
    }
}


