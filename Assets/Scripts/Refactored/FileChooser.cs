using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Scripts.Refactor
{
    public class FileChooser : MonoBehaviour
    {

        public void ChooseFilePath()
        {
            string path = EditorUtility.OpenFolderPanel("Choose folder", "", "");
            Debug.Log(path);
            DataHolder.PATH = path;
            GuiText.Instance.ChangePath(path);
        }
    }
}
