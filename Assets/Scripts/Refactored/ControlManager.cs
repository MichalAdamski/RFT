using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Refactor
{
    public class ControlManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject rod;
        [SerializeField]
        private GameObject tube;
        [SerializeField]
        private GameObject head;

        private TestStepsManager stepsManager = new TestStepsManager();

        private bool isTestStart = false;
        private bool isPreparingEnd = false;
        private bool isPreparingStart = false;
        private bool isResoultConfirmed = true;
        private bool isBackToZeroRotationNeeded = false;
        private float duration = 1f;
        private int testNum = 0;
        private float time = 1.5f;

        // Use this for initialization
        void Start()
        {
            EventExecutor.Instance.OnCtrlClick += OnCtrlClick;
            EventExecutor.Instance.OnReturnClick += OnReturClick;
            EventExecutor.Instance.OnScrollMove += OnScrollMove;
            EventExecutor.Instance.OnEndClick += OnTestEnd;
            EventExecutor.Instance.OnPlusClick += () => stepsManager.MoveHead(head, 0.1f, false);
            EventExecutor.Instance.OnMinusClick += () => stepsManager.MoveHead(head, 0.1f, true);
        }

        private void OnScrollMove()
        {
            if (isPreparingEnd && isTestStart)
            {
                stepsManager.RotateRod(rod);
            }
        }

        private void OnCtrlClick()
        {
            testNum = 0;
            stepsManager.StartNewTest();
            isTestStart = true;
        }

        private void OnRotationEnd()
        {
            isPreparingEnd = true;
            isPreparingStart = false;
        }

        private void Rotate()
        {
            isPreparingStart = true;
            isResoultConfirmed = false;
            
            if (testNum < stepsManager.GetTestLength())
            {
                StartCoroutine(stepsManager.MainAction(() => StartCoroutine(stepsManager.RotateZero(duration, rod, tube)), () => StartCoroutine(stepsManager.Rotate(testNum, duration, rod, tube, OnRotationEnd)), time));
            }
            else
            {
                OnTestEnd();
            }
        }

        private void OnTestEnd()
        {
            isTestStart = false;
            stepsManager.SaveResults();
        }

        private void Confirm()
        {
            stepsManager.ConfirmAngle(rod);
            isResoultConfirmed = true;
            isPreparingEnd = false;
            testNum++;
            OnReturClick();
        }

        private void OnReturClick()
        {
            if (!isPreparingStart && isTestStart && !isPreparingEnd && isResoultConfirmed)
            {
                Rotate();
            }
            else if (isTestStart && isPreparingEnd && !isResoultConfirmed)
            {
                Confirm();
            }
        }
    }
}
