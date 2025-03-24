//using GameLib.DI;
//using QS.Player;
//using System.Threading.Tasks;
//using UnityEngine;

//namespace QS.UI
//{

//    //[UnitScope(Value = ScopeFlag.Sington, Lazy = false)]
//    class HpUI : BaseUI
//    {
//        [Injected]
//        readonly IPlayer PlayerCharacter;

//        protected override bool NeedPreload => true;

//        protected override Task Init()
//        {
//            view = GameObject.Find("HP");
//            //var chara = GhostSamurai.ActiveChara;
//            //if (chara)
//            //{
//            //    OnActivaeCharacterChanged();
//            //}
//            //GhostSamurai.CharacterChanged.AddListener(OnActivaeCharacterChanged);
//            return base.Init();
//        }


//        //private void OnActivaeCharacterChanged()
//        //{
//        //    var chara = GhostSamurai.ActiveChara;
//        //    var combater = chara.GetComponent<CombatorBehaviour>();
//        //    if (combater == null) Debug.LogError("Combater Is Null");
//        //    view.GetComponent<Text>().text = "HP:" + combater.CombatData.Hp;
//        //    combater.Messager.AddListener("HP", NextMsg =>
//        //    {
//        //        var msg0 = (Msg1<float>)NextMsg;
//        //        view.GetComponent<Text>().text = "Hp:" + msg0.Value.ToString();
//        //    });
//        //}


//    }
//}