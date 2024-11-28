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
    /// �����Ƚ��ȶ�, ���Էŵ��ӿ�
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
            // ��Ϣ�Ƚ����,������Ե�û�о�̬���ͼ��,���׳���,���ȶ��ĵط�
            // ������Ҫʹ��
            // Flow APi һ������1to1 �����,Ҫ���͵ü�¼������
            // Ȼ����۲�����¼�仯, ���ݵı仯ҲҪ��ʵʱ��Ӧ
            // ����Ҳ���ʺ�ʹ����Ӧʽ���
        }


        /// <summary>
        /// ͨ����Ʒ�������������, 
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
