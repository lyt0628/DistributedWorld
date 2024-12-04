using QS.GameLib.Pattern;
using System;


namespace QS.Api
{
    /// <summary>
    /// Providing Lifecycle for those no Unity Component Classes.
    /// <seealso cref="Lifecycles"/>
    /// </summary>
    public interface ILifecycleProivder : ISington<ILifecycleProivder>
    {
        bool Request(Lifecycles statge, Action callback);

    }

}