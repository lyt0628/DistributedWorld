
using GameLib.View;
using QS;
using UnityEngine;
using UnityEngine.UI;

public class MainView : AbstractView
{
    protected override GameObject CreateWidget()
    {
        var ret = GameObject.Find("InventoryIcon");
        var sprite = Resources.Load<Sprite>("LightBox");


        var image = ret.GetComponent<Image>();
        image.sprite = sprite;

        return  ret;
    }
    public override void OnInit()
    {
        //Hide();
    }

    public override void Preload()
    {
        Widget = CreateWidget();
    }

    public override void OnUpdate()
    {
    }


}
