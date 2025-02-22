

using QS.Api.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

namespace QS.Common
{
    public static class MiscUtil
    {
        public static void CallAfterResourceStarted(IResourceInitializer initializer, UnityAction action)
        {
            switch (initializer.ResourceStatus)
            {
                case ResourceInitStatus.Started:
                    action?.Invoke();
                    break;
                case ResourceInitStatus.Initializing:
                    initializer.OnReady.AddListener(action);
                    break;
                default:
                    initializer.OnReady.AddListener(action);
                    initializer.Initialize();
                    break;
            }
        }

        public static void CallAfterResourcesStarted(IEnumerable<IResourceInitializer> initializers, UnityAction action)
        {
            if (initializers.Count() == 0) throw new Exception();
            if (initializers.Count() == 1)
            {
                CallAfterResourceStarted(initializers.First(), action);
            }
            else
            {
                CallAfterResourcesStarted(initializers.Skip(1), action);
            }
        }
    }
}