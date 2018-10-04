using Scripts.Refactor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiText : MonoBehaviour
{

    public InputField imie;
    public InputField wiek;
    public InputField path;
    private Saving zapis = new Saving();

    public void ZapiszDane()
    {
        Saving.FileName = path.text;
        zapis.Save(imie.text + "\t" + wiek.text + "\r\n");
        TextureSwapper.Color = "czarny" + "\t" + "biały";
    }

}
