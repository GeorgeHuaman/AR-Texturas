using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateQuad : MonoBehaviour
{
    public MeshFilter meshFilter;
    public Mesh mesh;
    public float width;
    public float height;


    public void CreateWall()
    {
        Mesh newMesh = new Mesh();
        newMesh = mesh;

        // Define los v�rtices
        Vector3[] vertices = new Vector3[4];

        vertices[0] = new Vector3(0, 0); // Abajo izquierda
        vertices[1] = new Vector3(0, height / 100); // Arriba izquierda
        vertices[2] = new Vector3(width / 100, height / 100); // Arriba derecha
        vertices[3] = new Vector3(width / 100, 0); // Abajo derecha

        // Calcula el centro del mesh
        Vector3 center = (vertices[0] + vertices[1] + vertices[2] + vertices[3]) / 4;

        // Traslada los v�rtices para centrar el pivote
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] -= center;
        }

        // Asigna los v�rtices al mesh
        newMesh.vertices = vertices;

        // Asigna el mesh al MeshFilter
        meshFilter.mesh = newMesh;
    }
}
