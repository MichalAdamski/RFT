using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Scripts.Refactor {
    public class Saving
    {
        public static string FileName;
        private string fileDir = @"/Badania";
        
        public void Save(string tekst)
        {
            string path = Application.dataPath + fileDir;
            DirectoryInfo dir = new DirectoryInfo(path);

            if (!dir.Exists)
            {
                dir.Create();
            }

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(Path.Combine(path,FileName), true))
            {
                file.WriteLine(tekst);
            }
        }
    }
}
