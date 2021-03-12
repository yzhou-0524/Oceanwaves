using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{
    public enum DrawMode { Update, colorMap};
    public DrawMode drawMode;
    public int Dimension = 10;
    public Octave[] Octaves;
    public float UVScale;
    public GameObject MainCamera;

    protected MeshFilter meshFilter;
    public Mesh mesh;
    public Material material;
    public Material lineMaterial;

    //public TerrainType[] regions;

    int sindex;
    float[] sinnumbers = { 1.0f, 0.4f, 2.0f, 0.7f, 0.2f, 3f, 1.5f, 5f };
    void Start()
    {
        
        //Dimension = MainCamera.GetComponent<WaveGUI>().guidimesion;
        mesh = new Mesh();
        mesh.name = gameObject.name;

        mesh.vertices = GenerateVerts();
        mesh.triangles = GenerateTris();
        mesh.uv = GenerateUVs();
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        meshFilter = gameObject.GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;

    }
    private Vector3[] GenerateVerts()
    {
        var verts = new Vector3[(Dimension + 1) * (Dimension + 1)];

        for(int x = 0; x <= Dimension; x++)
            for(int z = 0; z<= Dimension; z++)
                verts[index(x, z)] = new Vector3(x, 0, z );
        return verts;
    }

    private int index(int x, int z)
    {
        return x * (Dimension + 1) + z;
        //x=0 z=0 =>index is 0   x=0 z=9=>index is 9   x=1 z=0=>index is 12
    }

    private int[] GenerateTris()
    {
        var tris = new int[mesh.vertices.Length * 6];//

        for(int x =0; x< Dimension; x++)
        {
            for(int z =0; z<Dimension;z++)
            {
                tris[index(x, z) * 6 + 0] = index(x, z);
                tris[index(x, z) * 6 + 1] = index(x+1, z+1);
                tris[index(x, z) * 6 + 2] = index(x+1, z);//these three vertices make the first triangle
                tris[index(x, z) * 6 + 3] = index(x, z);
                tris[index(x, z) * 6 + 4] = index(x, z+1);
                tris[index(x, z) * 6 + 5] = index(x+1, z+1);

            }
        }
        return tris;
    }

    public Vector2[] GenerateUVs()
    {
        var uvs = new Vector2[mesh.vertices.Length];

        //set the uv normal tiles than flip the uv and set it again
        for(int x = 0; x<= Dimension; x++)
        {
            for(int z =0; z <= Dimension; z++)
            {
                var vec = new Vector2((x / UVScale) % 2, (z / UVScale) % 2);
                uvs[index(x, z)] = new Vector2(vec.x <= 1 ? vec.x : 2 - vec.x, vec.y <= 1 ? vec.y : 2 - vec.y);
            }
        }
        return uvs;
    }
    //this is the procedual texture
    /*public Color[] GenerateColors()
    {
        colors = new Color[mesh.vertices.Length];
        for (int x = 0; x <= Dimension; x++)
        {
            for (int z = 0; z <= Dimension; z++)
            {
                float height = mesh.vertices[x].y;
                colors[index(x, z)] = gradient.Evaluate(height);
                x++;
            }
        }

        return colors;
    }*/


    // Update is called once per frame
    void Update()
    {
        var verts = mesh.vertices;
        for(int x=0; x<= Dimension; x++)
        {
            for(int z=0; z <= Dimension; z++)
            {
                var y = 0f;
                for(int o =0; o<Octaves.Length; o++)
                {
                    if(Octaves[o].alternate)
                    {
                        Octaves[o].scale.x = MainCamera.GetComponent<WaveGUI>().scalex;//element's x 's scale
                        Octaves[o].scale.y = MainCamera.GetComponent<WaveGUI>().scaley;
                        var pern = Mathf.PerlinNoise((x * Octaves[o].scale.x) / Dimension, (z * Octaves[o].scale.y) / Dimension) * Mathf.PI * 2f;
                        //basicly is PerlinNoise(x,y)
                        Octaves[o].speed.x = MainCamera.GetComponent<WaveGUI>().hSliderValuex;
                        Octaves[o].speed.y = MainCamera.GetComponent<WaveGUI>().hSliderValuey;
                        Octaves[o].height = MainCamera.GetComponent<WaveGUI>().height;

                        sindex = MainCamera.GetComponent<WaveGUI>().guisnwaveindex;
                        if(sindex == 1)
                        y += Mathf.Sin(pern + Octaves[o].speed.magnitude * Time.time) * Octaves[o].height;//make the plane move up and down

                        if(sindex == 2)
                        y += 2 * Mathf.Sin(pern + Octaves[o].speed.magnitude * Time.time) * Octaves[o].height;//2sinx

                        if(sindex == 3)
                        y += Mathf.Abs(Mathf.Sin(pern + Octaves[o].speed.magnitude * Time.time) * Octaves[o].height);//|sinx|
                        
                        if(sindex == 4)
                        y += 0.5f * Mathf.Abs(Mathf.Sin(pern + Octaves[o].speed.magnitude * Time.time) * Octaves[o].height);//0.5|sinx|

                        if(sindex == 5)
                        y += 1 - Mathf.Abs(Mathf.Sin(pern + Octaves[o].speed.magnitude * Time.time) * Octaves[o].height);//1-|sinx|

                        if(sindex == 6)
                        y +=  Mathf.Pow((1 - Mathf.Abs(Mathf.Sin(pern + Octaves[o].speed.magnitude * Time.time))),2) * Octaves[o].height;//(1-|sinx|)^2

                        if(sindex == 7)
                        y +=  Mathf.Pow(Mathf.Sin(pern + Octaves[o].speed.magnitude * Time.time),2) * Octaves[o].height;//sinx ^ 2

                        if(sindex == 8)
                        y +=  Mathf.Cos(2 *(pern + Octaves[o].speed.magnitude * Time.time)) * Octaves[o].height;//1-2(sinx^2)

                        // + per1 makes the plane waves
                    }

                    else
                    {
                        var per1 = Mathf.PerlinNoise((x * Octaves[o].scale.x + Time.time * Octaves[o].speed.x) / Dimension, (z * Octaves[o].scale.y + Time.time * Octaves[o].speed.y) / Dimension) - 0.5f;
                        y += per1 * Octaves[o].height;
                    }
                }
                verts[index(x, z)] = new Vector3(x, y, z);
            }
        }

        mesh.vertices = verts;
        mesh.RecalculateNormals();

        material = MainCamera.GetComponent<WaveGUI>().guimaterial;
        gameObject.GetComponent<MeshRenderer>().materials[0].CopyPropertiesFromMaterial(material);
    }

    [System.Serializable]
    public struct Octave
    {
        public Vector2 speed;
        public Vector2 scale;
        public float height;
        public bool alternate;
    }

    public void Awake()
    {


    }

}
