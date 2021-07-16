using System;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UniTools;

namespace com.rpdev.usnfp {
    
    public class NamespaceCorrector {
        
        public static void CorrectNamespace(FilePathProcessor.ComplexFilePaths paths) {
            string file = File.ReadAllText(paths.full_path);
            
            if (file.IndexOf("namespace", StringComparison.Ordinal) > -1) {
                int    start_of_namespace = file.IndexOf("namespace", StringComparison.Ordinal);
                int    end_of_namespace   = file.IndexOf("{", StringComparison.Ordinal);
                string old_namespace      = file.Substring(start_of_namespace, end_of_namespace - start_of_namespace);
                
                Debug.Log("Old Namespace [" + old_namespace + "]");
                
                file = file.Replace(old_namespace,
                                    "namespace " + GetNamespaceForPath(paths.origin_path).ToLower() + " ");
            } else {
                string[] lines = file.Split(new[] {"\r\n", "\r", "\n"}, StringSplitOptions.None);

                string ready = "";

                for (int i = 0; i < lines.Length; i++) {
                    if (i < 3)
                        ready += (i == 0 ? "" : Environment.NewLine) + lines[i];
                    else
                        ready += Environment.NewLine + "\t" + lines[i];
                    if (i == 3)
                        ready += Environment.NewLine + "namespace " + GetNamespaceForPath(paths.origin_path) + " {" +
                                 Environment.NewLine;
                }

                ready += Environment.NewLine + "}";
                file  =  ready;
            }

            File.WriteAllText(paths.full_path, file);
            AssetDatabase.Refresh();
        }

        private static string GetNamespaceForPath(string path) {
            string income = path;

            int last_index = income.LastIndexOf('/');
            income = income.Remove(last_index);

            string[] bit    = income.Split('/');
            string   result = "";

            for (int i = 2; i < bit.Length; i++) result += "." + bit[i];

            if (result.Length > 0) result = result.Remove(0, 1);

            return result;
        }
    }
}

