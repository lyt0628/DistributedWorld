using GameLib.DI;
using QS.Api.Domain.Item;
using QS.GameLib.Util;
using QS.Impl.Data.Gateway.WorldItem;
using QS.Impl.Domain.Item;



namespace QS.Impl.Data.Gateway.Facade
{

    /// <summary>
    /// 有很多物体类型,创建一个外观类, 不让 这些物体的活动记录直接暴露
    /// 出去, 
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
            // 拿到一个武器原型,就给他分配一个新的UUID表示新物体
            // 如果是一个道具,这类的不可更新物体, 就直接返回 原型即可
            // 不可更新物体, 在添加后就无法改变了, 理应只暴露出只读接口
            // 基本在程序集内部, 但是, 没法
            return i;
        }
    }
}


