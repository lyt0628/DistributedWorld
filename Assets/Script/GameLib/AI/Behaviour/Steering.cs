using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A Data class stands for movement and rotation 
 */
public class Steering
{
    /*
     * the angle we should rotate
     */
    public float angular;
    /*
     * the movement we should run
     */
    public Vector3 liner;

    public Steering() {
        angular = 0f;
        liner = Vector3.zero;
    }
        
}
