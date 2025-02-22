using QS.Api.Common;
using QS.GameLib.Pattern.Message;
using QS.GameLib.Rx.Relay;
using QS.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace QS.GameLib.View
{

    /// <summary>
    /// 這個是 是UGUI綁定的，得稍加修改才行
    /// 不行，有所依赖的话，必须把依赖加进来才行，像是指令 InputSystem，
    /// </summary>
    public abstract class AbstractView : IViewNode
    {
        public AbstractView() {
            globalMessager = UIGlobal.Instance.GetInstance<IMessager>(QS.UI.DINames.UI_GLOBAL_MESSAGER);
        }
        public static string MSG_BLOCK_PLAYER_CONTROL_UI_SHOW = "MSG_BLOCK_PLAYER_CONTROL_UI_SHOW";
        public static string MSG_BLOCK_PLAYER_CONTROL_UI_HIDE = "MSG_BLOCK_PLAYER_CONTROL_UI_HIDE";

        /// <summary>
        /// 指示視圖是否可見
        /// </summary>
        public abstract bool IsVisible { get; }

        /// <summary>
        /// 指示視圖是否是葉節點
        /// </summary>
        public virtual bool IsLeaf { get; } = true;

        public virtual bool BlockPlayerControl { get; } = false;

        protected List<IViewNode> children = new();
        /// <summary>
        /// 指示視圖是否整個遊戲過程需要用
        /// </summary>
        public virtual bool IsResident { get; protected set; } = true;

        protected readonly IMessager globalMessager;
        public virtual IMessager Messager => globalMessager;

        public ResourceInitStatus ResourceStatus { get; protected set; } = ResourceInitStatus.Shutdown;

        public UnityEvent OnReady { get; } = new();

        public void Show()
        {
            // 初始化的時候暫時不接受處理，等到Flow完成後再來
            if (ResourceStatus == ResourceInitStatus.Initializing) return;

            if (ResourceInitStatus.Shutdown == ResourceStatus) Initialize();
            if (BlockPlayerControl) globalMessager.Boardcast(MSG_BLOCK_PLAYER_CONTROL_UI_SHOW, Msg0.Instance);
            DoShow();
            OnActive();
            if (!IsLeaf) children.ForEach(x => x.Show());
        }

        public void Hide()
        {
            if (BlockPlayerControl) globalMessager.Boardcast(MSG_BLOCK_PLAYER_CONTROL_UI_HIDE, Msg0.Instance);
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

        public void Preload()
        {
            if (DoPreload())
            {
                Initialize();
            }
            if (!IsLeaf)
            {
                children.ForEach(x => x.Preload());
            }

        }

        protected virtual bool DoPreload() => false;

        protected abstract void DoShow();

        protected abstract void DoHide();
      
        public  void Initialize()
        {

            if (ResourceInitStatus.Shutdown == ResourceStatus)
            {
                ResourceStatus = ResourceInitStatus.Initializing;
                DoInit();
            }

            if (!IsLeaf)
            {
                children.ForEach(x => x.Initialize());
            }

        }

        protected virtual void DoInit()
        {
            ResourceStatus = ResourceInitStatus.Started;
            OnReady.Invoke();
        }

        public virtual void Shutdown()
        {
            if (!IsLeaf)
            {
                children.ForEach(x => x.Shutdown());
            }
            DoShutdown();
        }

        protected virtual void DoShutdown()
        {
            ResourceStatus = ResourceInitStatus.Shutdown;
        }


        public virtual void OnUpdate() { }
        public virtual void OnModelChanged() { }

        public void Add(IViewNode view)
        {
            children.Add(view);
        }

        public virtual void OnActive()
        {
          
        }

        public virtual void OnDeactive()
        {
        }
    }
}