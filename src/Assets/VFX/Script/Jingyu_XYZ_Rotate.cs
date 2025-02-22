//Edit BY jingyu.Zhao
using UnityEngine;

public class Jingyu_XYZ_Rotate : MonoBehaviour
{

    public float x = 0f;
    public float y = 0f;
    public float z = 0f;
    // Use this for initialization
    void Start()
    {

    }

    // UpdateIfNeed is called once per frame


    void Update()
    {
        transform.Rotate(Time.deltaTime * x, 0, 0, Space.World);
        transform.Rotate(0, Time.deltaTime * y, 0, Space.World);
        transform.Rotate(0, 0, Time.deltaTime * z, Space.World);

    }
}












