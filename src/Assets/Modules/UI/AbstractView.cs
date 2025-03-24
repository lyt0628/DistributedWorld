
using QS.Api;
using QS.Api.Common;
using QS.Common;
using QS.GameLib.Pattern.Message;
using QS.GameLib.Rx.Relay;
using QS.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace QS.GameLib.View
{

    /// <summary>
    /// 這個是 是UGUI綁定的，得稍加修改才行
    /// 不行，有所依赖的话，必须把依赖加进来才行，像是指令 InputSystem，
    /// 
    /// UI 采用两站式管理，第一是覆盖所有界面的UI，
    /// 像是战斗显示，背包，人物，人物等专门UI，用栈来管理，称之为顶级视图
    /// 然后是顶级视图内部工作用的树状UI，像是背包中丢弃物品用于确认的弹窗
    /// 模态视图需要子视图持有父级视图
    /// 
    /// 我要仿照 React 的结构来设计这个模板
    /// </summary>
    public abstract class AbstractView<TProps, TStates> : IViewNode
    {
        sealed class ViewLoadOp : AsyncOpBase<IView>
        {
            readonly AbstractView<TProps, TStates> view;

            public ViewLoadOp(AbstractView<TProps, TStates> view)
            {
                this.view = view;
            }

            protected override async void Execute()
            {
                await view.Init();
                RegisterOnUpdateLifecycle();

                if (!view.IsLeaf)
                {
                    await System.Threading.Tasks.Task
                        .WhenAll(view.children
                                .Select(c => c.LoadAsync()
                                              .Task));
                }

                Complete(view);
            }

            private void RegisterOnUpdateLifecycle()
            {
                var life = UIGlobal.Instance.GetInstance<ILifecycleProivder>();
                life.UpdateAction.AddListener(view.OnUpdate);
            }

        }

        public AbstractView(IViewNode parent = null)
        {
            this.parent = parent;
            m_loadOp = new ViewLoadOp(this);

            globalMessager = new Messager();
            UIStack = UIGlobal.Instance.GetInstance<IUIStack>();

            Preload();
            Relay<TProps>
                .Emit(out m_PropsEmittter)
                .Subscrib(
                onNext: (nextProps) =>
                {
                    LoadStatesFromProps(nextProps, out var nextStates);
                    dirty = ShouldUpdate(nextProps, nextStates);
                    SetPropsWithoutNotify(nextProps);
                    this.states = nextStates;
                },
                onError: (e) =>
                {
                    throw e;
                });
        }


        [Obsolete]
        public IEmitter<TProps> PropsEmitter => m_PropsEmittter;
        readonly IEmitter<TProps> m_PropsEmittter;

        private void UnregisterLifecycle()
        {
            var life = UIGlobal.Instance.GetInstance<ILifecycleProivder>();
            life.UpdateAction.RemoveListener(OnUpdate);
        }

        #region [[ States]]
        [Stereotype.Nullable]
        public TStates states; 

        /// <summary>
        /// 访问器，设置属性时会触发响应流
        /// </summary>
#pragma warning disable IDE1006 // 命名样式
        public TProps props

        {
            get
            {
                return m_Props == null ? DefaultProps : m_Props;
            }
            set
            {
                m_PropsEmittter.Emit(value);
            }
        }
#pragma warning restore IDE1006 // 命名样式
        public abstract TProps DefaultProps { get; }
        private TProps m_Props;

        /// <summary>
        /// 直接设置Props，但不触发响应流
        /// </summary>
        /// <param name="props"></param>
        protected void SetPropsWithoutNotify(TProps props)
        {
            m_Props = props;
        }

        public TProps ElmentProps => props;

        public abstract bool IsVisible { get; }

        public virtual bool IsLeaf { get; } = true;

        public virtual bool BlockPlayerControl { get; } = false;

        protected List<IViewNode> children = new();

        public virtual bool IsResident { get; } = true;

        protected readonly IMessager globalMessager;
        public virtual IMessager Messager => globalMessager;

        public ResourceInitStatus ResourceStatus { get; protected set; } = ResourceInitStatus.Shutdown;

        readonly IViewNode parent;

        public bool IsRoot => parent == null;
        protected virtual bool NeedPreload { get; } = false;
        public void MarkDirty()
        {
            dirty = true;
        }
        bool dirty = false;
        /// <summary>
        /// 默认地，只要属性和状态不改变 AbstractView 就认为不用更新 
        /// </summary>
        /// <param name="nextProps"></param>
        /// <param name="nextStates"></param>
        /// <returns></returns>
        protected virtual bool ShouldUpdate(TProps nextProps, TStates nextStates)
        {
            return !nextProps.Equals(props);
        }

        protected IUIStack UIStack { get; }


        #endregion

        #region [[Public Method]]
        public virtual void Preload()
        {
            if (NeedPreload)
            {
                LoadAsync();
            }
        }

        public async void Show()
        {
            if (!m_loadOp.HasExecuted) LoadAsync();

            await LoadHandle.Task;
            //if (BlockPlayerControl) globalMessager.Boardcast(MSG_BLOCK_PLAYER_CONTROL_UI_SHOW, UnitMsg.Unit);
            DoShow();
            OnActive();
            if (!IsLeaf) children.ForEach(x => x.Show());
        }

        public void Hide()
        {
            //if (BlockPlayerControl) globalMessager.Boardcast(MSG_BLOCK_PLAYER_CONTROL_UI_HIDE, UnitMsg.Unit);
            if (!IsLeaf)
            {
                children.ForEach(x => x.Hide());
            }

            OnDeactive();
            if (IsVisible)
            {
                if (IsResident)
                {
                    DoHide();
                }
                else
                {
                    Shutdown();
                }
            }
        }

        public void Shutdown()
        {
            if (!IsLeaf)
            {
                children.ForEach(x => x.Shutdown());
            }
            DoShutdown();
            UnregisterLifecycle();
        }
        #endregion

        #region [[Lifecycle Methods]]

        internal virtual Task Init()
        {
            return Task.CompletedTask;
        }

        protected virtual void LoadStatesFromProps(TProps props, out TStates states) { states = default; }

        protected abstract void DoShow();

        protected abstract void DoHide();

        protected virtual void DoShutdown()
        {
        }

        public void OnUpdate()
        {

            if (dirty)
            {
                Render();
            }
            dirty = false;
        }

        public virtual void Render() { }


        public virtual void OnActive()
        {

        }

        public virtual void OnDeactive()
        {
        }
        #endregion


        public void Add(IViewNode view)
        {
            children.Add(view);
        }
        readonly AsyncOpBase<IView> m_loadOp;
        public IAsyncOpHandle<IView> LoadAsync()
        {
            if (!m_loadOp.HasExecuted)
            {
                m_loadOp.Invoke();
                LoadHandle = m_loadOp.Handle;
            }
            return LoadHandle;
        }

        public IAsyncOpHandle<IView> LoadHandle { get; private set; }
    }
}