using GameLib.DI;
using GameLib.Uitl.RayCast;
using GameLib.Util.Raycast;
using QS.API;
using QS.API.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 角色碰撞检测是主要的变化点
/// 要把玩家控制设计成 事务脚本 还是 领域模型 
/// </summary>
/// 

[Scope(Value =ScopeFlag.Prototype)]
public class PlayerControllService : IPlayerControllService
{
    [Injected]
    IGlobalPhysicSetting globalPhysicSetting;

    [Injected]
    IPlayerLocationData characterLocationData;

    [Injected]
    IPlayerInputData playerInputData;

    bool jumping = false;
    float vertSpeed = 0f;
    public Quaternion GetRotation()
    {
        var vert =  playerInputData.Vertical;
        var hor = playerInputData.Horizontal;
        //var targetRotation = Quaternion.LookRotation(moveVec);

        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 
        //5f * Time.deltaTime);

        return Quaternion.identity;
    }

    public ICharacterTranslationDTO GetTranslation()
    {
        var horizontalDisp = Vector3.zero;
        var vertDisp = 0f;
        

        var collider = characterLocationData.Collider;
        var position = characterLocationData.Location;
        var baseUp = characterLocationData.Up;

        var hor = playerInputData.Horizontal;
        var vert = playerInputData.Vertical;
        var baseRight = characterLocationData.Right;
        var baseForward = characterLocationData.Forward;

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

        if(RaycastHelper
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

        if (!isGrounded) {
            vertSpeed += globalPhysicSetting.Gravity * Time.deltaTime;
        }

        if (!jumping && Input.GetButtonDown("Jump"))
        {
            jumping = true;
            vertSpeed = 10f;
        }

        vertDisp = vertSpeed * Time.deltaTime;

        if(RaycastHelper
            .Of(CastedObject
                    .Ray(position, Vector3.down)
                    .IgnoreTrigger())
            .IsCloserThan(- vertDisp))
        {
            vertSpeed = 0f;
            vertDisp = 0f;
            jumping = false;
        }


        var translationDTO = new CharacterTranslationDTO
        {
            Displacement = horizontalDisp + new Vector3(0, vertDisp, 0),
            Speed = horizontalVelocity.magnitude,
            Jumping = jumping
        };

        return translationDTO;    
    }
}
