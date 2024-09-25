using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameLib;

public class UseMesager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Messager.Instance.AddListener("test", DoHandle);
    }

    // Update is called once per frame
    void Update()
    {
        
        Messager.Instance.Boardcast("test",new SingleArgMessage<int>(3));
        Messager.Instance.RemoveListener("test",  DoHandle);
    }

    private void DoHandle (IMessage msg)
    {
        var msg0  = msg as SingleArgMessage<int>;
        Debug.Log(msg0.Value);
    }
}
