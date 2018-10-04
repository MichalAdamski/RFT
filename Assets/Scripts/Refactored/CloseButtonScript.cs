using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Refactor
{
    public class CloseButtonScript : MonoBehaviour
    {
        public void onAppClose()
        {
            Application.Quit();
        }
    }
}