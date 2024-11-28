/*
 * A Data class stands for movement and rotation 
 */
using UnityEngine;

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

    public Steering()
    {
        angular = 0f;
        liner = Vector3.zero;
    }

}
