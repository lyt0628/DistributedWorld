


using GameLib.DI;
using QS.Chara.Domain;

//using QS.Chara.Domain.Handler;
using QS.Player;
using QS.PlayerControl;
using QS.Stereotype;
using QS.Trunk.UI;
using System.Threading.Tasks;
using UnityEngine.UIElements;

namespace QS.UI
{
    /// <summary>
    /// 只有Trunk 模塊可以訪問到所有的內容，所有，最終的UI只能放到Trunk程序集中
    /// 要么 UI 持有异步状态，要么有人收集 UI的状态。
    /// UI 有一些特点
    /// 常驻（顶层）UI都是单例的
    /// 我需要收集的就是这些常驻UI的状态
    /// </summary>
    [Scope(Lazy = false)]
    class MainHUD : BaseDocument
    {
        public override string Address => "PUI_MainHUD";

        [Injected]
        readonly IPlayer player;
        [Injected]
        readonly PlayerControls playerInput;
        [Injected]
        readonly InventoryUI inventoryUI;

        VisualElement hpBar;
        VisualElement mpBar;

        protected override bool NeedPreload => true;
        public override bool IsResident => true;
        protected override Task OnDocumentLoaded()
        {
            Document.name = "UI_MainHUD";

            hpBar = Container.Q("hpValue");
            mpBar = Container.Q("manaValue");

            var character = player.ActiveChara;
            if (character)
            {
                OnActiveCharacterChanged(character, null);
            }
            player.CharacterChanged.AddListener(OnActiveCharacterChanged);

            BindPlayerInputCB();

            return Task.CompletedTask;

            void BindPlayerInputCB()
            {
                playerInput.MainUI.OpenInventory.started += (_) =>
                {
                    inventoryUI.LoadAsync();
                    UIStack.Push(inventoryUI);
                };
            }
        }

        public override void OnDeactive()
        {
            base.OnDeactive();
            playerInput.MainUI.Disable();
        }

        public override void OnActive()
        {
            base.OnActive();
            playerInput.MainUI.Enable();
        }

        private void OnActiveCharacterChanged(Character newChara, [Nullable] Character oldChara)
        {
            newChara.Combator.OnStateChanged.AddListener(UpdateHp);
        }

        void UpdateHp()
        {

            var chara = player.ActiveChara;
            var combator = chara.Combator.Combator;
            hpBar.style.width = new StyleLength();
            hpBar.style.width = new Length(combator.Hp / combator.MaxHP * 100, LengthUnit.Percent);
            mpBar.style.width = new Length(combator.Mana / combator.MaxMana * 100, LengthUnit.Percent);
        }

    }
}