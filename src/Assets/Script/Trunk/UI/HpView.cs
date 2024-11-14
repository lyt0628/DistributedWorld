
using GameLib;
using GameLib.View;
using QS;
using QS.API;
using UnityEngine;
using UnityEngine.UI;

class HpView :  AbstractView
{
    protected override GameObject CreateWidget()
    {
        var ret = GameObject.Find("HP");

        return  ret;
    }
    public override void OnInit()
    {
        //Hide();

        var playerManager = GameManager.Instance.GetManager<IPlayerManager>();
        playerManager.Messager.AddListener("ActivedCharacterChanged", _ =>
        {
            var combater = playerManager.GetActivedCharacter().GetComponent<CDefaultCombater>();
            combater.Messager.AddListener("HP", msg =>
           {
               var msg0 = (SingleArgMessage<float>)msg;
               Widget.GetComponent<Text>().text = msg0.Value.ToString();
           });
        });


    }

    public override void Preload()
    {
        Widget = CreateWidget();
    }

}