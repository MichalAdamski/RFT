using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Refactor
{
    public class DataHolder
    {
        private static string fileDir = @"/Badania";
        public static string NAME = string.Empty;
        public static string AGE = string.Empty;
        public static string PATH = Application.dataPath + fileDir;
        public static string ID = string.Empty;

        private DataHolder()
        {

        }
    }
}