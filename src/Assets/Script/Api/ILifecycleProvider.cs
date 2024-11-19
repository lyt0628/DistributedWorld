
using GameLib;
using System;


namespace QS.API
{
    /// <summary>
    /// Providing Lifecycle for those non-component Classes.
    /// </summary>
    interface ILifecycleProivder : ISington<ILifecycleProivder>
    {
        

        bool Request(Lifecycles statge, Action callback);

    }

}