


using QS.Api;
using QS.GameLib.View;

namespace QS.UI
{
    public abstract class AbstractUI : AbstractView
    {
        public AbstractUI() 
        {
            var life =   UIGlobal.Instance.DI.GetInstance<ILifecycleProivder>();
            life.Request(Lifecycles.Start, Preload);
            life.Request(Lifecycles.Update, OnUpdate);
        }
    }
}