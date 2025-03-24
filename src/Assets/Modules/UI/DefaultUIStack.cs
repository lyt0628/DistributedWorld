using QS.GameLib.View;
using System.Collections.Generic;

namespace QS.UI
{
    /// <summary>
    /// ���ӵ��첽����¿϶��������
    /// TODO��Fade ��UI��Ч
    /// </summary>
    class DefaultUIStack : IUIStack
    {
        readonly Stack<IView> stack = new();
        //readonly Queue<UniTaskCompletionSource> workingViews = new();

        public void Pop()
        {
            var view = stack.Pop();
            view.Hide();

            if (stack.TryPeek(out var preView))
            {
                preView.Show();
            }

        }


        public void Push(IView view)
        {
            if (stack.TryPeek(out var preView))
            {
                if (preView == view) return;
                preView.Hide();
            }
            view.Show();
            stack.Push(view);
        }
    }
}