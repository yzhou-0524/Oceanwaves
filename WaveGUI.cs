using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGUI : MonoBehaviour
{
    string sinwaveindex = "1";
    float GUIsinwaveindex;
    string[] sinwavename = { "sinx", "2sinx", "|sinx|", "1/2sinx", "1-|sinx|", "(1 -| sinx |) ^ 2", "sinx ^ 2", "1-2(sinx^2)" };
    public int guisnwaveindex = 1;
    string guisinwavename;

    public float hSliderValuex = 0.1f;
    public float hSliderValuey = 5.1f;

    public float scalex = 11.1f;
    public float scaley = 1.69f;

    public float height = 1.9f;

    public float sun = 1;

    public bool wireframe = false;
    public GameObject Waves;


    public Material[] material;
    public Material guimaterial;

    public GameObject Sun;

    public void OnGUI()
    {
        GUI.Label(new Rect(20, 20, 300, 100), "Now the sin wave is: " + GUIsinwaveindex + " :" + guisinwavename);
        GUI.Label(new Rect(20, 50, 300, 150), "Click the button to try different sin waves");

        if (GUI.Button(new Rect(310, 50, 80, 20), "Change"))
        {
            ChangeSin();
          
        }

        GUI.Label(new Rect(20, 100, 300, 100), "X and Y speed is ( " + hSliderValuex +", "+ hSliderValuey +" )");
        hSliderValuex = GUI.HorizontalSlider(new Rect(300, 110, 40, 30), hSliderValuex, 1, 10);
        hSliderValuey = GUI.HorizontalSlider(new Rect(350, 110, 40, 30), hSliderValuey, 1, 10);

        GUI.Label(new Rect(20, 150, 300, 100), "X and Y scale is ( " + scalex + ", " + scaley + " )");
        scalex = GUI.HorizontalSlider(new Rect(300, 160, 40, 30), scalex, 1, 20);
        scaley = GUI.HorizontalSlider(new Rect(350, 160, 40, 30), scaley, 1, 20);

        GUI.Label(new Rect(20, 200, 300, 100), "The height of the wave is: " + height);
        height = GUI.HorizontalSlider(new Rect(300, 210, 80, 30), height, 0.5f, 8);

        GUI.Label(new Rect(20, 230, 300, 100), "Change the solid color of the wave to: ");
        if (GUI.Button(new Rect(290, 230, 40, 20), "Blue"))
        {
            Debug.Log("can change to blue");
            guimaterial = material[0];
        }

        if (GUI.Button(new Rect(335, 230, 50, 20), "Green"))
        {
            Debug.Log("can change to yellow");
            guimaterial = material[1];
        }

        if (GUI.Button(new Rect(390, 230, 47, 20), "Yellow"))
        {
            Debug.Log("can change to green");
            guimaterial = material[2];
        }

        GUI.Label(new Rect(30, 260, 300, 200), "Show the wave in the wireframe mode");
        wireframe = GUI.Toggle(new Rect(300, 260, 100, 30), wireframe, "wireframe");

        GUI.Label(new Rect(30, 305, 300, 100), "Press 'A' to change the sky you like");
       

        GUI.Label(new Rect(30, 350, 300, 100), "Change the light strength to " + sun);
        sun = GUI.HorizontalSlider(new Rect(300, 350, 80, 30), sun, 0f, 5);
    }


    void ChangeSin()
    {
        if(GUIsinwaveindex <8)
        {
            guisnwaveindex++;
            GUIsinwaveindex++;
            guisinwavename = sinwavename[guisnwaveindex -1];

        }

        else
        {
            guisnwaveindex = 1;
            GUIsinwaveindex = 1;
            guisinwavename = sinwavename[0];
        }
    }

    private void Start()
    {
            GUIsinwaveindex = float.Parse(sinwaveindex);
            guisnwaveindex = (int)GUIsinwaveindex;
            guisinwavename = sinwavename[0];
    }

    private void Update()
    {
        if(wireframe == true)
        {
            Waves.GetComponent<WireFrame>().enabled = true;
        }
        else
        {
            Waves.GetComponent<WireFrame>().enabled = false;
        }

        //Sun.transform.rotation = Quaternion.Euler(sun, 0, 0);
        Sun.GetComponent<Light>().intensity = sun;
    }
}
