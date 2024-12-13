using UnityEngine;




class CLogBehaviour : MonoBehaviour
{
    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("CLOG");
    }
}