using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureSwapper : MonoBehaviour
{
    [SerializeField]
    private GameObject rod;
    [SerializeField]
    private GameObject tube;
    [SerializeField]
    private GameObject plane;
    public static string Color { get; set; }

    // Use this for initialization
    void Start()
    {
        Color = "czarny" + "\t" + "biały";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {


            rod.GetComponent<Renderer>().material = Resources.Load("materials/deepblack") as Material;
            plane.GetComponent<Renderer>().material = Resources.Load("newMaterials/whiteBack") as Material;
            tube.GetComponent<Renderer>().material = Resources.Load("newMaterials/whiteTube") as Material;
            Color = "czarny" + "\t" + "biały";

        }
        if (Input.GetKey(KeyCode.Alpha2))
        {


            rod.GetComponent<Renderer>().material = Resources.Load("materials/purple") as Material;
            plane.GetComponent<Renderer>().material = Resources.Load("newMaterials/blueBack") as Material;
            tube.GetComponent<Renderer>().material = Resources.Load("newMaterials/blueTube") as Material;
            Color = "fioletowy" + "\t" + "niebieski";

        }
        if (Input.GetKey(KeyCode.Alpha3))
        {


            rod.GetComponent<Renderer>().material = Resources.Load("materials/orange") as Material;
            plane.GetComponent<Renderer>().material = Resources.Load("newMaterials/redBack") as Material;
            tube.GetComponent<Renderer>().material = Resources.Load("newMaterials/redTube") as Material;
            Color = "pomaranczowy" + "\t" + "czerwony";

        }

    }
}
