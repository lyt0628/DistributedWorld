//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;
//using UnityEngine.UI;

//namespace GameLib.View
//{
//    public class TabView : AbstractView
//    {
//        public override bool IsLeaf => false;
//        public bool isVertical = true;
//        protected TabBar tabBar;
//        protected TabContent tabContent;

//        public TabView()
//        {
//            tabBar = new TabBar(this);
//            tabContent = new TabContent(this);

//            Add(tabBar);
//            Add(tabContent);
//        }

//        protected override GameObject CreateWidget()
//        {
//            //var prefab = Resources.Load<GameObject>("UI");

//            //return GameObject.Instantiate(prefab) as GameObject;

//            var widget = new GameObject
//            {
//                name = "TabView"
//            };

//            widget.AddComponent<RectTransform>();
//            var sizeFilter = widget.AddComponent<ContentSizeFitter>();
//            sizeFilter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
//            sizeFilter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;

//            if (isVertical) {
//                widget.AddComponent<VerticalLayoutGroup>();
//            }
//            else
//            {
//                widget.AddComponent<HorizontalLayoutGroup>();
//            }

//            tabBar.Widget.transform.SetParent(widget.transform);
//            tabContent.Widget.transform.SetParent(widget.transform);

//            return widget;
//        }

//        public override void OnInit()
//        {
//            base.OnInit();

//            tabBar.AddTab();
//            tabBar.AddTab();
//            tabBar.AddTab();
//            tabBar.AddTab();
//            tabBar.AddTab();

//            var canvas = GameObject.Find("Canvas");
//            Widget.transform.SetParent(canvas.transform);

//            Hide();
//        }

//        protected class TabBar : AbstractView
//        {
//            public Sprite background;
//            protected TabView _tabView;

//            protected List<GameObject> tabs = new();

//            public TabBar(TabView tabView)
//            {
//                _tabView = tabView;

//            }

//            protected override GameObject CreateWidget()
//            {
//                var tabBar = new GameObject
//                {
//                    name = "TabBar"
//                };


//                tabBar.AddComponent<RectTransform>();
//                tabBar.AddComponent<CanvasRenderer>();

//                tabBar.AddComponent<Image>();
//                if (background)
//                {
//                    tabBar.GetComponent<Image>().sprite = background;
//                }

//                tabBar.AddComponent<ToggleGroup>();

//                //tabBar.AddComponent<ContentSizeFitter>();
//                //var sizeFiltter = tabBar.GetComponent<ContentSizeFitter>();
//                //if (_tabView.isVertical)
//                //{
//                //    sizeFiltter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
//                //}else
//                //{
//                //    sizeFiltter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
//                //}

//                tabBar.AddComponent<GridLayoutGroup>();
//                var grid = tabBar.GetComponent<GridLayoutGroup>();
//                if (_tabView.isVertical)
//                {
//                    grid.constraint = GridLayoutGroup.Constraint.FixedRowCount;
//                    grid.constraintCount = 1;
//                }else
//                {
//                    grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
//                    grid.constraintCount = 1;
//                }

//                return tabBar; 
//            }


//            public void AddTab()
//            {
//                var background = new GameObject("Background");
//                background.AddComponent<RectTransform>(); 
//                background.AddComponent<CanvasRenderer>();
//                background.AddComponent<Image>();

//                var tab = new GameObject("Tab");
//                tab.AddComponent<RectTransform>();
//                tab.AddComponent<Toggle>();
//                tab.GetComponent<Toggle>().group = Widget.GetComponent<ToggleGroup>();
//                tab.GetComponent<Toggle>().targetGraphic = background.GetComponent<Image>();

//                tab.transform.SetParent(Widget.transform);
//                background.transform.SetParent(tab.transform);
//                Debug.Log("XXXXXXXXXXXXXXXXXXXXXXXXXXXXX"+tab);
//                Debug.Log(background);
//                tabs.Add(tab);
//            }
//        }


//        protected class TabContent : AbstractView
//        {
//            protected TabView _tabView;
//            public TabContent(TabView tabView)
//            {
//                _tabView = tabView;
//            }


//            protected override GameObject CreateWidget()
//            {

//                var content = new GameObject
//                {
//                    name = "TabContent"
//                };
//                content.AddComponent<RectTransform>();

//                content.AddComponent<CanvasRenderer>();
//                var lo = content.AddComponent<LayoutElement>();
//                lo.preferredWidth = 100;
//                lo.preferredHeight = 100;

//                content.AddComponent<Image>();
//                return content; 
//            }
//        }
//    }
//}
