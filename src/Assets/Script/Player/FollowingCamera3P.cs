using GameLib.DI;
using QS.GameLib.Uitl.RayCast;
using QS.GameLib.Util;
using QS.GameLib.Util.Raycast;
using QS.Player;
using QS.PlayerControl;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class FollowingCamera3P : MonoBehaviour
{
    [Injected]
    readonly PlayerControls playerControls;
    [Injected]
    readonly IPlayerData playerData;

    /*
     * The obstacledDistance between camera and target object
     */
    public float expectedDistance = 5f;
    float normalDistance = 6f;
    float realtimeDistance;
    float recoveryDistance;

    /*
     * Rotate speed/sensitive of Camera
     */
    public float sensitive = .3f;
    /*
     * The Delay of Camera rotate
     */
    public float rotateDelay = 5f;

    private Quaternion lastRotation = Quaternion.identity;

    const float minRotationX = -30;
    const float lookupShrinkThreshold = 0f;
    const float maxLookupShrinkRatio = 0.2f;
    const float cameraRadius = .3f;

    Vector3 eulerRotation = Vector3.zero;

    void Start()
    {
        PlayerGlobal.Instance.DI.Inject(this);
        playerControls.Player.View.performed += (ctx)=>
        {
            var value = ctx.ReadValue<Vector2>();
            eulerRotation.x = Mathf.Clamp(eulerRotation.x + value.y* sensitive, minRotationX, 80);
            eulerRotation.y = MathUtil.Clamp(eulerRotation.y + value.x * sensitive, -180, 180);

            // 往下旋转的话，更新距离，防止过多地被地面阻碍
            if(eulerRotation.x < lookupShrinkThreshold)
            {
                expectedDistance = normalDistance * maxLookupShrinkRatio  // 最多缩减到1/2
                                    + (eulerRotation.x - minRotationX) / (lookupShrinkThreshold - minRotationX) *normalDistance* (1- maxLookupShrinkRatio);  
            }
            else
            {
                expectedDistance = normalDistance;
            }
        };
        realtimeDistance = expectedDistance;
        recoveryDistance = expectedDistance;
    }

    // UpdateIfNeed is called once per frame
    void LateUpdate()
    {
        if (playerData.ActivedCharacter != null)
        {
            Rotate();
        }
    }


    private void Rotate()
    {
        var target = playerData.ActivedCharacter.transform;
        var targetPosition = target.position + target.up * 1.6f;
        var quat = Quaternion.Euler(eulerRotation);
        quat = Quaternion.Lerp(lastRotation, quat, rotateDelay * Time.deltaTime);
        lastRotation = quat;

        /// 更新巨鹿
        realtimeDistance = Mathf.Lerp(realtimeDistance, recoveryDistance, Time.smoothDeltaTime);
        transform.position = targetPosition - (quat * Vector3.forward * realtimeDistance);

        var target2Camera = transform.position - targetPosition;
        var raycast = RaycastHelper.Of(CastedObject
                                    .Sphere(targetPosition, .3f, target2Camera.normalized)
                                    .MaxDistance(expectedDistance)
                                    .IgnoreTrigger());
        if (raycast.Hit) // 被物体阻碍，更新距离到阻碍的物体
        {
            // 被一个更近的物体阻碍了， 立即更新
            if (raycast.Distance < realtimeDistance)
            {
                transform.position = raycast.Point - cameraRadius * target2Camera.normalized;
                realtimeDistance = raycast.Distance - cameraRadius;
            }
            else // 否则就把它当作回复的目标
            {
                recoveryDistance = raycast.Distance - cameraRadius;
            }
        }
        else
        {
            // 没有被阻碍就回复到初始距离
            recoveryDistance = expectedDistance;
        }

        //Debug.Log(realtimeDistance);
        transform.LookAt(targetPosition); // Use AimContraint Instead
    }

}
