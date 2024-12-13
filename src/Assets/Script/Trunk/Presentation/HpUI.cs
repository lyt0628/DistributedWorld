using GameLib.DI;
using QS.Api.Combat.Domain;
using QS.Api.Data;
using QS.Combat.Domain;
using QS.GameLib.Pattern.Message;
using QS.GameLib.View;
using QS.UI;
using UnityEngine;
using UnityEngine.UI;


[Scope(Value =ScopeFlag.Sington, Lazy =false)]
class HpUI : AbstractUI
{

    [Injected]
    readonly IPlayerCharacterData PlayerCharacter;

    protected override bool CreateWidget(out GameObject widget)
    {
        widget = GameObject.Find("HP");
        return true;
    }

    public override void OnInit()
    {
        var character = PlayerCharacter.ActivedCharacter;
        if (character)
        {
            OnActivaedCharacterChanged();
        }

        PlayerCharacter
            .AddListenerForActivatedCharacterChanged(OnActivaedCharacterChanged);
    }


    private void OnActivaedCharacterChanged()
    {
        var character = PlayerCharacter.ActivedCharacter;
        var combater = character.GetComponent<CombatorBehaviour>();
        if (combater == null) Debug.LogError("Combater Is Null");
        Widget.GetComponent<Text>().text = "HP:" + combater.CombatData.Hp;
        combater.Messager.AddListener("HP", msg =>
        {
            var msg0 = (Msg1<float>)msg;
            Widget.GetComponent<Text>().text = "Hp:" + msg0.Value.ToString();
        });
    }
}