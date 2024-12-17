

using UnityEditor;
using UnityEngine;

namespace QS.GameLib.Editor
{
    public class DeleteSubAssetC : MonoBehaviour
    {
        [MenuItem("GameLib/Delete Sub Asset")]
        public static void Delete()
        {
            Object[] selectedAssets = Selection.objects;
            if (selectedAssets.Length < 1)
            {
                Debug.LogWarning("No asset selected.");
            }

            foreach (var asset in selectedAssets)
            {
                if (AssetDatabase.IsSubAsset(asset))
                {
                    string path = AssetDatabase.GetAssetPath(asset);
                    DestroyImmediate(asset, true);
                    AssetDatabase.ImportAsset(path);
                }
            }
        }
    }
}