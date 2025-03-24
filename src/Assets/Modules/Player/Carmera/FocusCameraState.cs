

using UnityEngine;

namespace QS.Player
{
    class FocusCameraState : CameraStateBase
    {
        internal Transform focusTarget;

        public FocusCameraState(CarmeraFSM carmeraFSM) : base(carmeraFSM)
        {
        }

        public override CameraState State => CameraState.Focus;

        protected override Quaternion GetCameraOffsetRotation(Quaternion lastOffsetRotation)
        {
            var targetPosition = carmeraFSM.player.position + Vector3.up * 1.6f;
            var focusPosition = focusTarget.position + Vector3.up * 1f;

            var face = focusPosition - targetPosition;
            var targetRotation = Quaternion.LookRotation(face);
            return Quaternion.Lerp(lastOffsetRotation, targetRotation, Time.deltaTime);
        }
    }
}