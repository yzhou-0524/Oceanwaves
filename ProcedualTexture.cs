using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ProcedualTexture : MonoBehaviour
{

    public MeshRenderer meshRenderer;
    public Material material;
    public Texture2D texture;
    [SerializeField] float width = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        material = meshRenderer.material;
        texture = new Texture2D(256, 256, TextureFormat.RGBA32, true, true);
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.filterMode = FilterMode.Point;
        material.SetTexture("_MainTex", texture);
        CreateTexture();

    }

    void CreateTexture()
    {
        for(int y = 0; y<texture.height; y++)
        {
            for(int x =0; x<texture.width; x++)
            {
               Color temp =  EvaluteTxturePixel(x, y);
                texture.SetPixel(x, y, temp);
            }
        }

        texture.Apply();
    }

    Color EvaluteTxturePixel(int x, int y)
    {
        float valueX = (x % (width * 2.0f)) / (width * 2.0f);
        int vX = 1;
        if(valueX < 0.5f)
        {
            vX = 0;
        }

        float valueY = (y % (width * 2.0f)) / (width * 2.0f);
        int vY = 1;
        if (valueY < 0.5f)
        {
            vY = 0;
        }

        float value = 0;
        if(vX == vY)
        {
            value = 1;
        }

        return new Color(value, value, value, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
