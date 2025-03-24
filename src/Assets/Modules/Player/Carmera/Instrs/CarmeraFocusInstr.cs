



using QS.Api.Executor.Domain;
using UnityEngine;

namespace QS.Player
{
    public interface ICarmeraFocusInstr : IInstruction
    {
        /// <summary>
        /// Ëø¶¨Ä¿±ê
        /// </summary>
        Transform FocusTarget { get; set; }
    }

    struct CarmeraFocusInstr : ICarmeraFocusInstr
    {
        public Transform FocusTarget { get; set; }
    }
}