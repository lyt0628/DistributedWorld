
using QS.API;
using UnityEngine;

namespace QS
{
    class DestroySelfWhenInteract : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
            Destroy(gameObject);
        }
    }
}