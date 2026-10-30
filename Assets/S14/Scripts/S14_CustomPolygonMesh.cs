using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class S14_CustomPolygonMesh : MonoBehaviour
{
    void OnEnable()
    {
        GenerateMesh();
    }

    void GenerateMesh()
    {
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(-1f, -0.5f, 0f),
            new Vector3(1f, -0.5f, 0f),
            new Vector3(1f, 0.5f, 0f),
            new Vector3(0f, 1.3f, 0f),
            new Vector3(-1f, 0.5f, 0f),
        };

        int[] triangles = new int[]
        {
            0, 1, 2,
            0, 2, 3,
            0, 3, 4,
        };

        Mesh mesh = new Mesh();
        mesh.name = "S14_PolygonMesh";
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        GetComponent<MeshFilter>().sharedMesh = mesh;
    }
}