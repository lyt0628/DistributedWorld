using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class AssetBundleUtil
{
    static readonly string assetFile = Path.Combine(Application.streamingAssetsPath, "StreamingAssets");

    [MenuItem("GameLib/Resource/Build AssertBundle")]
    static void BuildAssetBundle()
    {

        var outDir = Application.streamingAssetsPath;
        if (!Directory.Exists(outDir))
        {
            Directory.CreateDirectory(outDir);
        }


        var builds = new List<AssetBundleBuild>
        {
            new()
            {
                assetBundleName = "man.unity3d",
                assetNames = new string[]
                {
                    "Assets/Temp/Man.prefab"
                }
            }
        };

        BuildPipeline.BuildAssetBundles(outDir,
                            builds.ToArray(),
                            BuildAssetBundleOptions.ChunkBasedCompression,
                            EditorUserBuildSettings.activeBuildTarget);



        AssetDatabase.Refresh();

    }


    [MenuItem("GameLib/Resource/Load AssetBundle")]
    static void LoadAssetBundle()
    {
        var bundle = AssetBundle.LoadFromFile(assetFile);
        AssetBundleManifest manifest =
            bundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");

        foreach (var item in manifest.GetAllDependencies("man.unity3d"))
        {
            AssetBundle.LoadFromFile(
                Path.Combine(Application.streamingAssetsPath, item));
        }

        bundle = AssetBundle.LoadFromFile(Path.Combine(
            Application.streamingAssetsPath, "man.unity3d"));

        var asset = bundle.LoadAsset("Man");
        GameObject.Instantiate(asset);
    }

    [MenuItem("GameLib/Resource/Unload Objects AssetBundles")]
    static void UnloadAllAssetBundle()
    {
        AssetBundle.UnloadAllAssetBundles(true);
        Debug.Log("Objects AssetBundles were unload.");
    }

}
