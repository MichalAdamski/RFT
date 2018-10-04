using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Refactor
{
    public class TestStepsManager
    {
        
        //private readonly float[] ANGLESBANK = new float[] { -22.5f, 22.5f, -22.5f, 22.5f, -22.5f, 22.5f, -22.5f, 22.5f, -10f, 10f, -10f, 10f, -10f, 10f, -10f, 10f, -15f, 15f, -15f, 15f, -15f, 15f, -15f, 15f };
        private readonly float[] ANGLESBANK = new float[] {-45f, -40f, -30f, -20f, -15f, -10f, -5f, 5f, 10f, 15f, 20f, 30f, 40f, 45f};

        private List<float> results = new List<float>();
        public float[] Angles;
        private Saving save = new Saving();
        private RandomAngle random = new RandomAngle();

        public void StartNewTest()
        {
            Angles = random.Randomise(ANGLESBANK);
        }

        public int GetTestLength()
        {
            return Angles.Length;
        }

        public void ConfirmAngle(GameObject rod)
        {
            float result;
            float resultBoffor;
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
        }

        public void SaveResults()
        {/*
            for (int i = 0; i < results.Count; i++)
            {
                save.Save(Angles[i] + "\t" + results[i]);
            }
            */
            SaveBasicData();

            SortedList<float, float> data = Sort.sorto(results, Angles);

            foreach (KeyValuePair<float, float> a in data)
            {
                save.Save(a.Key + "\t" + a.Value);
            }
            
            results.Clear();
        }

        private void SaveBasicData()
        {
            save.Save(DataHolder.NAME + "\t" + DataHolder.AGE);
        }

        public void RotateRod(GameObject rod)
        {
            var scrollAxis = Input.GetAxis("Mouse ScrollWheel");
            if (scrollAxis > 0)
            {
                rod.transform.rotation *= Quaternion.Euler(0, 0, 0.5f);
            }
            else if (scrollAxis < 0)
            {
                rod.transform.rotation *= Quaternion.Euler(0, 0, -0.5f);
            }
        }

        public void ChangeToScene()
        {
            SceneManager.LoadScene(0);
        }

        private float[] PrepareAngles(int testNum)
        {
            float tubeAngleB;
            float rodAngleB;
            
            if ((tubeAngleB = Angles[testNum]) > 0)
            {
                rodAngleB = -tubeAngleB + 5f;
                tubeAngleB = -tubeAngleB + 180;
            }
            else
            {
                rodAngleB = -tubeAngleB - 5f;
                tubeAngleB = -tubeAngleB - 180;
            }
            return new float[] { rodAngleB, tubeAngleB };
        }

        public IEnumerator Rotate(int num, float duration, GameObject rod, GameObject tube, Action act3)
        {
            float[] angles = PrepareAngles(num);
            Quaternion fromR = rod.transform.rotation;
            Quaternion toR = rod.transform.rotation;
            Quaternion fromT = tube.transform.rotation;
            Quaternion toT = tube.transform.rotation;

            toR *= Quaternion.Euler(0, 0, angles[0]);
            toT *= Quaternion.Euler(0, angles[1], 0);

            float elapsed = 0.0f;
            while (elapsed < duration)
            {
                rod.transform.rotation = Quaternion.Slerp(fromR, toR, elapsed / duration);
                tube.transform.rotation = Quaternion.Slerp(fromT, toT, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }
            rod.transform.rotation = toR;
            tube.transform.rotation = toT;
            act3();
        }
        public IEnumerator RotateZero(float duration, GameObject rod, GameObject tube)
        {
            Quaternion fromR = rod.transform.rotation;
            Quaternion toR = Quaternion.Euler(0, 0, 0);
            Quaternion fromT = tube.transform.rotation;
            Quaternion toT = Quaternion.Euler(0, 90, 90);

            float elapsed = 0.0f;
            while (elapsed < duration)
            {
                rod.transform.rotation = Quaternion.Slerp(fromR, toR, elapsed / duration);
                tube.transform.rotation = Quaternion.Slerp(fromT, toT, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }
            rod.transform.rotation = toR;
            tube.transform.rotation = toT;
        }

        public IEnumerator MainAction(Action act1, Action act2, float time)
        {
            act1();
            yield return new WaitForSeconds(time);
            act2();
        }

        public void MoveHead(GameObject head, float level, bool down)
        {
            if (down)
            {
                head.transform.Translate(new Vector3(0, -level, 0));
            }
            else
            {
                head.transform.Translate(new Vector3(0, level, 0));
            }
        }
    }
}
