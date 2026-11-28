
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class S23_VertexColorTest : MonoBehaviour
{
    void Start()
    {
        // 기존 Sphere 메시를 복제하여 원본을 보호
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        Mesh mesh = Instantiate(meshFilter.sharedMesh);

        // 정점 개수만큼 색상 배열 생성
        Vector3[] vertices = mesh.vertices;
        Color[] colors = new Color[vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            // 정점의 높이를 0~1 범위로 변환
            float t = Mathf.InverseLerp(-0.5f, 0.5f, vertices[i].y);

            // 아래쪽은 파란색, 위쪽은 빨간색
            colors[i] = Color.Lerp(Color.blue, Color.red, t);
        }

        // 정점 색상을 실제 Mesh에 저장
        mesh.colors = colors;
        meshFilter.mesh = mesh;
    }
}
