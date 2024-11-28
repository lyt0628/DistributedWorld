using GameLib.DI;
using QS.Api.Data;
using QS.Api.Service;
using QS.Api.Service.DTO;
using QS.Api.Setting;
using QS.GameLib.Uitl.RayCast;
using QS.GameLib.Util.Raycast;
using QS.Impl.Service.DTO;
using UnityEngine;



namespace QS.Impl.Service
{

    /// <summary>
    /// 角色碰撞检测是主要的变化点
    /// 要把玩家控制设计成 事务脚本 还是 领域模型 
    /// </summary>
    /// 
    [Scope(Value = ScopeFlag.Prototype)]
    public class PlayerControllService : IPlayerControllService
    {

        [Injected]
        readonly IGlobalPhysicSetting globalPhysic;

        [Injected]
        readonly IPlayerInstructionData playerInstruction;

        [Injected]
        readonly IPlayerLocationData playerLocation;

        [Injected]
        readonly IPlayerInputData playerInput;

        [Injected]
        readonly CharacterTranslationDTO translationDTO;

        bool jumping = false;
        float vertSpeed = 0f;

        Quaternion rotation = Quaternion.identity;
        public Quaternion GetRotation()
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

        public ICharacterTranslationDTO GetTranslation()
        {
            var horizontalDisp = Vector3.zero;
            var vertDisp = 0f;

            var collider = playerLocation.Collider;
            var position = playerLocation.Location;
            var baseUp = playerLocation.Up;

            var hor = playerInput.Horizontal;
            var vert = playerInput.Vertical;

            var baseRight = playerLocation.Right;
            var baseForward = playerLocation.Forward;

            var horizontalVelocity = hor * baseRight + vert * baseForward;
            horizontalVelocity.y = 0f;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                horizontalVelocity *= 4;
            }
            horizontalDisp = horizontalVelocity * Time.deltaTime;

            Vector3 point1 = position;
            Vector3 point2 = point1 + collider.height * baseUp;
            float redius = collider.radius;

            var castedCapsule = CastedObject
                .Capsule(point1, point2, redius,
                        horizontalDisp.normalized)
                .IgnoreTrigger();

            if (RaycastHelper
                 .Of(castedCapsule)
                 .IsCloserThan(0.01f))
            {
                //horizontalVelocity = Vector3.zero; for Action Infomation
                horizontalDisp = Vector3.zero;
            }

            // Jump Logic
            var isGrounded = RaycastHelper
                .Of(CastedObject.Ray(position, Vector3.down))
                .IsCloserThan(0.01f);

            if (!isGrounded)
            {
                vertSpeed += globalPhysic.Gravity * Time.deltaTime;
            }

            if (!jumping && playerInstruction.Jump)
            {
                jumping = true;
                vertSpeed = 10f;
            }

            vertDisp = vertSpeed * Time.deltaTime;

            if (RaycastHelper
                .Of(CastedObject
                        .Ray(position, Vector3.down)
                        .IgnoreTrigger())
                .IsCloserThan(-vertDisp))
            {
                vertSpeed = 0f;
                vertDisp = 0f;
                jumping = false;
            }


            translationDTO.Displacement = horizontalDisp + new Vector3(0, vertDisp, 0);
            translationDTO.Speed = horizontalVelocity.magnitude;
            translationDTO.Jumping = jumping;
            return translationDTO;
        }
    }
}


