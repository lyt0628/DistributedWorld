using QS.GameLib.View;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UIElements;

namespace QS.UI
{

    public abstract class BaseDocument<TProp, TState> : AbstractView<TProp, TState>
    {

        public override bool IsVisible => Document.rootVisualElement.visible;
        public virtual string Address
        {
            get
            {
                throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// �ĵ����ڵ� document ����������Ǵ� ��Դ���س����Ļ��� document����Ϊ��
        /// </summary>
        [Stereotype.Nullable]
        public UIDocument Document { get; private set; }

        [Stereotype.Nullable]
        public VisualElement Container { get; private set; }
        [Stereotype.Nullable]
        public VisualTreeAsset TreeAsset { get; private set; }

        protected BaseDocument(IViewNode parent = null) : base(parent)
        {
        }
        protected BaseDocument(VisualTreeAsset treeAsset, IViewNode parent = null) : base(parent)
        {
            TreeAsset = treeAsset;
            Container = TreeAsset.Instantiate();
        }
        protected BaseDocument(Func<VisualElement> generator, IViewNode parent = null) : base(parent)
        {
            Container = generator.Invoke();
            TreeAsset = Container.visualTreeAssetSource;
        }
        protected BaseDocument(VisualElement container, IViewNode parent = null) : base(parent)
        {
            Container = container;
            TreeAsset = Container.visualTreeAssetSource;
        }
        internal override async Task Init()
        {

            // �������Ϊ������ͼ���س����Ļ������Ǽ��ص�UIDocument
            if (IsRoot)
            {
                var handle = Addressables.LoadAssetAsync<GameObject>(Address);
                await handle.Task;

                var instance = GameObject.Instantiate(handle.Result);
                Document = instance.GetComponent<UIDocument>();
                Container = Document.rootVisualElement;
                TreeAsset = Container.visualTreeAssetSource;
                Container.userData = this;
                DoHide();
                await OnDocumentLoaded();
                Hide();
            }
            // ������Ǽ���Ϊ����Ϊ Template
            else if (Container != null || TreeAsset != null)
            {
                await OnDocumentLoaded();
                Container.userData = this;
            }
            else
            {
                AsyncOperationHandle<VisualTreeAsset> handle = Addressables.LoadAssetAsync<VisualTreeAsset>(Address);
                await handle.Task;
                TreeAsset = handle.Result;
                Container = TreeAsset.Instantiate();
                await OnDocumentLoaded();
                Container.userData = this;
            }

        }

        protected virtual Task OnDocumentLoaded() { return System.Threading.Tasks.Task.CompletedTask; }
        //protected virtual Task OnElementLoaded(VisualElement container) { return System.Threading.Tasks.Task.CompletedTask; }


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


    public abstract class BaseDocument : BaseDocument<object, object>
    {
        static readonly object m_DefaultProps = new();
        public override object DefaultProps => m_DefaultProps;
        protected BaseDocument(IViewNode parent = null) : base(parent)
        {
        }

        protected BaseDocument(VisualTreeAsset treeAsset, IViewNode parent = null) : base(treeAsset, parent)
        {
        }

        protected BaseDocument(VisualElement container, IViewNode parent = null) : base(container, parent)
        {
        }

        protected BaseDocument(Func<VisualElement> generator, IViewNode parent = null) : base(generator, parent)
        {
        }


    }
}