using UnityEngine;

namespace QS.Common
{
    public static class MiscUtil
    {
        public static void HideCursor()
        {
            UnityEngine.Cursor.visible = false;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        }
        public static void ShowCursor()
        {
            UnityEngine.Cursor.visible = true;
            UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        }
        //public static void CallAfterResourceStarted(IAsyncOpHandle initializer, UnityAction action)
        //{
        //    switch (initializer.ResourceStatus)
        //    {
        //        case ResourceInitStatus.Started:
        //            action?.Invoke();
        //            break;
        //        case ResourceInitStatus.Initializing:
        //            initializer.Completed.AddListener(action);
        //            break;
        //        default:
        //            initializer.Completed.AddListener(action);
        //            initializer.Initialize();
        //            break;
        //    }
        //}

        //public static void CallAfterResourcesStarted(IEnumerable<IAsyncOpHandle> initializers, UnityAction action)
        //{
        //    if (initializers.Count() == 0) throw new Exception();
        //    if (initializers.Count() == 1)
        //    {
        //        CallAfterResourceStarted(initializers.First(), action);
        //    }
        //    else
        //    {
        //        CallAfterResourcesStarted(initializers.Skip(1), action);
        //    }
        //}

        public static Vector3 Forward3p
        {
            get
            {
                var f = Camera.main.transform.forward;
                f.y = 0;
                return f.normalized;
            }
        }
        public static Vector3 Right3P
        {
            get
            {
                var f = Camera.main.transform.right;
                f.y = 0;
                return f.normalized;
            }
        }
    }
}