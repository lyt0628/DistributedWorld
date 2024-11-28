using QS.Api.Data;
using UnityEngine;

namespace QS.Impl.Data
{
    public class PlayerInputData : IPlayerInputData
    {
        public float Horizontal => Input.GetAxis("Horizontal");

        public float Vertical => Input.GetAxis("Vertical");
    }
}