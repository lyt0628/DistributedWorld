using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControllable 
{
    /*
     * CVelocity of Character
     */
    Vector3 CVelocity { get; set; }

    bool IsGrounded { get; set; }

    /*
     * Main camera
     */
    Camera CCamera{ get; set; }

    /*
     * T
     */
    GameObject CGameObject{ get; set; }


}

