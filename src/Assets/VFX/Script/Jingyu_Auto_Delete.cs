//Edit BY jingyu.Zhao
using UnityEngine;

public class Jingyu_Auto_Delete : MonoBehaviour
{

    public float destroy = 1f;
    public bool destroyRoot = false;
    public bool autodesPs = true;
    private float maxLife = 0f;
    private float psDur = 0f;
    private float pscount = 0f;
    private float psdelay = 0f;
    private float myTime = 0;

    void Start()
    {
        maxLife = GetComponentInChildren<ParticleSystem>().startLifetime;
        psDur = GetComponentInChildren<ParticleSystem>().duration;
        pscount = GetComponentInChildren<ParticleSystem>().particleCount;
        psdelay = GetComponentInChildren<ParticleSystem>().startDelay;
        myTime = maxLife + psDur + psdelay;
        if (!autodesPs)
        {
            Destroy(gameObject, destroy);
        }
        else if (autodesPs && pscount == 0)
        {
            Destroy(gameObject, myTime);
        }
        if (destroyRoot)
        {
            Destroy(transform.root.gameObject, myTime);
        }
    }
}