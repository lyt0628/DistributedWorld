using System.Collections;
using System.Collections.Generic;
using UnityEngine;



using QS.API;
using QS;


[RequireComponent(typeof(DestroySelfWhenInteract))]
public class InteractalbleObject : MonoBehaviour
{
    SphereInteractTrigger trigger;

    private void Start()
    {
        trigger = new SphereInteractTrigger
        {
            item = transform,
            radius = 1.5f
        };

        trigger.AddListener(GetComponent<DestroySelfWhenInteract>());

    }
    private void Update()
    {
       var ret =  trigger.TryTrig();
    }
}