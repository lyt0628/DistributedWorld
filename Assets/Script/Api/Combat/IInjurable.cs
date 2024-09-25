using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace QS.API
{

public interface IInjurable 
{
    public void Injured(IAttack attack);
}
}
