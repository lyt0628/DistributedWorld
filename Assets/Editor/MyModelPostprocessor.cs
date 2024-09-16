using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MyModelPostprocessor : AssetPostprocessor
{

    /*
     * Disable to import materials from model files
     */
    private void OnPreprocessModel()
    {
        ModelImporter importer = (ModelImporter) assetImporter;
        importer.materialImportMode = ModelImporterMaterialImportMode.None;
    }

}
