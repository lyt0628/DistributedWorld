

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
//    class Wine : BaseItem<IPropBreed>, IProp
//    {

//        [Injected]
//        readonly IBuffFactory buffFactory;
//        [Injected]
//        readonly IPlayer playerData;

//        public Wine() : base(
//            new DefaultPropBreed("Wine", "img_prop_hpBottle", "MpBottle", "Recovery a little MP"),
//            MathUtil.UUID())
//        {
//        }

//        public void Use()
//        {
//            Debug.Log($"Item {Name} used!!!!");
//            //playerData.ActiveChara.Combator.AddBuff(MathUtil.uuid(), buffFactory.LinearDef(10, 0));
//            //Debug.Log(playerData.ActiveChara.Combator.Attack().Atn);
//        }

//    }
//}