
using GameLib.View;
using QS;
using UnityEngine;
using UnityEngine.UI;

class ImageView :  AbstractView
{
    protected override bool CreateWidget(out GameObject widget)
    {
        widget = GameObject.Find("Image");
        var sprite = Resources.Load<Sprite>("Cross1");

        var image = widget.GetComponent<Image>();
        //image.sprite = sprite;

        var cur = GameManager.Instance.GetManager<InventoryManager>()
                                        .Inventory.GetItemEntry(GameConstants.REALITY_PIECE);
        image.sprite = cur.SpriteImg;

        return  false;
    }
    public override void OnInit()
    {
        //Hide();
    }




}