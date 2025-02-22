

using QS.Api.Common;
using QS.GameLib.Util;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace QS.Common.Util.Detector
{

    abstract class ReporterBehaviour<T> : MonoBehaviour, IReporter<T>, IResourceInitializer
    {

        List<T> colliders;
        public string UUID { get; private set; }

        public IEnumerable<T> Report()
        {
            
            var res = colliders.ToArray();
            colliders.Clear();
            return res;
        }


        public ResourceInitStatus ResourceStatus { get; private set; } = ResourceInitStatus.Initializing;

        public UnityEvent OnReady { get; } = new();

        protected void Add(T collider)
        {
            colliders.Add(collider);
        }

        public void Initialize()
        {
            colliders ??= new List<T>();
            UUID ??= MathUtil.UUID();
            ResourceStatus = ResourceInitStatus.Started;
            OnReady.Invoke();
        }
    }
}