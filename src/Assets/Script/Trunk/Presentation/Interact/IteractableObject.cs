using QS;
using UnityEngine;


[RequireComponent(typeof(DestroySelfWhenInteract))]
[RequireComponent(typeof(AttackedWhenInteract))]
public class InteractalbleObject : MonoBehaviour
{
    SphereInteractTrigger trigger;

    private void Start()
    {
        trigger = new SphereInteractTrigger
        {
            position = transform.position,
            radius = 1.5f
        };

        trigger.AddListener(GetComponent<DestroySelfWhenInteract>());
        trigger.AddListener(GetComponent<AttackedWhenInteract>());

    }
    private void Update()
    {
        var ret = trigger.TryTrig();
    }
}