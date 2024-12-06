


using GameLib.DI;
using QS.Api.Control.Service;
using QS.Api.Control.Service.DTO;
using QS.Api.Setting;
using QS.Control.Domain;
using QS.GameLib.Rx.Relay;
using QS.GameLib.Uitl.RayCast;
using QS.GameLib.Util.Raycast;
using QS.Impl.Service.DTO;
using UnityEngine;

namespace QS.Control.Service
{
    class ControlledPointService : IControlledPointService
    {
        [Injected]
        readonly IGlobalPhysicSetting globalPhysic;


        [Injected]
        readonly IControlledPointDataSource_tag datasource;

        public Relay<ICharacterTranslationDTO> GetTranslation(string uuid)
        {
            return  datasource
                .Get(uuid)
                .Map(p =>
            {
                return ComputeTranslation(p);
                });
        }


        ICharacterTranslationDTO ComputeTranslation(IControlledPoint point)
        {
            // Input
            var pos = point.PointData.Position;
            var R = point.PointData.BaseRight;
            var F = point.PointData.Baseforword;
            var hor = point.PointData.Horizontal;
            var vert = point.PointData.Vertical;
            var jumpInstruction = point.PointData.Jump;

            var vRay = RaycastHelper
                            .Of(CastedObject
                                    .Ray(pos, Vector3.down)
                                    .IgnoreTrigger());


            var hVelocity = vert * F + hor * R;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                hVelocity *= 4;
            }
            var hDisp = Time.deltaTime * hVelocity;

            var isGrounded = point.VerticalSpeed <= 0 && vRay.IsCloserThan(globalPhysic.ErrorTolerance);
            var isGroundedTolerance = point.VerticalSpeed <= 0 && vRay.IsCloserThan(globalPhysic.HalfErrorTolerance);

            if (isGrounded)
            {
                if (isGroundedTolerance)
                {
                    point.VerticalSpeed = 0;
                }
                if (jumpInstruction)
                {
                    point.VerticalSpeed = 10f;
                }
            }
            else
            {
                point.VerticalSpeed += globalPhysic.Gravity * Time.deltaTime;
            }
            var vDisp = point.VerticalSpeed * Time.deltaTime;

            var disp = new Vector3(hDisp.x, vDisp, hDisp.z);
            disp = hDisp;
            var dispRay = RaycastHelper
                            .Of(CastedObject
                                    .Ray(pos, disp)
                                    .IgnoreTrigger());
            disp = Vector3.ProjectOnPlane(disp, dispRay.Normal);
            disp.y += vDisp;

            var a = RaycastHelper
                            .Of(CastedObject
                                    .Ray(pos, disp.y * Vector3.up)
                                    .IgnoreTrigger());
          
            if (a.IsCloserThanOrJust(-disp.y))
            {
                disp.y = -a.Distance + globalPhysic.HalfErrorTolerance;
            }

            var b = RaycastHelper
                            .Of(CastedObject
                                    .Ray(pos, disp)
                                    .IgnoreTrigger());
            if (b.IsCloserThanOrJust(disp.magnitude))
            {
                disp.z = 0f;
                disp.x = 0f;
            }

            Debug.Log($"IsGrounded:{isGrounded}\t VertSpeed:{point.VerticalSpeed} \n" +
                        $"\t Disp:{disp}\t Pos: {pos}");
            var translation = ControlGlobal.Instance.DI.GetInstance<CharacterTranslationDTO>();
            translation.Displacement = disp;
            translation.Speed = hVelocity.magnitude;
            translation.Jumping = false;
            return translation;
        }

    }
}