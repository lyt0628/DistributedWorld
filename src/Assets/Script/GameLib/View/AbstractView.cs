

using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace GameLib.View
{
    public abstract class AbstractView : IViewNode
    {
        virtual public bool IsVisible
        {
            get; protected set;
        } = false;
        virtual public bool Initialed
        {
            get; protected set;
        } = false;
        virtual public bool IsLeaf { get; }  = true;

        protected static IMessager globalMessager = new Messager();
        protected List<IViewNode> children = new();

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
        /*
         * Show This View. If View is not a Leaf, Show Children 
         */
        virtual public void Show()
        {
                if (!Initialed ) { 
                    //Widget = CreateWidget();
                    OnInit(); 
                }
                if (Widget) {
                    Widget.SetActive(true);
                    OnActive();
                }
                if (!IsLeaf) {
                    children.ForEach(x => x.Show());
                }

                IsVisible = true;
        }

       virtual  public void Hide()
        {
            if (!IsLeaf)
            {
                children.ForEach(x => x.Hide());
            }

            if (Widget && Widget.activeSelf == true) { 
                if (IsResident)
                {
                    OnDeActive();
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

        /*
         * Called When Widget Show
         */
        virtual public void OnActive()
        {
        }

        /*
         * Called when widget hidden
         */
         virtual public void OnDeActive()
        {
        }


        virtual public void Preload()
        {
        }


        virtual public void OnRealse()
        {
        }

        virtual public void OnInit()
        {
            if(!Initialed)
            {
               Load();
            }
            Initialed=true;
        }
        virtual protected void Load()
        {
                if (!IsLeaf) {
                    children.FindAll(x => !x.Initialed)
                    .ForEach(x => x.OnInit());
                }
                Widget = CreateWidget();
        }

        virtual public void OnUpdate()
        {
        }
        virtual public void OnModelChanged()
        {
        }


        #endregion

        #region [[ViewNode]]
        virtual public void Add(IViewNode view)
        {
            children.Add(view);
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