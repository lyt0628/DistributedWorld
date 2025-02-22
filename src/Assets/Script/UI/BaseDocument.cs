

using QS.Api;
using QS.Api.Common;
using QS.GameLib.View;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UIElements;

namespace QS.UI
{
    public abstract class BaseDocument : AbstractView
    {

        public override bool IsVisible => Document.rootVisualElement.visible;
        protected abstract string Address { get; }
        protected UIDocument Document { get; private set;}

        public BaseDocument()
        {
            var life = UIGlobal.Instance.DI.GetInstance<ILifecycleProivder>();
            life.Request(Lifecycles.Start, Preload);
            life.UpdateAction.AddListener(OnUpdate);
        }

        protected override void DoInit()
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(Address);
            handle.Completed += (AsyncOperationHandle<GameObject> h) =>
            {
                var instance = GameObject.Instantiate(h.Result);
                Document = instance.GetComponent<UIDocument>();
                OnDocumentLoaded(Document);
                ResourceStatus = ResourceInitStatus.Started;
            };

        }

        protected abstract void OnDocumentLoaded(UIDocument document);


        protected override void DoShow()
        {
            Document.rootVisualElement.visible = true;
            //Document.rootVisualElement.MarkDirtyRepaint();
        }

        protected override void DoHide()
        {
            Document.rootVisualElement.visible = false;
            //Document.rootVisualElement.MarkDirtyRepaint();
        }

    }
}