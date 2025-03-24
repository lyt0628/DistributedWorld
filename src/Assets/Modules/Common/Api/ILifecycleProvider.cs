using QS.GameLib.Pattern;
using UnityEngine.Events;


namespace QS.Api
{
    /// <summary>
    /// Providing Lifecycle for those no Unity Component Classes.
    /// <seealso cref="Lifecycles"/>
    /// </summary>
    public interface ILifecycleProivder : ISington<ILifecycleProivder>
    {
        UnityEvent UpdateAction { get; }
        UnityEvent StartAction { get; }
    }

}