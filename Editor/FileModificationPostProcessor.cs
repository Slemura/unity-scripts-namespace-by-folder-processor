using UnityEditor;
using UnityEngine;

namespace com.rpdev.usnfp.Editor {

    [InitializeOnLoad]
    public class FileModificationPostProcessor : AssetPostprocessor {
        
        public static void OnPostprocessAllAssets(string[] imported_assets, string[] deleted_assets, string[] moved_assets, string[] moved_from_asset_paths) {


            for (int i = 0; i < moved_from_asset_paths.Length; i++) {
                
                if (!moved_assets[i].Contains("Assets/Scripts/")) continue;

                FilePathProcessor.ComplexFilePaths paths = FilePathProcessor.CheckAndGetPath(moved_assets[i]);

                if (paths != null) {
                    NamespaceCorrector.CorrectNamespace(paths);
                }
            }
        }
    }
}