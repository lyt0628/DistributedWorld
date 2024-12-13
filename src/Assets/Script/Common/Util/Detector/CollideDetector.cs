

using QS.Api.Common.Util.Detector;
using QS.GameLib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace QS.Common.Util.Detector
{
    class CollideDetector : ICollideDetector
    {
        Collider collider;
        bool enable = false;
        AbstractColliderReporter reporter;
        CollideStage stage;
        public CollideDetector(CollideStage stage) 
        { 
            this.stage = stage;
        }
        public CollideDetector(Collider collider, CollideStage stage) 
        { 
            this.collider = collider; 
            this.stage = stage;
        }

        public void Enable()
        {
            enable = true;
            reporter = stage switch
            {
                CollideStage.Enter => collider.gameObject.AddComponent<ColliderEnterReporter>(),
                CollideStage.Stay => collider.gameObject.AddComponent<ColliderStayReporter>(),
                CollideStage.Exit => collider.gameObject.AddComponent<ColliderExitReporter>(),
                CollideStage.EnterAndStay => collider.gameObject.AddComponent<ColliderEnterAndStayReporter>(),
                CollideStage.EnterAndExit => collider.gameObject.AddComponent<ColliderEnterAndExitReporter>(),
                CollideStage.StayAndExit => collider.gameObject.AddComponent<ColliderStayAndExitReporter>(),
                _ => collider.gameObject.AddComponent<ColliderAllReporter>(),
            };
        }

        public IEnumerable<GameObject> Detect()
        {
            if (!enable)
            {
                throw new InvalidOperationException("You cannot detect when CollideDetector is enabled");
            }
            if(collider == null)
            {
                throw new Exception("[CollideDetector] Collider is null");
            }
            return reporter.Report().Select(c=>c.gameObject);
        }


        public void Disable()
        {
            if (enable && reporter != null)
            {
                GameObject.Destroy(reporter);
            }
            enable = false;
        }

        public void SetCollider(Collider collider)
        {
            if (enable)
            {
                throw new InvalidOperationException("You cannot change collider when CollideDetector is enabled");
            }
            this.collider = collider;
        }
    
        interface IColliderReporter
        {
            IEnumerable<Collider> Report();
            string UUID { get; }
        }

        abstract class AbstractColliderReporter : MonoBehaviour, IColliderReporter
        {
            List<Collider> colliders = new();
            public AbstractColliderReporter(string uuid) 
            {
                UUID = uuid;
            }
            public IEnumerable<Collider> Report()
            {
                var c = colliders;
                colliders = new();
                return c;
            }

            public string UUID { get; }
            protected void AddCollider(Collider collider)
            {
                colliders.Add(collider);
            }
        }

        class ColliderEnterReporter : AbstractColliderReporter
        {
            public ColliderEnterReporter(string uuid) : base(uuid)
            {
            }

            private void OnCollisionEnter(Collision collision)
            {
                AddCollider(collision.collider);
            }
        }

        class ColliderStayReporter : AbstractColliderReporter
        {
            public ColliderStayReporter(string uuid) : base(uuid)
            {
            }

            private void OnCollisionStay(Collision collision)
            {
                AddCollider(collision.collider);
            }
        }

        class ColliderExitReporter : AbstractColliderReporter
        {
            public ColliderExitReporter(string uuid) : base(uuid)
            {
            }

            private void OnCollisionExit(Collision collision)
            {
                AddCollider(collision.collider);
            }
        }

        class ColliderEnterAndStayReporter : AbstractColliderReporter
        {
            public ColliderEnterAndStayReporter(string uuid) : base(uuid)
            {
            }

            private void OnCollisionEnter(Collision collision)
            {
                AddCollider(collision.collider);
            }

            private void OnCollisionStay(Collision collision)
            {
                AddCollider(collision.collider);
            }
        }

        class ColliderEnterAndExitReporter : AbstractColliderReporter
        {
            public ColliderEnterAndExitReporter(string uuid) : base(uuid)
            {
            }

            private void OnCollisionEnter(Collision collision)
            {
                AddCollider(collision.collider);
            }

            private void OnCollisionExit(Collision collision)
            {
                AddCollider(collision.collider);
            }
        }
    
        class ColliderStayAndExitReporter : AbstractColliderReporter
        {
            public ColliderStayAndExitReporter(string uuid) : base(uuid)
            {
            }

            private void OnCollisionStay(Collision collision)
            {
                AddCollider(collision.collider);
            }
            private void OnCollisionExit(Collision collision)
            {
                AddCollider(collision.collider);
            }
        }

        class ColliderAllReporter : AbstractColliderReporter
        {
            public ColliderAllReporter(string uuid) : base(uuid)
            {
            }

            private void OnCollisionEnter(Collision collision)
            {
                AddCollider(collision.collider);
            }

            private void OnCollisionStay(Collision collision)
            {
                AddCollider(collision.collider);
            }

            private void OnCollisionExit(Collision collision)
            {
                AddCollider(collision.collider);
            }
        }
    }
}