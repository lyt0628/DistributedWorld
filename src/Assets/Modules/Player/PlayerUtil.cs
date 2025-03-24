
using UnityEngine;

namespace QS.Player
{
    public static class PlayerUtil
    {
        public static void FrozeCurrentCharacter()
        {
            var player = PlayerGlobal.Instance.GetInstance<IPlayer>();
            player.ActiveChara.Frozen = true;
            var cameraFSM = Camera.main.GetComponent<CarmeraFSM>();
            cameraFSM.SwitchTo(CameraState.Frozen);
        }

        public static void UnfrozeCurrentCharacter()
        {
            var player = PlayerGlobal.Instance.GetInstance<IPlayer>();
            player.ActiveChara.Frozen = false;
            var cameraFSM = Camera.main.GetComponent<CarmeraFSM>();
            cameraFSM.SwitchTo(CameraState.Free3P);
        }
    }
}