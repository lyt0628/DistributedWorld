using GameLib.DI;
using QS.Api.Domain.Item;
using QS.GameLib.Pattern.Message;
using QS.GameLib.Util;
using QS.Impl.Data.Gateway.PlayerItem;
using QS.Impl.Domain.Item;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QS.Impl.Data.Gateway.Facade
{

    /// <summary>
    /// 外观类比较稳定, 可以放到接口
    /// 
    /// </summary>
    public class PlayerItemRepofacade
    {
        readonly WorldItemRepoFacade worldItems;

        readonly PlayerItemRepo playerItems;

        [Injected]
        public PlayerItemRepofacade(WorldItemRepoFacade worldItems,
                                    PlayerItemRepo playerItems)
        {
            this.worldItems = worldItems;
            this.playerItems = playerItems;

            playerItems.Messager.AddListener(PlayerItemMsgs.ITEM_UPDATED, msg =>
            {
                var i = ((Msg1<IItem>)msg).Value;
                OnItemUpdated?.Invoke(i);
            });
            // 消息比较灵活,但是相对的没有静态类型检查,容易出错,在稳定的地方
            // 尽量不要使用
            // Flow APi 一般用于1to1 的情况,要不就得记录订阅了
            // 然而外观并不记录变化, 数据的变化也要求实时响应
            // 所以也不适合使用响应式编程
        }


        /// <summary>
        /// 通过物品名称来添加物体, 
        /// </summary>
        /// <param uuid="itemName"></param>
        public void AddItem(string itemName)
        {
            var item = worldItems.GetItemProto<Item>(itemName);
            var r = playerItems.Create();
            r.Wrap(item);
            r.Save();
        }

        event Action<IItem> OnItemUpdated;
        public void AddListenerForItemUpdated(Action<IItem> action)
        {
            OnItemUpdated += action;
        }
        public void RemoveListenerForItemUpdated(Action<IItem> action)
        {
            OnItemUpdated -= action;
        }


        public T GetItem<T>(string uuid) where T : class, IItem
        {
            var r = playerItems.Find(r => r.UUID == uuid);
            var i = r.Unwrap();

            if (ReflectionUtil.IsChildOf<T>(i))
            {
                return i as T;
            }
            throw new System.NotImplementedException();
        }

        public List<T> GetItems<T>() where T : class, IItem
        {
            var items = playerItems
                    .Where(r => ReflectionUtil.IsChildOf<T>(r))
                    .Select(r => r.Unwrap() as T)
                    .ToList();
            return items;
        }

        internal void UpdateItem(Item item)  {

           var r = playerItems.Create();
            r.Wrap(item);
            r.Update();
        }

    }

}
