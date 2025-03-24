using QS.Api.Executor.Domain;
using QS.Common.FSM;
using UnityEngine;

namespace QS.Player
{
    public class CarmeraFSM : FSMBehaviour<CameraState>
    {
        /// <summary>
        /// 对水平旋转的输入敏感度
        /// </summary>
        [Range(.5f, 3f)]
        public float horizontalSensitive = 1f;
        /// <summary>
        /// 对垂直方向旋转的输入敏感度
        /// </summary>
        [Range(.5f, 3f)]
        public float verticalSensitive = 1f;
        /// <summary>
        /// 一般情况下，相机到角色的距离
        /// </summary>
        [Range(3f, 8f)]
        public float normalDistance = 5f;

        FocusCameraState focusState;
        CameraStateBase free3PState;
        CameraStateBase fronzenState;
        public Transform player;

        public override ProcessTime ProcessTime => ProcessTime.LateUpdate;
        public override CameraState DefaultState => CameraState.Free3P;

        private void Start()
        {
            focusState = new FocusCameraState(this);
            free3PState = new Free3PCameraState(this);
            fronzenState = new FrozenCameraState(this);
        }



        public override bool CanHandle(IInstruction instruction)
            => instruction is ICarmeraFocusInstr or ICarmeraFollowInstr;

        public override IState<CameraState> GetState(CameraState state)
        {
            return state switch
            {
                CameraState.Free3P => free3PState,
                CameraState.Focus => focusState,
                CameraState.Frozen => fronzenState,
                _ => throw new System.NotImplementedException(),
            };
        }

        public override void Handle(IInstruction instruction)
        {
            switch (instruction)
            {
                case ICarmeraFocusInstr focusInstr:
                    HandleAimLockInstr(focusInstr);
                    break;
                case ICarmeraFollowInstr followInstr:
                    HandlerFollowInstr(followInstr);
                    break;
            }
        }

        private void HandlerFollowInstr(ICarmeraFollowInstr followInstr)
        {
            player = followInstr.Target;
            SwitchTo(CameraState.Free3P);
        }

        private void HandleAimLockInstr(ICarmeraFocusInstr focusInstr)
        {
            if (focusInstr.FocusTarget == null)
            {
                free3PState.lastOffsetRotation = focusState.lastOffsetRotation;
                SwitchTo(CameraState.Free3P);
            }
            else
            {
                focusState.lastOffsetRotation = free3PState.lastOffsetRotation;

                focusState.focusTarget = focusInstr.FocusTarget;
                SwitchTo(CameraState.Focus);
            }
        }
    }
}