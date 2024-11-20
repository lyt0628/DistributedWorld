using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QS.API
{
    public interface ICombater : ICombatable, IAttackable, IInjurable
    {

       public bool Combating { get; set; }
       public List<ICombatable> Enemies {  get; set; }

    }
}
