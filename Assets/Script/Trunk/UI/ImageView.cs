
using GameLib.View;
using QS;
using UnityEngine;
using UnityEngine.UI;

class ImageView :  AbstractView
{
    protected override GameObject CreateWidget()
    {
        var ret = GameObject.Find("Image");
        var sprite = Resources.Load<Sprite>("Cross1");


        var image = ret.GetComponent<Image>();
        image.sprite = sprite;

        var cur = GameManager.Instance.GetManager<InventoryManager>()
                                        .Inventory.GetItemEntry(GameConstants.REALITY_PIECE);
        image.sprite = cur.SpriteImg;
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