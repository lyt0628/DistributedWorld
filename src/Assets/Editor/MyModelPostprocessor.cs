using UnityEditor;

public class MyModelPostprocessor : AssetPostprocessor
{

    /*
     * Disable to import materials from Model files
     */
    private void OnPreprocessModel()
    {
        ModelImporter importer = (ModelImporter)assetImporter;
        importer.materialImportMode = ModelImporterMaterialImportMode.None;
    }

}
