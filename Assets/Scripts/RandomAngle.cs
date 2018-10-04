using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAngle
{

    // Use this for initialization


    public float[] los(float angle, int testNumber, float gap, float[] angles)
    {
        int j = 0;

        List<int> num = new List<int>();
        for (int i = 0; i < testNumber; i++)
        {
            num.Add(i);
        }

        while (angle <= ((testNumber * gap) / 2))
        {
            if (angle != 0)
            {
                j = (int)(Random.Range(0, num.Count));


                angles[num[j]] = angle;
                num.RemoveAt(j);
            }
            angle += gap;
        }
        return angles;
    }

    public float[] los(float[] angle1, int testNumber, float[] angles)
    {
        int j = 0;

        List<int> num = new List<int>();

        for (int i = 0; i < testNumber; i++)
        {
            num.Add(i);
        }

        for(int i = 0; i < testNumber; i++)
        {
            j = (int)(Random.Range(0, num.Count));

            angles[num[j]] = angle1[i];
            num.RemoveAt(j);
        }
        return angles;
    }

}
