

//using GameLib.DI;
//using QS.Api.Combat.Service;
//using QS.GameLib.Util;
//using QS.Player;

////using QS.Player;
//using QS.WorldItem.Domain;
//using UnityEngine;

//namespace QS.WorldItem
//{
//    [Scope(Lazy = false)]
//    class HpBottle : BaseItem<IPropBreed>, IProp
//    {
//        public const string itemName = nameof(HpBottle);
//        public const string imageAddress = "img_prop_hpBottle";
//        public const string prefabAddress = "MpBottle";
//        public const string description = "Recovery a little HP";

//        public HpBottle() : base(
//            new DefaultPropBreed(itemName, imageAddress, prefabAddress, description),
//            MathUtil.UUID())
//        {
//        }

//        public void Use()
//        {
//            Debug.Log($"Item {Name} used!!!!");
//        }

//    }
//}