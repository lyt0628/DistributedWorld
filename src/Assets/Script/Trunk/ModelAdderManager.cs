using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    /// <summary>
    /// 目標裝配位置，是要裝配物體的父級
    /// </summary>
    public Transform target;
    /// <summary>
    /// 要裝配的模型
    /// </summary>
    public SkinnedMeshRenderer model;
    /// <summary>
    /// 給要裝配的模型取一個名字
    /// </summary>
    public string objName;
    /// <summary>
    /// 具有骨骼信息的模型，新裝配的模型需要用已經裝配的這個模型的骨骼來指揮自己的骨骼動畫
    /// </summary>
    public SkinnedMeshRenderer targetModel;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ModelAdder.Replace(target, objName, model, targetModel);
        }
    }
}
