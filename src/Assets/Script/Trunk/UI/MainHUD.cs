


using GameLib.DI;
using QS.Api.Combat.Domain;
using QS.Chara.Domain;
using QS.Chara.Domain.Handler;

using QS.GameLib.Pattern.Message;
using QS.Player;
using QS.PlayerControl;
using QS.Stereotype;
using QS.Trunk.UI;
using QSUILibrary;
using UnityEngine;
using UnityEngine.UIElements;

namespace QS.UI
{
    /// <summary>
    /// 只有Trunk 模塊可以訪問到所有的內容，所有，最終的UI只能放到Trunk程序集中
    /// 
    /// </summary>
    [Scope(Value = ScopeFlag.Sington, Lazy = false)]
    class MainHUD : BaseDocument
    {
        protected override string Address => "PUI_MainHUD";

        [Injected]
        readonly IPlayerData playerCharacter;
        [Injected]
        readonly PlayerControls playerControls;

        FillSlider hpBar;
        FillSlider mpBar;

        protected override bool DoPreload() => true;

        protected override void OnDocumentLoaded(UIDocument document)
        {
            document.name = "MainHUD";
            var root = document.rootVisualElement;

            hpBar = root.Q<FillSlider>("hpBar");
            mpBar = root.Q<FillSlider>("mpBar");

            var character = playerCharacter.ActivedCharacter;
            if (character)
            {
                OnActiveCharacterChanged(character, null);
            }
            playerCharacter
                .CharacterChanged.AddListener(OnActiveCharacterChanged);




        }


        private void OnActiveCharacterChanged(Character newChara, [Nullable] Character oldChara)
        {
            var chara = playerCharacter.ActivedCharacter;
            var combatorHandler = chara.Get<CombatorHandler>();
            combatorHandler.CombatDataChanged.AddListener(OnCombatDataChanged);

        }

        void OnCombatDataChanged(ICombatData newData, ICombatData oldData, ICombatData maxData)
        {
            hpBar.value = newData.Hp / maxData.Hp;
            mpBar.value = newData.Mp / maxData.Mp;
        }

    }
}