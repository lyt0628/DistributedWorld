


using QS.Api;
using QS.GameLib.View;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace QS.UI
{
    public abstract class BaseUI : AbstractView
    {
        public override bool IsVisible => view.activeSelf;
        public BaseUI() 
        {
            
            var life =   UIGlobal.Instance.DI.GetInstance<ILifecycleProivder>();
            life.Request(Lifecycles.Start, Preload);
            life.UpdateAction.AddListener(OnUpdate);
        }
        protected GameObject view;


        protected override void DoShow()
        {
            view.SetActive(true);
        }

        protected override void DoHide()
        {
            view.SetActive(false);
        }
    }
}