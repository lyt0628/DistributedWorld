using QS.GameLib.Pattern;
using System.Collections.Generic;

namespace QS.Api.Presentation.Interact
{

    public abstract class AbstractInteractTrigger : IInteractTrigger
    {
        #region [[Listener]]
        private readonly ListenableWrapper<IInteractable> wrapper = new();
        protected List<IInteractable> listners
        {
            get { return wrapper.Listeners; }
        }

        public virtual void AddListener(IInteractable listener)
        {
            wrapper.AddListener(listener);
        }
        public virtual void RemoveListener(IInteractable listener)
        {
            wrapper.RemoveListener(listener);
        }

        #endregion

        public abstract bool IsOneshot { get; }

        public abstract bool TryTrig();
    }
}