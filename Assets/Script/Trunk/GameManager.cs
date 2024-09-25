using GameLib.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private IView inventoryView;
    // Start is called before the first frame update
    void Start()
    {
        inventoryView = new InventoryView();
        inventoryView.OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        inventoryView.OnUpdate();
    }
}
