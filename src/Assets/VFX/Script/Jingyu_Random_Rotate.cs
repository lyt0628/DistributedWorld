//Edit BY jingyu.Zhao
using System.Collections;
using UnityEngine;

public class Jingyu_Random_Rotate : MonoBehaviour
{

    public bool isRotate = true;
    public int fps = 30;
    public int x = 0, y = 0, z = 0;

    private float rangeX, rangeY, rangeZ;
    private float deltaTime;
    bool isVisible;
    // Use this for initialization
    void Start()
    {
        deltaTime = 1f / fps;
        rangeX = Random.Range(0, 10);
        rangeY = Random.Range(0, 10);
        rangeZ = Random.Range(0, 10);
    }

    void OnBecameVisible()
    {
        isVisible = true;
        StartCoroutine(UpdateRotation());
    }

    void OnBecameInvisible()
    {
        isVisible = false;
    }
    // UpdateIfNeed is called once per frame
    IEnumerator UpdateRotation()
    {
        while (isVisible)
        {
            if (isRotate)
            {
                transform.Rotate(deltaTime * Mathf.Sin(Time.time + rangeX) * x,
                  deltaTime * Mathf.Sin(Time.time + rangeY) * y,
                  deltaTime * Mathf.Sin(Time.time + rangeZ) * z);
            }
            yield return new WaitForSeconds(deltaTime);
        }
    }
}
