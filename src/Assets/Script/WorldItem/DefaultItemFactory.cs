using GameLib.DI;
using QS.Api.WorldItem;
using QS.Api.WorldItem.Domain;
using QS.Api.WorldItem.Service;
using QS.GameLib.Util;
using QS.WorldItem;
using QS.WorldItem.Domain;



namespace QS.WorldItem.Service
{

    /// <summary>
    /// �кܶ���������,����һ�������, ���� ��Щ����Ļ��¼ֱ�ӱ�¶
    /// ��ȥ, 
    /// </summary>
    public class DefaultItemFactory : IItemFactory
    {
        [Injected]
        IItemBreedRepo breedRepo;

        public IProp CreateProp(string name)
        {
            var pb = breedRepo.GetItemBreed<IPropBreed>(name);
            var p = new DefaultProp(pb, MathUtil.UUID());
            return p;
        }

        public IWeapon CreateWeapon(string name)
        {
            var wb = breedRepo.GetItemBreed<IWeaponBreed>(name);
            var w = new DefaultWeapon(wb, MathUtil.UUID());
            return w;
        }
    }
}


