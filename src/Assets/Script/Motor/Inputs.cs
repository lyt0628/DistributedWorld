

using UnityEngine;

namespace QS.Motor
{
    public class GrivityInput
    {
        public GrivityInput(Vector3 position) { 
            this.Position = position;
        }
        public Vector3 Position { get; }
    }


    public class MoveInput
    {
        public float horizontal;
        public float vertical;
        public float speedFactor = 1.0f;
        public Vector3 right;
        public Vector3 forward;
        public Vector3 up;
        public Vector3 position;
    }
}