using GameLib.DI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace QS.Common
{
    public interface IBindingProvider 
    {
        void ProvideBinding(IDIContext context);
    }

}