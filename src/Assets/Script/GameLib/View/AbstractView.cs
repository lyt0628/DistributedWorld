

using Unity.VisualScripting;
using UnityEngine;

namespace GameLib.View
{
    public abstract class AbstractView : IViewNode
    {
        virtual public bool IsVisible
        {
            get; protected set;
        }

        protected static IMessager globalMessager = new Messager();

        public GameObject Widget { get; protected set; }

        private bool _IsResident = true;
        public virtual bool IsResident
        {
            get { return _IsResident; }
            protected set
            {
                _IsResident=value;
            }
        }

        #region [[View]]
        virtual public void Show()
        {
                if (Widget == null) { 
                    Widget = CreateWidget();
                }
                if (Widget) {
                    Widget.SetActive(true);
                    IsVisible = true;
                    OnActive();
                }
        }

       virtual  public void Hide()
        {
            if (Widget && Widget.activeSelf == true) { 
                OnDeActive();
                if (IsResident)
                {
                    Widget.SetActive(false);
                }
                else
                {
                    ReleaseWidget(Widget);
                    OnRealse();
                }

            }
            IsVisible = false;
        }

        virtual public void OnActive()
        {
        }
        virtual public void Preload()
        {
        }


         virtual public void OnDeActive()
        {
        }

        virtual public void OnRealse()
        {
        }

        virtual public void OnInit()
        {
        }

        virtual public void OnUpdate()
        {
        }
        virtual public void OnModelChanged()
        {
        }


        #endregion

        #region [[ViewNode]]
        virtual public void Add(IView view)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region [[Template Method]]
        virtual protected GameObject CreateWidget() {
            throw new System.NotImplementedException();
        }
        virtual protected void ReleaseWidget(GameObject widget) { }

        virtual public IMessager GetMessager()
        {
            return globalMessager;
        }

        #endregion
    }
}