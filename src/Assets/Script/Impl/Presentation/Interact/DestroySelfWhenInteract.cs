
using QS.Api.Presentation.Interact;
using UnityEngine;

namespace QS
{
    public class DestroySelfWhenInteract : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
            Destroy(gameObject);
        }
    }
}