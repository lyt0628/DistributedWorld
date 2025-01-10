using QS.GameLib.Editor.AssetBundle;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class AssetBundleUtil
{
    static readonly string streamingAssetBundlePath = Path.Combine(Application.streamingAssetsPath, "StreamingAssets");

    [MenuItem("GameLib/Resource/Build AssertBundle")]
    static void BuildAssetBundle()
    {
        var p = Path.Combine(Application.dataPath, "BuildConf", "AssetBundle", "Conf.toml");

        var conf = new AssetBundleConf(p);


        var outDir = Application.streamingAssetsPath;
        if (!Directory.Exists(outDir))
        {
            Directory.CreateDirectory(outDir);
        }


        var builds = new List<AssetBundleBuild>
        {
            new()
            {
                assetBundleName = conf.BundleName,
                assetNames = conf.Files,
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
        var p = Path.Combine(Application.dataPath, "BuildConf", "AssetBundle", "Conf.toml");

        var conf = new AssetBundleConf(p);

        var streamingBundle = AssetBundle.LoadFromFile(streamingAssetBundlePath);
        AssetBundleManifest bundleManifest =
            streamingBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");

        foreach (var bundle in bundleManifest.GetAllDependencies(conf.BundleName))
        {
            AssetBundle.LoadFromFile(
                Path.Combine(Application.streamingAssetsPath, bundle));
        }

        streamingBundle = AssetBundle.LoadFromFile(Path.Combine(
            Application.streamingAssetsPath, conf.BundleName));

        var asset = streamingBundle.LoadAsset("CubeMan");
        GameObject.Instantiate(asset);
    }


    [MenuItem("GameLib/Resource/Unload Objects AssetBundles")]
    static void UnloadAllAssetBundle()
    {
        AssetBundle.UnloadAllAssetBundles(true);
        Debug.Log("Objects AssetBundles were unload.");
    }

}
