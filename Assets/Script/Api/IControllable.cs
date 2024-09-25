using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControllable 
{
    /*
     * CVelocity of Character
     */
    public Vector3 CVelocity { get; set; }

    /*
     * Main camera
     */
    public Camera CCamera{ get; set; }

    /*
     * T
     */
    public GameObject CGameObject{ get; set; }


}

