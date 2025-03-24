

using QS.Api.Common.Util.Detector;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace QS.Common.Util.Detector
{
    class CollideDetector : ICollideDetector
    {
        Collider collider;
        bool enable = false;
        ReporterBehaviour<Collider> reporter;
        readonly CollideStage stage;
        readonly bool useTrigger;

        public bool Enabled => enable;


        public CollideDetector(CollideStage stage, bool useTrigger = true)
        {
            this.stage = stage;
            this.useTrigger = useTrigger;

        }
        public CollideDetector(Collider collider, CollideStage stage, bool useTrigger = true)
        {
            this.collider = collider;
            this.stage = stage;
            this.useTrigger = useTrigger;
            Assert.AreEqual(useTrigger, collider.isTrigger);
        }

        public void Enable()
        {
            enable = true;
            if (useTrigger)
            {
                reporter = stage switch
                {
                    CollideStage.Enter => collider.gameObject.AddComponent<TriggerEnterAndExitReporter>(),
                    CollideStage.Stay => collider.gameObject.AddComponent<TriggerStayReporter>(),
                    CollideStage.Exit => collider.gameObject.AddComponent<TriggerExitReporter>(),
                    CollideStage.EnterAndStay => collider.gameObject.AddComponent<TriggerEnterAndStayReporter>(),
                    CollideStage.EnterAndExit => collider.gameObject.AddComponent<TriggerEnterAndExitReporter>(),
                    CollideStage.StayAndExit => collider.gameObject.AddComponent<TriggerStayAndExitReporter>(),
                    _ => collider.gameObject.AddComponent<TriggerAllStageReporter>(),
                };
            }
            else
            {
                reporter = stage switch
                {
                    CollideStage.Enter => collider.gameObject.AddComponent<CollisionEnterReporter>(),
                    CollideStage.Stay => collider.gameObject.AddComponent<CollisionStayReporter>(),
                    CollideStage.Exit => collider.gameObject.AddComponent<CollisionExitReporter>(),
                    CollideStage.EnterAndStay => collider.gameObject.AddComponent<CollisionEnterAndStayReporter>(),
                    CollideStage.EnterAndExit => collider.gameObject.AddComponent<CollisionEnterAndExitReporter>(),
                    CollideStage.StayAndExit => collider.gameObject.AddComponent<CollisionStayAndExitReporter>(),
                    _ => collider.gameObject.AddComponent<CollisionAllStageReporter>(),
                };
            }
            reporter.Initialize();
        }

        public GameObject[] Detect()
        {
            if (!enable)
            {
                throw new InvalidOperationException("You cannot detect when CollideDetector is enabled");
            }
            if (collider == null)
            {
                throw new InvalidOperationException("[CollideDetector] Collider is null");
            }
            return reporter.Report()
                .Select(c => c.gameObject)
                .ToArray();
        }


        public void Disable()
        {
            if (enable && reporter != null)
            {
                GameObject.Destroy(reporter);

                //GameObject.DestroyImmediate(reporter);
            }
            enable = false;
        }

        public void SetCollider(Collider collider)
        {
            if (enable)
            {
                throw new InvalidOperationException("You cannot change collider when CollideDetector is enabled");
            }
            Assert.AreEqual(useTrigger, collider.isTrigger);
            this.collider = collider;
        }



    }
}