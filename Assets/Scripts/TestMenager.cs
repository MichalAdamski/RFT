using Scripts.Refactor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestMenager : MonoBehaviour
{
    [SerializeField]
    private GameObject rod;
    [SerializeField]
    private GameObject tube;
    [SerializeField]
    private float duration = 1f;
    [SerializeField]
    private GameObject cam;

    private bool isNewAngleAllowed = false;
    private bool isNewTestStarted = false;
    private bool isBackToZeroRotationNeeded = true;
    [SerializeField]
    private bool isAngleRangom = false;
    private int testCouter = 0;
    private int testNumber;
    private float[] angles;
    private int step;
    private float result;
    private float gap = 10f;
    private float angle = -40f;
    private bool isEnd = false;
    private float resultBoffor;
    private List<float> results;
    private Rotation rot;
    private RandomAngle ran;
    private Saving save;
    private Sort sort;
    private SortedList<float, float> data;
    private bool isControllerChanged = false;
    private bool standingTest = false;
    private float timer = 0;

    private float[] angle1;
    // Use this for initialization
    void Start()
    {
        angle1 = new float[] { -22.5f, 22.5f, -22.5f, 22.5f, -22.5f, 22.5f, -22.5f, 22.5f, -10f, 10f, -10f, 10f, -10f, 10f, -10f, 10f, -15f, 15f, -15f, 15f, -15f, 15f, -15f, 15f};
        testNumber = angle1.Length;

        // testNumber = (int)(((Mathf.Abs(angle) * 2) / gap));
        angles = new float[testNumber];

        rot = new Rotation();
        ran = new RandomAngle();
        save = new Saving();
        results = new List<float>();
        sort = new Sort();
        data = new SortedList<float, float>();
    }

    // Update is called once per frame
    void Update()
    {
        StartNewTest();
        NextAngle();

        if (isNewTestStarted && !rot.isFrameInStartPosition)
        {
            RotateRod();
            SaveResult();
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            isControllerChanged = !isControllerChanged;
        }
        else if (Input.GetKeyDown(KeyCode.Minus))
        {
            cam.transform.position = new Vector3(0, cam.transform.position.y - 0.1f, 5);
        }
        else if (Input.GetKeyDown(KeyCode.Equals))
        {
            cam.transform.position = new Vector3(0, cam.transform.position.y + 0.1f, 5);
        }

    }

    private void ChangeToScene()
    {
        SceneManager.LoadScene(0);
    }

    private void RotateRod()
    {
        var scrollAxis = Input.GetAxis("Mouse ScrollWheel");

        if (!isControllerChanged)
        {
            if (scrollAxis > 0)
            {
                rod.transform.rotation *= Quaternion.Euler(0, 0, 0.5f);
            }
            else if (scrollAxis < 0)
            {
                rod.transform.rotation *= Quaternion.Euler(0, 0, -0.5f);
            }
        }
        else
        {
            if (OVRInput.Get(OVRInput.Button.DpadLeft))
            {
                timer += Time.deltaTime;
                if (timer > 1)
                {
                    rod.transform.rotation *= Quaternion.Euler(0, 0, 0.5f);
                    timer -= 0.03f;
                }
            }
            else if (OVRInput.Get(OVRInput.Button.DpadRight))
            {
                timer += Time.deltaTime;
                if (timer > 1)
                {
                    rod.transform.rotation *= Quaternion.Euler(0, 0, -0.5f);
                    timer -= 0.03f;
                }
            }

            if (OVRInput.GetUp(OVRInput.Button.DpadLeft))
            {
                timer = 0;
            }
            else if (OVRInput.GetUp(OVRInput.Button.DpadRight))
            {
                timer = 0;
            }

            if (OVRInput.GetDown(OVRInput.Button.DpadLeft))
            {
                rod.transform.rotation *= Quaternion.Euler(0, 0, 0.5f);
            }
            else if (OVRInput.GetDown(OVRInput.Button.DpadRight))
            {
                rod.transform.rotation *= Quaternion.Euler(0, 0, -0.5f);
            }
        }
    }

    private void SaveResult()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (rod.transform.eulerAngles.z >= 180)
            {
                result = -(rod.transform.eulerAngles.z - 360);
                if (result > 90)
                {
                    resultBoffor = result - 180;
                }
                else if (result < -90)
                {
                    resultBoffor = result + 180;
                }
                else
                {
                    resultBoffor = result;
                }
            }
            else
            {
                result = -rod.transform.eulerAngles.z;
                if (result > 90)
                {
                    resultBoffor = result - 180;
                }
                else if (result < -90)
                {
                    resultBoffor = result + 180;
                }
                else
                {
                    resultBoffor = result;
                }
            }

            results.Add(resultBoffor);
            step++;

            isNewTestStarted = false;
            isNewAngleAllowed = true;
            isBackToZeroRotationNeeded = true;
        }
    }

    private void StartNewTest()
    {
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            save.Save("newTest");
            save.Save("choosenAngle" + "\t" + "testedAngle" + "\t");

            angles = ran.los(angle1, testNumber, angles);

            testCouter = 0;
            isNewAngleAllowed = true;
            isBackToZeroRotationNeeded = true;
            step = 0;
        }
    }

    private void NextAngle()
    {
        if (testCouter < testNumber && isNewAngleAllowed && Input.GetKeyDown(KeyCode.Return))
        {
            if (isBackToZeroRotationNeeded && !rot.isFrameInStartPosition)
            {
                StartCoroutine(rot.RotateZeroTube(duration, tube));
                StartCoroutine(rot.RotateZeroRod(duration, rod));
                isBackToZeroRotationNeeded = false;
            }
            else if (rot.isFrameInStartPosition && !isBackToZeroRotationNeeded && Input.GetKeyDown(KeyCode.Return))
            {
                if (angles[testCouter] > 0)
                {
                    StartCoroutine(rot.RotateTube(-(angles[testCouter] + 180), duration, isAngleRangom, tube));
                    StartCoroutine(rot.RotateRod(-(angles[testCouter] + 5f), duration, isAngleRangom, rod));
                }
                else
                {
                    StartCoroutine(rot.RotateTube(-(angles[testCouter] - 180), duration, isAngleRangom, tube));
                    StartCoroutine(rot.RotateRod(-(angles[testCouter] - 5f), duration, isAngleRangom, rod));
                }

                isNewTestStarted = true;
                isNewAngleAllowed = false;
                testCouter++;
            }
        }
        else if ((testCouter == testNumber && isNewAngleAllowed))
        {
            /*
            data = sort.sorto(results, angles);

            foreach (KeyValuePair<float, float> a in data)
            {
                save.Save(a.Key + "\t" + a.Value);
            }
            */

            for (int i = 0; i < results.Count; i++)
            {
                save.Save(angles[i] + "\t" + results[i]);
            }

            save.Save("koniec" + "\r\n");

            isNewAngleAllowed = false;
            isBackToZeroRotationNeeded = true;
            isEnd = true;
            data.Clear();
            results.Clear();
        }
        else if (Input.GetKeyDown(KeyCode.End))
        {
            if (!isEnd)
            {
                /*
                data = sort.sorto(results, angles);

                foreach (KeyValuePair<float, float> a in data)
                {
                    save.Save(a.Key + "\t" + a.Value);
                }*/
                Debug.Log(results.Count);
                for (int i = 0; i < results.Count; i++) {
                    save.Save(angles[i] + "\t" + results[i]);
                }
               
                save.Save("koniec" + "\r\n");
            }
            isNewAngleAllowed = false;
            isBackToZeroRotationNeeded = true;
            isEnd = false;
            data.Clear();
            results.Clear();
            ChangeToScene();
        }
    }
}

