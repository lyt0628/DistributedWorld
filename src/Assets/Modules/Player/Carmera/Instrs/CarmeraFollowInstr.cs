


using UnityEngine;

namespace QS.Player
{
    public interface ICarmeraFollowInstr
    {
        Transform Target { get; set; }
    }

    struct CarmeraFollowInstr : ICarmeraFollowInstr
    {
        public Transform Target { get; set; }
    }
}