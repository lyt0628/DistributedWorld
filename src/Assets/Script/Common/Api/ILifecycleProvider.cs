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
        void Request(Lifecycles statge, Action callback);
        void Cancel(Lifecycles statge, Action callback);

    }

}