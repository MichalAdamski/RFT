using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Refactor
{
    public class EventExecutor : MonoBehaviour
    {
        public event Action OnReturnClick;
        public event Action OnCtrlClick;
        public event Action OnPlusClick;
        public event Action OnMinusClick;
        public event Action OnEndClick;
        public event Action OnScrollMove;

        public static EventExecutor Instance;

        private void Awake()
        {
            Instance = this;
        }

        void Update()
        {
            if (OVRInput.IsControllerConnected(OVRInput.Controller.Remote))
            {
                if (OVRInput.GetDown(OVRInput.Button.One))
                {
                    if (OnReturnClick != null)
                        OnReturnClick();
                }
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (OnReturnClick != null)
                    OnReturnClick();
            }
            if (Input.GetKeyDown(KeyCode.RightControl))
            {
                if (OnCtrlClick != null)
                    OnCtrlClick();
            }
            if (Input.GetKeyDown(KeyCode.Equals))
            {
                if (OnPlusClick != null)
                    OnPlusClick();
            }
            if (Input.GetKeyDown(KeyCode.Minus))
            {
                if (OnMinusClick != null)
                    OnMinusClick();
            }
            if (Input.GetKeyDown(KeyCode.End))
            {
                if (OnEndClick != null)
                    OnEndClick();
            }
            if (OnScrollMove != null)
                OnScrollMove();
        }
    }
}
