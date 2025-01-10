/*
 * New Mdeol 
 */
using UnityEngine;

public class ModelAdder : MonoBehaviour
{
    /// <summary>
    /// 裝配網格模型到目標物體上，用name來確定這個掛載的網格模型
    /// </summary>
    /// <param objName="target">目標掛載點，將作爲mesh的直接父級</param>
    /// <param objName="name">將要裝配得網格模型的名字，用來標識這個網格物體</param>
    /// <param objName="mesh">將要被裝配上的網格模型</param>
    public static void Replace(Transform target, string name, SkinnedMeshRenderer mesh, SkinnedMeshRenderer targetMesh)
    {
        if (target == null || string.IsNullOrEmpty(name) || mesh == null)
        {
            return;
        }

        RemoveMeshIfFoundGivenName(target, name);
        // 複製要裝配得網格模型的數據，裝到一個GameObject上
        var modelObj = CloneSkinnedMesh(mesh);
        modelObj.name = name;
        // 設置Unity中的父子關係
        modelObj.transform.parent = target.transform;

        modelObj.GetComponent<SkinnedMeshRenderer>().bones = targetMesh.bones;
    }

    private static void RemoveMeshIfFoundGivenName(Transform target, string name)
    {
        // 獲取SkinnedMeshRenderer 組件，Unity把網格信息保存在裏面
        var targetParts = target.GetComponents<SkinnedMeshRenderer>();
        foreach (var part in targetParts)
        {
            // 用名字確定裝配的子物體，如果已經裝配，就刪除原本的子物體，裝新的
            if (part.name == name)
            {
                Destroy(part);
            }
        }
    }

    private static GameObject CloneSkinnedMesh(SkinnedMeshRenderer mesh)
    {

        if (mesh == null)
        {
            return null;
        }

        var partObj = new GameObject();
        //partObj.objName = mesh.objName;
        partObj.AddComponent<SkinnedMeshRenderer>();
        partObj.GetComponent<SkinnedMeshRenderer>().sharedMesh = mesh.sharedMesh;
        //partObj.GetComponent<SkinnedMeshRenderer>().bones = mesh.bones;
        partObj.GetComponent<SkinnedMeshRenderer>().materials = mesh.sharedMaterials;

        return partObj;
    }
}
