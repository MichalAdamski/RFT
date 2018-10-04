using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodAndRotatingFrameMain : MonoBehaviour
{

    [SerializeField]
    private float speed = 10;
    [SerializeField]
    GameObject plane;
    [SerializeField]
    GameObject tube;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        plane.transform.Rotate(0, speed * Time.deltaTime, 0);
        tube.transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}
