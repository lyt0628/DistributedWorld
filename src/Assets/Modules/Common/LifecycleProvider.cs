using QS.Api;
using QS.GameLib.Pattern;
using UnityEngine.Events;

namespace QS.Impl
{
    /// <summary>
    /// 服务层和数据层(包含Domain和Setting)结合计较紧密, 作为一个程序集就好
    /// 表现层主要是一些UI和特效,以及利用Service做的一些终端服务,可以划分开来
    /// 问题还是, 得先实现层次DI上下文才行
    /// </summary>
    public class LifecycleProvider : SingtonBehaviour<LifecycleProvider>, ILifecycleProivder
    {

        public UnityEvent UpdateAction { get; } = new();
        public UnityEvent StartAction { get; } = new();


        void Start() => StartAction.Invoke();

        void Update() => UpdateAction?.Invoke();

    }
}