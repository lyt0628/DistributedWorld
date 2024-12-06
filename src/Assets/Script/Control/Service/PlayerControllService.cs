using GameLib.DI;
using QS.Api;
using QS.Api.Control.Service.DTO;
using QS.Api.Data;
using QS.Api.Service;
using QS.Api.Setting;
using QS.Control;
using QS.GameLib.Rx.Relay;
using QS.GameLib.Uitl.RayCast;
using QS.GameLib.Util.Raycast;
using QS.Impl.Service.DTO;
using UnityEngine;



namespace QS.Impl.Service
{

    /// <summary>
    /// 一般性的玩家控制根本做不到, 但我至少可以提供一个点的控制来做位移和旋转控制
    /// 因为角色的局部坐标原点都在底部, 大部分时候已经可以满足需求了
    /// 角色碰撞检测是主要的变化点
    /// 要把玩家控制设计成 事务脚本 还是 领域模型 
    /// </summary>
    /// 
    [Scope(Value = ScopeFlag.Prototype)]
    class PlayerControllService : IPlayerControllService
    {

        // q
        [Injected]
        readonly IGlobalPhysicSetting globalPhysic;

        //q
        [Injected]
        readonly IPlayerInstructionData playerInstruction;

        //q
        [Injected]
        readonly IPlayerLocationData playerLocation;

        //q
        [Injected]
        readonly IPlayerInputData playerInput;


        [Injected]
        readonly CharacterTranslationDTO translation;

        bool jumping = false;
        readonly float minFallSpeed = -1.5f;
        readonly float maxFallSpeed = -10f;
        float vertSpeed = -0f;

        Quaternion rotation = Quaternion.identity;
        readonly IMotion tMotion;
        readonly IMotion rMotion;
        readonly Relay<ICharacterTranslationDTO> tRelay;
        readonly Relay<Quaternion> rRelay;
        public PlayerControllService()
        {
            tRelay = Relay<ICharacterTranslationDTO>
                     .Tick(() => ComputeTranslation(), out tMotion);
            rRelay = Relay<Quaternion>
                     .Tick(()=>ComputeRotation(), out rMotion);
 
           var life = ControlGlobal.Instance.DI.GetInstance<ILifecycleProivder>();
            life.Request(Lifecycles.Update, () =>
            {
                tMotion.Set();
                rMotion.Set();
            });
        }

        public Relay<ICharacterTranslationDTO> GetTranslation()=> tRelay;
        public Relay<Quaternion> GetRotation() => rRelay;


        Quaternion ComputeRotation()
        {

            var vert = playerInput.Vertical;
            var hor = playerInput.Horizontal;
            var baseRight = playerLocation.Right;
            var baseForward = playerLocation.Forward;

            var face = hor * baseRight + vert * baseForward;
            if (face.magnitude == 0f) return rotation;
            face.y = 0;
            face = face.normalized;


            // Do not delete this Comments
            //
            //Quaternion tmp = camera.rotation;
            //camera.eulerAngles = new Vector3(0, camera.eulerAngles.y, 0);
            //moveVec = camera.TransformDirection(moveVec);


            rotation = Quaternion.LookRotation(face);

            return Quaternion.Slerp(playerLocation.Rotation,
                rotation,
                5f * Time.deltaTime);
        }


        //bool CollideTest(Vector3 direction)
        //{
        //    var collider = playerLocation.Collider;
        //    var position = playerLocation.Location ;
        //    var baseUp = playerLocation.Up;


        //    float redius = collider.radius;
        //    Vector3 point1 = position + redius * Vector3.up;
        //    Vector3 point2 = position + collider.height * baseUp - redius * Vector3.up;
        //    if (RaycastHelper
        //         .Of(CastedObject
        //                .Capsule(point1, point2, redius, direction.normalized)
        //                .IgnoreTrigger())
        //         .IsCloserThan(0.01f))
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        ICharacterTranslationDTO ComputeTranslation()
        {
            // Input
            var pos = playerLocation.Location;
            var R = playerLocation.Right;
            var F = playerLocation.Forward;
            var hor = playerInput.Horizontal;
            var vert = playerInput.Vertical;
            var jumpInstruction = playerInstruction.Jump;

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

            var isGrounded = vertSpeed <= 0 && vRay.IsCloserThan(globalPhysic.ErrorTolerance);
            var isGroundedTolerance = vertSpeed <= 0 && vRay.IsCloserThan(globalPhysic.HalfErrorTolerance);

            if (isGrounded)
            {
                if (isGroundedTolerance)
                {
                    vertSpeed = 0;
                }
                if (jumpInstruction)
                {
                    vertSpeed = 10f;
                }
            }
            else
            {
                vertSpeed += globalPhysic.Gravity * Time.deltaTime;
            }
            var vDisp = vertSpeed * Time.deltaTime;

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

            Debug.Log($"IsGrounded:{isGrounded}\t VertSpeed:{vertSpeed} \n" +
                        $"\t Disp:{disp}\t Pos: {pos}");

            translation.Displacement = disp;
            translation.Speed = hVelocity.magnitude;
            translation.Jumping = jumping;
            return translation;
        }


      
    }
}


