using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class S14_CustomDiamondMesh : MonoBehaviour
{
    void OnEnable()
    {
        GenerateMesh();
    }

    void GenerateMesh()
    {
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(0f, 0f, 0f),
            new Vector3(1f, 0f, 0f),
            new Vector3(1f, 0f, 1f),
            new Vector3(0f, 0f, 1f),
            new Vector3(0.5f, 1f, 0.5f),
            new Vector3(0.5f, -1f, 0.5f),
        };

        int[] triangles = new int[]
        {
            // 위쪽 4면
            4, 1, 0,
            4, 2, 1,
            4, 3, 2,
            4, 0, 3,

            // 아래쪽 4면
            5, 0, 1,
            5, 1, 2,
            5, 2, 3,
            5, 3, 0,
        };

        Mesh mesh = new Mesh();
        mesh.name = "S14_DiamondMesh";
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        GetComponent<MeshFilter>().sharedMesh = mesh;
    }
}