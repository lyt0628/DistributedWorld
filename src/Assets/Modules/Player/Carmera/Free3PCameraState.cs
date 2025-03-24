

using QS.GameLib.Util;
using QS.PlayerControl;
using UnityEngine;

namespace QS.Player
{
    class Free3PCameraState : CameraStateBase
    {
        readonly PlayerControls playerInput;
        Vector3 offset = Vector3.zero;
        const float minRotationX = -30;
        const float lookupShrinkThreshold = 0f;
        const float maxLookupShrinkRatio = 0.2f;
        private const int maxVerCamlpAngle = 30;
        public float horClampAngle = 180;
        public float recoveryDamp = 10f;
        public float sensitive = .8f;
        float m_normalDistance;
        public override float NormalDistance => m_normalDistance;
        public Free3PCameraState(CarmeraFSM carmeraFSM) : base(carmeraFSM)
        {
            m_normalDistance = carmeraFSM.normalDistance;

            playerInput = PlayerGlobal.Instance.GetInstance<PlayerControls>();
            playerInput.Player.View.performed += (ctx) =>
            {
                var value = ctx.ReadValue<Vector2>();
                offset.x = Mathf.Clamp(offset.x + value.y * sensitive, minRotationX, maxVerCamlpAngle);
                offset.y = MathUtil.Clamp(offset.y + (value.x * sensitive), -horClampAngle, horClampAngle);

                // 往下旋转的话，更新距离，防止过多地被地面阻碍
                if (offset.x < lookupShrinkThreshold)
                {
                    m_normalDistance = carmeraFSM.normalDistance * maxLookupShrinkRatio  // 最多缩减到1/2
                                        + (offset.x - minRotationX) / (lookupShrinkThreshold - minRotationX) * carmeraFSM.normalDistance * (1 - maxLookupShrinkRatio);
                }
                else
                {
                    m_normalDistance = carmeraFSM.normalDistance;
                }
            };
        }

        public override CameraState State => CameraState.Free3P;

        protected override Quaternion GetCameraOffsetRotation(Quaternion lastOffsetRotation)
        {
            var targetOffsetDir = Quaternion.Euler(offset);
            return Quaternion.Lerp(lastOffsetRotation, targetOffsetDir, 10f * Time.deltaTime);
        }

        public override void Enter()
        {
            playerInput.Player.View.Enable();
            offset = lastOffsetRotation.eulerAngles;
        }
        public override void Exit()
        {
            playerInput.Player.View.Disable();
        }
    }
}