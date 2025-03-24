

using QS.Common.FSM;
using UnityEngine;

namespace QS.Player
{
    /// <summary>
    /// ��������ƶ�����ת���ƶ�����ת�ǲ�ͬ�ģ��ֿ���
    /// �ȼ����ƶ�Ȼ�������ת��
    /// �ƶ�����ת�ڲ�ͬ��״̬���ǹ����
    /// 
    /// ���⻹�� Obstacle �ļ�⣬��ʵ�ϣ����ڳ����Ļ�ȷʵ�ǿ���
    /// �����ڳ����ڵ����壬һ�������黯����
    /// </summary>
    abstract class CameraStateBase : IState<CameraState>
    {
        public abstract CameraState State { get; }

        private const float moveSpeed = 10.0f;

        /// <summary>
        /// ��һ�ε����ƫ��
        /// </summary>
        public Quaternion lastOffsetRotation = Quaternion.identity;
        /// <summary>
        /// ��ǰ��������ƫ��
        /// </summary>
        public Vector3 CurrentOffset { get; private set; } = Vector3.zero;

        /// <summary>
        /// �����Ҫ�ظ����ľ���
        /// </summary>
        float recoveryDistance;
        /// <summary>
        /// û�б����������赲����£�������ɫ�ľ���
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
        /// �����������ɫ��ƫ��ʸ�� ���λ��= ��ɫ - ƫ��ʸ��
        /// </summary>
        /// <returns></returns>
        protected abstract Quaternion GetCameraOffsetRotation(Quaternion lastOffsetRotation);
        /// <summary>
        /// ���ӽ�ɫ�����������谭����
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