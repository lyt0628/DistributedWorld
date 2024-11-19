
using GameLib;
using GameLib.DI;
using GameLib.View;
using QS;
using QS.API;
using QS.API.Data;
using UnityEngine;
using UnityEngine.UI;

class HpView :  AbstractView
{
    [Injected]
    IPlayerCharacterData PlayerCharacter;

    protected override bool CreateWidget(out GameObject widget)
    {
        widget = GameObject.Find("HP");
        return true;
    }
    public override void OnInit()
    {
        //Hide();
        var ctx = GameManager.Instance.GlobalDIContext;
        ctx.Inject(this);

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
        var combater = character.GetComponent<CDefaultCombater>();
        if (combater == null) Debug.LogError("Combater Is Null");    

        combater.Messager.AddListener("HP", msg =>
        {
           var msg0 = (SingleArgMessage<float>)msg;
           Widget.GetComponent<Text>().text = "Hp:" + msg0.Value.ToString();
        });
    }
}