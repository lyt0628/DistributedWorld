

using QS.GameLib.Uitl.RayCast;
using QS.GameLib.Util.Raycast;
using System.Linq;
using UnityEngine;

namespace QS.Chara
{
    public class DefaultCharaControlPhysicalProbe : ICharaControlPhysicalProbe
    {
        private const float radius = 0.3f;
        private const float dotValueNearOne2 = 0.95f;
        private const float dotValueNearOne = 0.7f;
        readonly CharaControlTemplate controlFSM;
        public Transform target;
        public CapsuleCollider Colli;
        public DefaultCharaControlPhysicalProbe(CharaControlTemplate ctrlTpl)
        {
            this.controlFSM = ctrlTpl;
            target = ctrlTpl.transform;
            Colli = ctrlTpl.GetComponent<CapsuleCollider>();
        }

        public bool IsGrounded
        {
            get
            {
                return RaycastHelperAll.Of(CastedObject
                .Ray(controlFSM.transform.position, -controlFSM.meta.up())
                .IgnoreTrigger()
                .MaxDistance(0.05f))
                .AllColliders
                .Where(o => o.gameObject != controlFSM.gameObject
                                    && !o.transform.IsChildOf(controlFSM.transform))
                                    .Any();
            }
        }

        public bool IsGroundedStrict =>
            RaycastHelperAll.Of(CastedObject
                .Ray(controlFSM.transform.position, -controlFSM.meta.up())
                .IgnoreTrigger()
                .MaxDistance(0.025f))
                .AllColliders
                .Where(o => o.gameObject != controlFSM.gameObject
                                    && !o.transform.IsChildOf(controlFSM.transform))
                                    .Any();


        public float GroundSlope
        {
            get
            {
                var helper = RaycastHelper.Of(CastedObject
                 .Ray(target.position, Vector3.down)
                 .IgnoreTrigger()
                 .MaxDistance(0.1f));

                return Vector3.Angle(Vector3.up, helper.Normal);
            }
        }

        public bool InStep
        {
            get
            {
                var footHelp = RaycastHelper.Of(CastedObject
                 .Ray(target.position, Vector3.forward)
                 .IgnoreTrigger()
                 .MaxDistance(radius));
                var offsetHelp = RaycastHelper.Of(CastedObject
                 .Ray(target.position + 0.2f * Vector3.up, Vector3.forward)
                 .IgnoreTrigger()
                 .MaxDistance(radius));
                var stepHelp = RaycastHelper.Of(CastedObject
                 .Ray(target.position + 0.2f * Vector3.up + radius * Vector3.forward, Vector3.down)
                 .IgnoreTrigger()
                 .MaxDistance(radius));
                return footHelp.Hit
                    && (!offsetHelp.Hit || Vector3.Dot(offsetHelp.Normal, -target.forward) > dotValueNearOne)
                    && Vector3.Dot(stepHelp.Normal, Vector3.up) > dotValueNearOne2;
            }
        }


        public bool IsBlockedInDir(Vector3 dir)
        {
            var hit = RaycastHelper.Of(CastedObject
                .Ray(target.position + 0.2f * Vector3.up, Vector3.forward)
                .MaxDistance(radius)).Hit;
            if (hit)
            {
                hit = RaycastHelper.Of(CastedObject
                     .Sphere(target.position + 1.7f * Vector3.up + radius * Vector3.forward, .3f, Vector3.down)
                     .MaxDistance(1.7f))
                     .Hit;
            }
            return hit;
        }

        public bool IsWallInDir(Vector3 dir, out float height)
        {
            throw new System.NotImplementedException();
        }

        public static bool GroundedCheck(Vector3 position, Vector3 downDir, Vector3 forwardDir, Vector3 rightDir, float error)
        {
            var downRay = RaycastHelper
                .Of(CastedObject
                        .Ray(position, downDir)
                        .IgnoreTrigger());

            var fwdRay = RaycastHelper
                .Of(CastedObject
                        .Ray(position, (downDir + forwardDir).normalized)
                        .IgnoreTrigger());

            var leftRay = RaycastHelper
                .Of(CastedObject
                        .Ray(position, (downDir - rightDir).normalized)
                        .IgnoreTrigger());

            var rightRay = RaycastHelper
                    .Of(CastedObject
                            .Ray(position, (downDir + rightDir).normalized)
                            .IgnoreTrigger());

            var backRay = RaycastHelper
                    .Of(CastedObject
                            .Ray(position, (downDir - forwardDir).normalized)
                            .IgnoreTrigger());

            var isGrounded = downRay.IsCloserThan(error)
                            || fwdRay.IsCloserThan(error)
                            || leftRay.IsCloserThan(error)
                            || rightRay.IsCloserThan(error)
                            || backRay.IsCloserThan(error);
            return isGrounded;
        }
    }
}