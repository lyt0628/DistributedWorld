using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.AssetImporters;
using UnityEngine;


[ScriptedImporter(version: 1, new[] {"toml", "lua"})]
public class CustomTextAssetImporter : ScriptedImporter
{
    public override void OnImportAsset(AssetImportContext ctx)
    {
        string content = File.ReadAllText(ctx.assetPath, System.Text.Encoding.UTF8);  

        TextAsset textAsset = new(content);
        var hash = new HashCode();
        hash.Add(content);
       
        ctx.AddObjectToAsset(hash.ToHashCode().ToString(), textAsset);
        ctx.SetMainObject(textAsset);

    }
}
