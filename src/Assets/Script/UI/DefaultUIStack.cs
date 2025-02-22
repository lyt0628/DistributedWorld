

using QS.GameLib.View;
using System.Collections.Generic;

namespace QS.UI
{
    class DefaultUIStack : IUIStack
    {
        readonly Stack<IView> stack = new();

        public void Pop()
        {
            var view = stack.Pop();
            view.Hide();
        }

        public void Push(IView view)
        {
            stack.Push(view);
            view.Show();
        }
    }
}