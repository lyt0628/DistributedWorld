using QS.Api.Presentation.Interact;
using UnityEngine;

namespace QS
{

    public class SphereInteractTrigger : AbstractInteractTrigger
    {
        public Vector3 position;
        public float radius;
        public override bool IsOneshot => true;

        public override bool TryTrig()
        {
            bool flag = false;
            if (Input.GetButtonDown("Interact"))
            {

                var hitColliders = Physics.OverlapSphere(position, radius);
                foreach (var hitCollider in hitColliders)
                {
                    if (hitCollider.gameObject.CompareTag("Player"))
                    {
                        flag = true;
                        break;
                    }
                }
            }

            if (flag)
            {

                foreach (var interactable in listners)
                {
                    interactable.Interact();
                }
            }

            return flag;
        }


    }
}