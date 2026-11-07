
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Mid_MagicPillar : MonoBehaviour
{
    public int sides = 16;
    public float radius = 0.5f;
    public float height = 1.5f;

    void OnEnable()
    {
        Mesh mesh = new Mesh();
        mesh.name = "MagicPillar";

        Vector3[] vertices = new Vector3[sides * 2 + 2];
        int[] triangles = new int[sides * 12];

        // 반복문으로 원기둥의 위쪽과 아래쪽 정점 생성
        for (int i = 0; i < sides; i++)
        {
            float angle = 2f * Mathf.PI * i / sides;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;

            vertices[i] = new Vector3(x, 0, z);
            vertices[i + sides] = new Vector3(x, height, z);
        }

        vertices[sides * 2] = new Vector3(0, 0, 0);
        vertices[sides * 2 + 1] = new Vector3(0, height, 0);

        // 옆면, 윗면, 아랫면을 삼각형으로 연결
        for (int i = 0; i < sides; i++)
        {
            int next = (i + 1) % sides;
            int t = i * 12;

            triangles[t] = i;
            triangles[t + 1] = i + sides;
            triangles[t + 2] = next;

            triangles[t + 3] = next;
            triangles[t + 4] = i + sides;
            triangles[t + 5] = next + sides;

            triangles[t + 6] = sides * 2;
            triangles[t + 7] = next;
            triangles[t + 8] = i;

            triangles[t + 9] = sides * 2 + 1;
            triangles[t + 10] = i + sides;
            triangles[t + 11] = next + sides;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        GetComponent<MeshFilter>().mesh = mesh;
    }
}
