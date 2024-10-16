using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameLib.View
{
    public interface IView {
        bool IsVisible { get; }
        bool IsResident { get; }
        /*
         * Init
         */
        void OnInit();

        /*
         * Put into use
         */

        void OnActive();

        /*
         * Update Per Frame
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
