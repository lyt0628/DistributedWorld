

using UnityEngine;

namespace GameLib
{
    class TransformFollow : MonoBehaviour
    {
        public Transform target;
        [SerializeField] private bool translationFollow = false;
        [SerializeField] private bool RotationFollow = false;

        private Vector3 _offset;
        private Quaternion _targetInitRotation;
        private Quaternion _initRotation;

        private void Start()
        {
            _offset = target.position - transform.position;
            _targetInitRotation = target.rotation;
            _initRotation = transform.rotation;
        }

        private void Update()
        {
            transform.position = target.position - _offset;

            var targetRotation = transform.rotation;
            if (!targetRotation.Equals(_targetInitRotation))
            {

            }
        }
    }
}