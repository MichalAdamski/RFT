using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Scripts.Refactor
{
    public class Saving
    {

        public void Save(string tekst)
        {
            string path = DataHolder.PATH;
            DirectoryInfo dir = new DirectoryInfo(path);
            string fileName = CreateFileName();

            if (!dir.Exists)
            {
                dir.Create();
            }

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(Path.Combine(path, fileName), true))
            {
                file.WriteLine(tekst);
            }
        }

        private string CreateFileName()
        {
            string name = DataHolder.NAME;
            string[] split = name.Split(' ');
            string fileName = DataHolder.ID;
            foreach(string s in split)
            {
                fileName += s.Substring(0, 1);
            }
            fileName += DataHolder.AGE + ".txt";
            return fileName;
        }
    }
}
