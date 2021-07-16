namespace com.rpdev.usnfp {
    public class FileModificationPreProcessor : UnityEditor.AssetModificationProcessor {
        public static void OnWillCreateAsset(string path) {
            if (!path.Contains("Assets/Scripts/")) return;
            
            FilePathProcessor.ComplexFilePaths paths = FilePathProcessor.CheckAndGetPath(path);
            if (paths != null) {
                NamespaceCorrector.CorrectNamespace(paths);
            }
        }
    }
}