

using GameLib.DI;
using UnityEngine;

interface IA
{
    void SayHello();
}

class A : IA
{
    public void SayHello()
    {
        Debug.Log("Hello");
    }
}


class B
{
    [Injected]
    public IA MyA;

}