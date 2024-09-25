using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QS.API{
    using GameLib;
    using System;

    public interface ICombatData : IClonable<ICombatData>
    {
        public float Hp {get;set;}
        public float Mp{get;set;}
        public float Atn{get;set;}
        public float Def {get;set;}
        public float Matk { get;set;}
        public float Res { get;set;}

    }

}
