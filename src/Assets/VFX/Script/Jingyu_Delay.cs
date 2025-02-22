//Edit BY jingyu.Zhao
using UnityEngine;

public class Jingyu_Delay : MonoBehaviour
{

    public float delayTime = 1.0f;

    // Use this for initialization
    void Start()
    {
        gameObject.SetActiveRecursively(false);
        Invoke("DelayFunc", delayTime);
    }

    void DelayFunc()
    {
        gameObject.SetActiveRecursively(true);
    }

}
