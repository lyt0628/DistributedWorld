
using GameLib;
using GameLib.View;
using QS;
using QS.API;
using UnityEngine;
using UnityEngine.UI;

class HpView :  AbstractView
{
    protected override bool CreateWidget(out GameObject widget)
    {
        widget = GameObject.Find("HP");
        return true;
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
               Widget.GetComponent<Text>().text = "Hp:" + msg0.Value.ToString();
           });
        });


    }

    

}