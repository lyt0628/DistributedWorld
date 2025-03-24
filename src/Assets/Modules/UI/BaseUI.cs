using QS.GameLib.View;
using UnityEngine;

namespace QS.UI
{
    /// <summary>
    /// œÏ”¶ Ω  ≈‰
    /// </summary>
    /// <typeparam name="TProp"></typeparam>
    /// <typeparam name="TState"></typeparam>
    public abstract class BaseUI<TProp, TState> : AbstractView<TProp, TState>
    {
        public override bool IsVisible => view.activeSelf;

        protected GameObject view;

        protected override void DoShow()
        {
            view.SetActive(true);
        }

        protected override void DoHide()
        {
            view.SetActive(false);
        }
    }

    public abstract class BaseUI : BaseUI<object, object>
    {
    }
}