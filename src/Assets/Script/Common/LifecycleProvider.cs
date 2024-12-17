using QS.Api;
using QS.GameLib.Pattern;
using System;

namespace QS.Impl
{
    /// <summary>
    /// 服务层和数据层(包含Domain和Setting)结合计较紧密, 作为一个程序集就好
    /// 表现层主要是一些UI和特效,以及利用Service做的一些终端服务,可以划分开来
    /// 问题还是, 得先实现层次DI上下文才行
    /// </summary>
    public class LifecycleProvider : SingtonBehaviour<LifecycleProvider>, ILifecycleProivder
    {
        event Action UpdateCallbacks ;
        event Action LateUPdateCallbacks ;
        event Action StartCallbacks;

        public void Request(Lifecycles statge, Action callback)
        {
            switch (statge)
            {
                case Lifecycles.Start:
                    StartCallbacks += callback;
                    break;
                case Lifecycles.Update:
                    UpdateCallbacks += callback;
                    break;
                case Lifecycles.LateUpdate:
                    LateUPdateCallbacks += callback;
                    break;
            }
        }

        void Start()
        {
            StartCallbacks?.Invoke();
        }
        void Update()
        {
            UpdateCallbacks?.Invoke();
        }
        void LateUpdate()
        {
            LateUPdateCallbacks?.Invoke();
        }

        public void Cancel(Lifecycles statge, Action callback)
        {
             switch (statge)
            {
                case Lifecycles.Start:
                    StartCallbacks -= callback;
                    break;
                case Lifecycles.Update:
                    UpdateCallbacks -= callback;
                    break;
                case Lifecycles.LateUpdate:
                    LateUPdateCallbacks -= callback;
                    break;
            }
        }
    }
}