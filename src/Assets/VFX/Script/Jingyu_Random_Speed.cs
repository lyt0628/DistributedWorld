//Edit BY jingyu.Zhao
using UnityEngine;

public class Jingyu_Random_Speed : MonoBehaviour
{


    public float MinSpeed = 0.9f;
    public float MaxSpeed = 1.1f;
    private Animator anim;
    private AnimatorStateInfo animatorInfo;
    private float AnSpeed = 1.0f;
    void Start()
    {
        anim = GetComponent<Animator>();
        AnSpeed = Random.Range(MinSpeed, MaxSpeed);
    }

    void Update()
    {
        //animatorInfo=anim.GetCurrentAnimatorStateInfo(0);
        anim.speed = AnSpeed;
        //		if(animatorInfo.IsName("Markan"))//注意这里指的不是动画的名字而是动画状态的名字
        //			{
        //			anim.speed=AnSpeed;
        //			}
    }
}