

using GameLib;
using GameLib.DI;
using QS.API;
using UnityEngine;

public class RealityPiece :  AbstractItem
{
    [Injected]
    public RealityPieceModel MyModel { get; set; }

     public RealityPiece() {
        Model = new RealityPieceModel();
    }

    //[Injected]
    public RealityPiece(RealityPieceModel model)
    {
        this.Model = model;
    }


}