using GameLib.DI;
using QS.Api.Data;
using QS.Api.Setting;
using UnityEngine;

namespace QS.Impl.Data
{

    public class PlayerInstructionData : IPlayerInstructionData
    {

        [Injected]
        readonly IPlayerInstructionSetting playerInstruction;

        public bool Jump => Input.GetButtonDown(playerInstruction.JUMP);
    }
}