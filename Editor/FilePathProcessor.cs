using System;
using UnityEngine;

namespace com.rpdev.usnfp {
    public class FilePathProcessor {
        public class ComplexFilePaths {
            public string origin_path;
            public string full_path;
        }

        public static ComplexFilePaths CheckAndGetPath(string path) {
            string origin_path   = path;
            string modified_path = path.Replace(".meta", "");

            int file_type_separator_index = modified_path.LastIndexOf(".", StringComparison.Ordinal);

            if (file_type_separator_index < 0) return null;

            string file_type = modified_path.Substring(file_type_separator_index);

            if (file_type != ".cs" && file_type != ".js" && file_type != ".boo") return null;

            int    full_path_index = Application.dataPath.LastIndexOf("Assets", StringComparison.Ordinal);
            string full_path       = Application.dataPath.Substring(0, full_path_index) + modified_path;

            return new ComplexFilePaths {origin_path = origin_path, full_path = full_path};
        }
    }
}