using QS.Api;
using QS.GameLib.Pattern;
using UnityEngine.Events;

namespace QS.Impl
{
    /// <summary>
    /// ���������ݲ�(����Domain��Setting)��ϼƽϽ���, ��Ϊһ�����򼯾ͺ�
    /// ���ֲ���Ҫ��һЩUI����Ч,�Լ�����Service����һЩ�ն˷���,���Ի��ֿ���
    /// ���⻹��, ����ʵ�ֲ��DI�����Ĳ���
    /// </summary>
    public class LifecycleProvider : SingtonBehaviour<LifecycleProvider>, ILifecycleProivder
    {

        public UnityEvent UpdateAction { get; } = new();
        public UnityEvent StartAction { get; } = new();


        void Start() => StartAction.Invoke();

        void Update() => UpdateAction?.Invoke();

    }
}