using UnityEngine;

public class Mid_MagicTree : MonoBehaviour
{
    [Header("Tree Settings")]
    public int sides = 12;
    public float trunkHeight = 1.5f;
    public float leafHeight = 1.2f;
    public float leafRadius = 1.2f;

    [Header("Tree Materials")]
    public Material trunkMaterial;
    public Material leafMaterial;

    void Start()
    {
        // 나무 줄기 생성 및 재질 적용
        GameObject trunk = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        trunk.name = "Trunk";
        trunk.transform.SetParent(transform, false);
        trunk.transform.localPosition = new Vector3(0, trunkHeight / 2f, 0);
        trunk.transform.localScale = new Vector3(0.35f, trunkHeight / 2f, 0.35f);

        trunk.GetComponent<MeshRenderer>().sharedMaterial = trunkMaterial;

        // 원뿔 나뭇잎을 3층으로 생성
        for (int layer = 0; layer < 3; layer++)
        {
            float radius = leafRadius * (1f - layer * 0.2f);
            float y = trunkHeight * 0.5f + layer * 0.65f;

            GameObject leaves = new GameObject("MagicLeaves_" + layer);
            leaves.transform.SetParent(transform, false);
            leaves.transform.localPosition = new Vector3(0, y, 0);

            MeshFilter filter = leaves.AddComponent<MeshFilter>();
            filter.mesh = CreateCone(radius, leafHeight);

            // 나뭇잎에 지정한 재질 적용
            MeshRenderer renderer = leaves.AddComponent<MeshRenderer>();
            renderer.sharedMaterial = leafMaterial;
        }
    }

    Mesh CreateCone(float radius, float height)
    {
        Mesh mesh = new Mesh();
        int count = Mathf.Max(3, sides);

        Vector3[] vertices = new Vector3[count + 2];
        int[] triangles = new int[count * 6];

        vertices[0] = Vector3.zero;
        vertices[1] = new Vector3(0, height, 0);

        // 반복문으로 원뿔의 정점과 삼각형 생성
        for (int i = 0; i < count; i++)
        {
            float angle = 2f * Mathf.PI * i / count;

            vertices[i + 2] = new Vector3(
                Mathf.Cos(angle) * radius,
                0,
                Mathf.Sin(angle) * radius
            );

            int next = (i + 1) % count;

            triangles[i * 6] = 1;
            triangles[i * 6 + 1] = i + 2;
            triangles[i * 6 + 2] = next + 2;

            triangles[i * 6 + 3] = 0;
            triangles[i * 6 + 4] = next + 2;
            triangles[i * 6 + 5] = i + 2;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }
}