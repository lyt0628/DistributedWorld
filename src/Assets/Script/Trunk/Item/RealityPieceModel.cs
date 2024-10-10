
using GameLib;
using QS.API;
using UnityEngine;



public class RealityPieceModel : IItemModel
{
        
    public string Name => GameConstants.REALITY_PIECE;
    public string UUID => throw new System.NotImplementedException();

    public ItemType Type => ItemType.Misc;

    public string SubType => GameConstants.CURRENCY;

    public int Rank => GameConstants.LV_5;

    public string Description => "Currency";

    public Sprite SpriteImg => Resources.Load<Sprite>("Point_0081");

    public bool Usable => false;

    public bool Consumable => false;

    public GameObject Prefab => throw new System.NotImplementedException();

    public void Use()
    {
    }
}
