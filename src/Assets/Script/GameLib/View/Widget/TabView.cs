using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace GameLib.View
{
    public class TabView : AbstractView
    {
        public bool isVertical = false;
        private TabBar _tabBar;
        private TabContent _tabContent;

        protected override GameObject CreateWidget()
        {
            //var prefab = Resources.Load<GameObject>("UI");

            //return GameObject.Instantiate(prefab) as GameObject;

            var widget = new GameObject
            {
                name = "TabView"
            };
            Widget = widget;

            widget.AddComponent<RectTransform>();

            _tabBar = new TabBar(this);
            _tabContent = new TabContent();


            _tabBar.Widget.transform.SetParent(widget.transform, false);
            _tabContent.Widget.transform.SetParent(widget.transform, false);

            _tabBar.AddTab();
            return widget;
        }


        private class TabBar : AbstractView
        {
            public Sprite background;

            private TabView _tabView;
            private List<GameObject> tabs = new();
            public TabBar(TabView tabView)
            {
                _tabView = tabView;
                
                Preload();
            }
            public override void Preload()
            {
               Widget = CreateWidget();

            }

            protected override GameObject CreateWidget()
            {
                var tabBar = new GameObject
                {
                    name = "TabBar"
                };
                tabBar.AddComponent<RectTransform>();
                tabBar.AddComponent<CanvasRenderer>();

                tabBar.AddComponent<Image>();
                if (background)
                {
                    tabBar.GetComponent<Image>().sprite = background;
                }

                tabBar.AddComponent<ToggleGroup>();

                tabBar.AddComponent<ContentSizeFitter>();
                var sizeFiltter = tabBar.GetComponent<ContentSizeFitter>();
                if (_tabView.isVertical)
                {
                    sizeFiltter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
                }else
                {
                    sizeFiltter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                }

                tabBar.AddComponent<GridLayoutGroup>();
                var grid = tabBar.GetComponent<GridLayoutGroup>();
                if (_tabView.isVertical)
                {
                    grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
                    grid.constraintCount = 1;
                }else
                {
                    grid.constraint = GridLayoutGroup.Constraint.FixedRowCount;
                    grid.constraintCount = 1;
                }


                return tabBar; 
            }
 
            public void AddTab()
            {
                var background = new GameObject("Background");
                background.AddComponent<RectTransform>(); 
                background.AddComponent<CanvasRenderer>();
                background.AddComponent<Image>();

                var tab = new GameObject("Tab");
                tab.AddComponent<RectTransform>();
                tab.AddComponent<Toggle>();
                tab.GetComponent<Toggle>().group = Widget.GetComponent<ToggleGroup>();
                tab.GetComponent<Toggle>().targetGraphic = background.GetComponent<Image>();
        
                tab.transform.SetParent(Widget.transform, false);
                background.transform.SetParent(tab.transform, false);
                tabs.Add(tab);
            }
        }

        private class TabContent : AbstractView
        {
            public TabContent()
            {
                Preload();
            }
            public override void Preload()
            {
                Widget = CreateWidget();
            }
            protected override GameObject CreateWidget()
            {

                var content = new GameObject
                {
                    name = "TabContent"
                };

                return content; 
            }
        }
    }
}
