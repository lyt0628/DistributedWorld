using UnityEngine;

namespace QS.Player
{
    class FrozenCameraState : CameraStateBase
    {
        public FrozenCameraState(CarmeraFSM carmeraFSM) : base(carmeraFSM)
        {
        }

        public override CameraState State => CameraState.Frozen;

        protected override Quaternion GetCameraOffsetRotation(Quaternion lastOffsetRotation)
        {
            return lastOffsetRotation;
        }
    }
}