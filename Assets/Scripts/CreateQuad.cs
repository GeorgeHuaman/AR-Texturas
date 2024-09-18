using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateQuad : MonoBehaviour
{
    public MeshFilter meshFilter;
    public MeshFilter meshFilterQR;
    public Mesh mesh;
    public Mesh meshQR;
    public float width;
    public float height;

    private void Update()
    {
        if (!meshFilter && Manager.Instance.arObject != null)
        {
            meshFilter = Manager.Instance.arObject.GetComponent<MeshFilter>();
        }
        if (!meshFilterQR && Manager.Instance.arObjectQR != null)
        {
            meshFilterQR = Manager.Instance.arObjectQR.GetComponent<MeshFilter>();
        }
    }
    public void CreateWall()
    {
        if (meshFilter != null)
        {
            Mesh newMesh = new Mesh();
            newMesh = mesh;

            Vector3[] vertices = new Vector3[4];

            vertices[0] = new Vector3(0, 0); // Abajo izquierda
            vertices[1] = new Vector3(0, height / 100); // Arriba izquierda
            vertices[2] = new Vector3(width / 100, height / 100); // Arriba derecha
            vertices[3] = new Vector3(width / 100, 0); // Abajo derecha

            // Calcula el centro del mesh
            Vector3 center = (vertices[0] + vertices[1] + vertices[2] + vertices[3]) / 4;

            // Traslada los vértices para centrar el pivote
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] -= center;
            }

            newMesh.vertices = vertices;

            meshFilter.mesh = newMesh;
        }
        
    }
    public void CreateWallQR()
    {
        if (meshFilterQR != null)
        {
            Mesh newMesh = new Mesh();
            newMesh = meshQR;

            Vector3[] vertices = new Vector3[4];

            vertices[0] = new Vector3(0, 0); // Abajo izquierda
            vertices[1] = new Vector3(0, height / 100); // Arriba izquierda
            vertices[2] = new Vector3(width / 100, height / 100); // Arriba derecha
            vertices[3] = new Vector3(width / 100, 0); // Abajo derecha

            // Calcula el centro del mesh
            Vector3 center = (vertices[0] + vertices[1] + vertices[2] + vertices[3]) / 4;

            // Traslada los vértices para centrar el pivote
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] -= center;
            }

            newMesh.vertices = vertices;

            meshFilterQR.mesh = newMesh;
        }

    }
}
