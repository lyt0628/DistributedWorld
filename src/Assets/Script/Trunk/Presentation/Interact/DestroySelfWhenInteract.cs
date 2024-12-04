using UnityEngine;
using QS.Api.Presentation.Interact;

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