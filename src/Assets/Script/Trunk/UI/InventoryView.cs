using UnityEngine;

using GameLib.View;



public class InventoryView : AbstractView
{
    public override bool IsResident {
        get { return true; }
    }

    protected override GameObject CreateWidget()
    {
        return GameObject.Find("Inventory");
    }
    public override void OnInit()
    {
        base.OnInit();
        Hide();
    }

    public override void OnModelChanged()
    {
    }

}