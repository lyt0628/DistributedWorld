


using GameLib;
using QS.Api;
using System;

namespace QS.Impl
{
    public class LifecycleProvider : SingtonBehaviour<LifecycleProvider>, ILifecycleProivder
    {
        private event Action UpdateCallbacks = () => { };
        private event Action LateUPdateCallbacks = () => { };

        public bool Request(Lifecycles statge, Action callback)
        {
            switch (statge)
            {
                case Lifecycles.Update:
                    UpdateCallbacks += callback;
                    break;
                case Lifecycles.LateUpdate:
                    LateUPdateCallbacks += callback;
                    break;
                default:
                    return false;
            }
            return true;
        }

        void Update()
        {
            UpdateCallbacks.Invoke();
        }
        void LateUpdate()
        {
            LateUPdateCallbacks.Invoke();
        }
    }
}