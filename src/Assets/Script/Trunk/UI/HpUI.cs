using GameLib.DI;
using QS.Api.Combat.Domain;
using QS.Combat.Domain;
using QS.GameLib.Pattern.Message;
using QS.GameLib.View;
using QS.PlayerControl;
using QS.UI;
using UnityEngine;
using UnityEngine.UI;

namespace QS.UI
{

    [Scope(Value = ScopeFlag.Sington, Lazy = false)]
    class HpUI : BaseUI
    {

        [Injected]
        readonly IPlayerCharacterData PlayerCharacter;

        protected override bool DoPreload() => true;

        protected override void DoInit()
        {
            view = GameObject.Find("HP");
            //var character = PlayerCharacter.ActivedCharacter;
            //if (character)
            //{
            //    OnActivaeCharacterChanged();
            //}
            //PlayerCharacter.CharacterChanged.AddListener(OnActivaeCharacterChanged);
        }


        private void OnActivaeCharacterChanged()
        {
            var character = PlayerCharacter.ActivedCharacter;
            var combater = character.GetComponent<CombatorBehaviour>();
            if (combater == null) Debug.LogError("Combater Is Null");
            view.GetComponent<Text>().text = "HP:" + combater.CombatData.Hp;
            combater.Messager.AddListener("HP", msg =>
            {
                var msg0 = (Msg1<float>)msg;
                view.GetComponent<Text>().text = "Hp:" + msg0.Value.ToString();
            });
        }


    }
}