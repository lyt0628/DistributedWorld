

using QS.Common.FSM;
using UnityEngine;

namespace QS.Player
{
    /// <summary>
    /// 控制相机移动和旋转。移动和旋转是不同的，分开的
    /// 先计算移动然后计算旋转。
    /// 移动和旋转在不同的状态下是共享的
    /// 
    /// 此外还有 Obstacle 的检测，事实上，对于场景的话确实是靠近
    /// 但对于场景内的物体，一般是做虚化处理
    /// </summary>
    abstract class CameraStateBase : IState<CameraState>
    {
        public abstract CameraState State { get; }

        private const float moveSpeed = 10.0f;

        /// <summary>
        /// 上一次的相机偏移
        /// </summary>
        public Quaternion lastOffsetRotation = Quaternion.identity;
        /// <summary>
        /// 当前计算的相机偏移
        /// </summary>
        public Vector3 CurrentOffset { get; private set; } = Vector3.zero;

        /// <summary>
        /// 相机需要回复到的距离
        /// </summary>
        float recoveryDistance;
        /// <summary>
        /// 没有被其他物体阻挡情况下，相机离角色的距离
        /// </summary>
        public virtual float NormalDistance { get; }

        public ITransition<CameraState>[] Transitions => ITransition<CameraState>.Empty;

        protected readonly CarmeraFSM carmeraFSM;

        protected CameraStateBase(CarmeraFSM carmeraFSM)
        {
            this.carmeraFSM = carmeraFSM;
            NormalDistance = carmeraFSM.normalDistance;
            recoveryDistance = NormalDistance;
        }

        public virtual void Enter()
        {
        }
        public virtual void Exit()
        {
        }
        /// <summary>
        /// 计算相机到角色的偏移矢量 相机位置= 角色 - 偏移矢量
        /// </summary>
        /// <returns></returns>
        protected abstract Quaternion GetCameraOffsetRotation(Quaternion lastOffsetRotation);
        /// <summary>
        /// 检测从角色到相机的最近阻碍物体
        /// </summary>
        /// <param name="player"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        protected virtual bool TryGetNearObstacle(Vector3 offsetDir, out Transform obstalce)
        {
            obstalce = default;
            return false;
        }

        protected virtual float RecoverDistance(float currentDistance, float targetDistance)
        {
            return Mathf.Lerp(currentDistance, targetDistance, moveSpeed * Time.deltaTime);
        }
        protected virtual Quaternion GetCameraRotation(Vector3 playerPosition)
        {
            var camera2Target = playerPosition - carmeraFSM.transform.position;
            return Quaternion.LookRotation(camera2Target);

            //return Quaternion.Lerp(carmeraFSM.transform.rotation, targetRotation, 10f * Time.deltaTime);
        }

        public virtual void Process()
        {
            var fixPlayerPosition = carmeraFSM.player.position + 1.6f * Vector3.up;
            var currentDistance = Vector3.Distance(carmeraFSM.transform.position, fixPlayerPosition);
            var OffsetRotation = GetCameraOffsetRotation(lastOffsetRotation).normalized;
            if (TryGetNearObstacle(OffsetRotation.eulerAngles, out var obstalce))
            {
                var obstacleDistance = Vector3.Distance(obstalce.position, fixPlayerPosition);
                if (obstacleDistance < recoveryDistance)
                {
                    recoveryDistance = obstacleDistance;
                }
                else
                {
                    recoveryDistance = RecoverDistance(currentDistance, obstacleDistance - 0.3f);
                }
            }
            else
            {
                recoveryDistance = RecoverDistance(currentDistance, NormalDistance - 0.3f);
            }

            carmeraFSM.transform.position = fixPlayerPosition - recoveryDistance * (OffsetRotation * Vector3.forward);
            carmeraFSM.transform.rotation = GetCameraRotation(fixPlayerPosition);

            lastOffsetRotation = OffsetRotation;
        }


    }
}