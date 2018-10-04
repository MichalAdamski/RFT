using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Refactor {
    public class RandomAngle
    {
        public float[] Randomise(float[] anglesToRandomise)
        {
            int length = anglesToRandomise.Length;
            float[] angles = new float[length];
            int j = 0;
            List<int> num = new List<int>();

            for (int i = 0; i < length; i++)
            {
                num.Add(i);
            }

            for (int i = 0; i < length; i++)
            {
                j = (int)(Random.Range(0, num.Count));

                angles[num[j]] = anglesToRandomise[i];
                num.RemoveAt(j);
            }
            return angles;
        }
    }
}
