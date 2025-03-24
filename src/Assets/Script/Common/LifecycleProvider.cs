using QS.Api;
using QS.GameLib.Pattern;
using System;

namespace QS.Impl
{
    /// <summary>
    /// ���������ݲ�(����Domain��Setting)��ϼƽϽ���, ��Ϊһ�����򼯾ͺ�
    /// ���ֲ���Ҫ��һЩUI����Ч,�Լ�����Service����һЩ�ն˷���,���Ի��ֿ���
    /// ���⻹��, ����ʵ�ֲ��DI�����Ĳ���
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