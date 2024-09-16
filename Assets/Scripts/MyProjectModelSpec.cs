using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyProjectModelSpec 
{

    private static string MODEL_DIRECTORY = "Model";
    
    public static string GetModelPath(string model)
    {
        return MODEL_DIRECTORY + "/" + model;
    }
}
