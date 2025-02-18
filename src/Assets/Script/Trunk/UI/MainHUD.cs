


using GameLib.DI;
using QS.Api.Combat.Domain;
using QS.Chara.Domain;
using QS.Chara.Domain.Handler;

using QS.GameLib.Pattern.Message;
using QS.PlayerControl;
using QS.Stereotype;
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
        readonly IPlayerCharacterData playerCharacter;

        FillSlider hpBar;

        protected override bool DoPreload() => true;
        public override bool IsResident => true;

        protected override void OnDocumentLoaded(UIDocument document)
        {
            document.name = "MainHUD";
            var root = document.rootVisualElement;

            hpBar = root.Q<FillSlider>();

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

            //if (!chara.TryGetComponent<CombatorBehaviour>(out var combater)) Debug.LogError("Combater Is Null");
            //hpBar.value = combater.CombatData.Hp / 100;
            //combater.Messager.AddListener("HP", msg =>
            //{
            //    hpBar.value = ((Msg1<float>)msg).Value / 100;
            //});
        }

        void OnCombatDataChanged(ICombatData combatData)
        {
            hpBar.value = combatData.Hp / 100;
        }

    }
}