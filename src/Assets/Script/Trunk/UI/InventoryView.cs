using UnityEngine;

using GameLib.View;



public class InventoryView : AbstractView
{
    public override bool IsResident {
        get { return true; }
    }

    public override void OnInit()
    {
        Widget = new GameObject
        {
            name = "InventoryView"
        };
        Widget.SetActive(false);
    }

    public override void OnUpdate()
    {
        Debug.Log(Widget.name);
    }
    public override void OnModelChanged()
    {
        base.OnModelChanged();
    }

}