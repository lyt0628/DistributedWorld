

using QS.API;
using UnityEngine;

namespace QS
{

    class SphereInteractTrigger : AbstractInteractTrigger

    {
        public Transform item;
        public float radius;
        public override bool IsOneshot => true;


        public override bool TryTrig()
        {
            bool flag = false;
            if (Input.GetButtonDown("Interact"))
            {

                var hitColliders = Physics.OverlapSphere(item.position, radius);
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