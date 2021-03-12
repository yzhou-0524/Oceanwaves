using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireFrame : MonoBehaviour
{
    public Material lineMaterial;
    private Mesh mesh;
    public void Start()
    {
         mesh = gameObject.GetComponent<Waves>().mesh;
    }


    public void OnRenderObject()
    {
        lineMaterial.SetPass(0);   

        GL.PushMatrix();
        GL.MultMatrix(transform.localToWorldMatrix);

        GL.Begin(GL.LINES);

        for (int cnt = 0; cnt < mesh.triangles.Length; cnt += 3)
        {
            GL.Vertex(mesh.vertices[mesh.triangles[cnt]]);
            GL.Vertex(mesh.vertices[mesh.triangles[cnt + 1]]);
            GL.Vertex(mesh.vertices[mesh.triangles[cnt + 1]]);
            GL.Vertex(mesh.vertices[mesh.triangles[cnt + 2]]);
            GL.Vertex(mesh.vertices[mesh.triangles[cnt + 2]]);
            GL.Vertex(mesh.vertices[mesh.triangles[cnt]]);
        }

        GL.End();
        GL.PopMatrix();
    }


}


