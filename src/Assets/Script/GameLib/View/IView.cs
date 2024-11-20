using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameLib.View
{
    public interface IView {
        bool IsVisible { get; }
        bool IsResident { get; }
        bool Initialed { get;  }
        /*
         * Init
         */
        void OnInit();

        /*
         * Put into use
         */

        void OnActive();

        /*
         * UpdateIfNeed Per Frame
         */
        void Show();

        void Preload();
        void OnUpdate();
        void OnModelChanged();

        /*
         * From Front to background
         */
        void Hide();

        void OnDeActive();

        /*
         * Release Resources
         */
        void OnRealse();

        IMessager GetMessager();

    }

}
