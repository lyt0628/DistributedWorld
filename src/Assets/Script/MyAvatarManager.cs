using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/*
 * Create Mdeol 
 */
public class MyAvatarManager : MonoBehaviour
{

    public void Replace(Transform target, string name, SkinnedMeshRenderer mesh)
    {
        if (target == null || string.IsNullOrEmpty(name) || mesh == null)
        {
            return;
        }

        var targetParts = target.GetComponents<SkinnedMeshRenderer>();
        foreach (var part in targetParts)
        {
            if (part.name == name)
            {
                Destroy(part);
            }
        }
        var replacement = CloneSkinnedMesh(mesh);
        replacement.transform.parent = target.transform;
        
    }
    private static GameObject CloneSkinnedMesh(SkinnedMeshRenderer mesh)
    {
        
        if ( mesh == null)
        {
            return null;
        }

        var partObj = new GameObject();
        //partObj.name = mesh.name;
        partObj.AddComponent<SkinnedMeshRenderer>();
        partObj.GetComponent<SkinnedMeshRenderer>().sharedMesh = mesh.sharedMesh;
        partObj.GetComponent<SkinnedMeshRenderer>().bones = mesh.bones;
        partObj.GetComponent<SkinnedMeshRenderer>().materials = mesh.materials;

        return partObj;
    }
}
