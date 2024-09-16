using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace GameLib
{
    public interface ISington<T>
    {
        public static T Instance{ get;}
}
}
