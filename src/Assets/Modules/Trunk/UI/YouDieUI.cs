


using GameLib.DI;
using QS.Chara;

//using QS.Api.Combat.Domain;
using QS.Chara.Domain;
using QS.GameLib.Pattern.Message;

//using QS.Chara.Domain.Handler;
using QS.Player;
using QS.Stereotype;
using QS.UI;
using System.Threading.Tasks;

namespace QS.Trunk.UI
{
    [Scope(Lazy = false)]
    class YouDieUI : BaseDocument
    {
        public override string Address => "YouDieUI";
        protected override bool NeedPreload => true;
        public override bool BlockPlayerControl => true;


        [Injected]
        readonly IPlayer player;
        [Injected]
        readonly IUIStack uiStack;


        [Injected]
        public YouDieUI(IPlayer player)
        {
            this.player = player;
        }

        protected override Task OnDocumentLoaded()
        {
            Document.name = "YouDieUI";

            var character = player.ActiveChara;
            if (character)
            {
                OnActiveCharacterChanged(character, null);
            }
            player
                .CharacterChanged.AddListener(OnActiveCharacterChanged);

            return Task.CompletedTask;
        }

        private void OnActiveCharacterChanged(Character newChara, [Nullable] Character oldChara)
        {
            var chara = player.ActiveChara;
            chara.Messager.AddListener(CharaConstants.CHARA_DIE, OnPlayerDead);

        }
        void OnPlayerDead(IMessage msg)
        {
            uiStack.Push(this);
        }
    }

}