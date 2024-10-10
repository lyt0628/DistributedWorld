using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MyFighter  
{
    [SerializeField] private string xname;
    [SerializeField] private string description;
    [SerializeField] public List<string> scripts = new List<string>();
     private MyDictionary<string, int> kvPairs  = new();

    public MyFighter()
    {
        xname = "666";
        description = "FFF";
        
        scripts.Add("666");
        scripts.Add(description);

        kvPairs["name"] = 6;
        kvPairs["info"] = 7;

        var json = JsonUtility.ToJson(kvPairs);
        Debug.Log(json);
    }

    public MyFighter Clone()
    {
        var obj = new MyFighter();
        obj.xname = xname;
        obj.description = description;
        obj.scripts = new List<string>(scripts);
       
        return obj;
    }
}
