using System.Collections;
using System.Collections.Generic;
using UnityEngine;



using QS.API;
using QS;


//[RequireComponent(typeof(DestroySelfWhenInteract))]
[RequireComponent(typeof(AttackedWhenInteract))]
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

        //trigger.AddListener(GetComponent<DestroySelfWhenInteract>());
        trigger.AddListener(GetComponent<AttackedWhenInteract>());
        
    }
    private void Update()
    {
       var ret =  trigger.TryTrig();
    }
}