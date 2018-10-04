using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation
{
    //public bool n = false;

    public bool isFrameInStartPosition = false;

    public IEnumerator RotateTube(float angle, float duration, bool rand, GameObject tube)
    {
        if (rand)
        {
            angle = (int)(Random.value * 360 - 180);
        }
        Quaternion from = tube.transform.rotation;
        Quaternion to = tube.transform.rotation;

        to *= Quaternion.Euler(0, angle, 0);

        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            tube.transform.rotation = Quaternion.Slerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        tube.transform.rotation = to;
    }
    public IEnumerator RotateRod(float angle, float duration, bool rand, GameObject rod)
    {
        if (rand)
        {
            angle = (int)(Random.value * 720 - 360);
        }
        Quaternion from = rod.transform.rotation;
        Quaternion to = rod.transform.rotation;

        to *= Quaternion.Euler(0, 0, angle);

        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            rod.transform.rotation = Quaternion.Slerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        rod.transform.rotation = to;
        isFrameInStartPosition = false;
    }

    public IEnumerator RotateZeroRod(float duration, GameObject rod)
    {
        Quaternion from = rod.transform.rotation;
        Quaternion to = Quaternion.Euler(0, 0, 0);


        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            rod.transform.rotation = Quaternion.Slerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        rod.transform.rotation = to;
        isFrameInStartPosition = true;
    }
    public IEnumerator RotateZeroTube(float duration, GameObject tube)
    {
        Quaternion from = tube.transform.rotation;
        Quaternion to = Quaternion.Euler(0, 90, 90);

        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            tube.transform.rotation = Quaternion.Slerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        tube.transform.rotation = to;
    }
}