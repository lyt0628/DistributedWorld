using GameLib.DI;
using QS.Api.Domain.Item;
using QS.GameLib.Util;
using QS.Impl.Data.Gateway.WorldItem;
using QS.Impl.Domain.Item;



namespace QS.Impl.Data.Gateway.Facade
{

    /// <summary>
    /// �кܶ���������,����һ�������, ���� ��Щ����Ļ��¼ֱ�ӱ�¶
    /// ��ȥ, 
    /// </summary>
    public class WorldItemRepoFacade
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

        private Item GetWeaponProto(string name)
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


