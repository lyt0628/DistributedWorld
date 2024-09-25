

using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace GameLib.View
{
    public class ViewNode : AbstractView
    {
        override public bool IsVisible => false;

        private readonly List<IView> views = new();

        override public void Add(IView view)
        {
            
            views.Add(view);
        }
        override  public void OnActive()
        {
            foreach (var view in views)
            {
                view.OnActive();
            }
        }

       override  public void OnDeActive()
        {
            foreach(var view in views)
            {
                view.OnDeActive();
            }
        }

       override  public void OnRealse()
        {
            foreach( var view in views)
            {
                view.OnRealse();
            }
        }

       override  public void OnInit()
        {
            foreach(var view in views)
            {
                view.OnInit();
            }
        }

       override  public void OnUpdate()
        {
            foreach(var view in views)
            {
                view.OnUpdate();
            }
        }

       override  public void Show()
        {
            foreach(var view in views)
            {
                view.OnUpdate();
            }
        }

       override  public void Hide()
        {
            foreach(var view in views)
            {
                view.Hide();
            }
        }

    }
}