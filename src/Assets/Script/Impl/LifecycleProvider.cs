


using GameLib;
using QS.API;
using System;
using UnityEngine;

namespace QS.Impl
{
    class LifecycleProvider : SingtonBehaviour<LifecycleProvider>, ILifecycleProivder
    {
        private Action updateCallbacks = ()=> { };
        private Action lateUPdateCallbacks = ()=> { };

        public bool Request(Lifecycles statge, Action callback)
        {
            switch (statge) {
                case Lifecycles.Update:
                    updateCallbacks += callback;
                    break;
                case Lifecycles.LateUpdate:
                    lateUPdateCallbacks += callback;
                    break;
                default:
                    return false;
            }
            return true;
        }

        void Update() {
            updateCallbacks.Invoke();
        }
        void LateUpdate()
        {
            lateUPdateCallbacks.Invoke();
        }
    }
}