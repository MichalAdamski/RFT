using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Refactor
{
    public class GuiText : MonoBehaviour
    {
        [SerializeField]
        private InputField PatientName;
        [SerializeField]
        private InputField age;
        [SerializeField]
        private InputField id;
        [SerializeField]
        private Text path;

        public static GuiText Instance;

        public void Start()
        {
            Instance = this;
            PatientName.text = DataHolder.NAME;
            age.text = DataHolder.AGE;
            path.text = DataHolder.PATH;
            id.text = DataHolder.ID;
        }

        public void SaveData()
        {
            DataHolder.NAME = PatientName.text;
            DataHolder.AGE = age.text;
            DataHolder.ID = id.text;
            DataHolder.PATH = path.text;
        }

        public void ChangePath(string path)
        {
            this.path.text = path;
        }
    }
}