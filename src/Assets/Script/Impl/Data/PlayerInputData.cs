
using QS.API;
using UnityEngine;

namespace QS.Impl
{
    class PlayerInputData : IPlayerInputData
    {
        public float Horizontal => Input.GetAxis("Horizontal");

        public float Vertical => Input.GetAxis("Vertical");
    }
}