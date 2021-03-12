using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkyBox : MonoBehaviour
{
    public Material[] mats;
    int index;

    private Skybox sky;

    private void Start()
    {
        index = 0;
        sky = transform.GetComponent<Skybox>();
    }
    void ChangeSkyBox()
    {
        RenderSettings.skybox = mats[index];
        index++;
        index %= mats.Length;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeSkyBox();
        }
    }
}
