using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(DiamondMesh))]
public class S09_Shear : MonoBehaviour
{
    [SerializeField] float k = 2f;

    DiamondMesh diamondMesh;

    void OnEnable()
    {
        diamondMesh = GetComponent<DiamondMesh>();
    }

    void Update()
    {
        if (diamondMesh == null) return;

        float[,] M = ShearMatrixRaw(k);
        Vector3[] baseVertices = diamondMesh.BaseVertices;
        Vector3[] verts = new Vector3[baseVertices.Length];

        for (int i = 0; i < baseVertices.Length; i++)
        {
            Vector3 v = baseVertices[i];

            Vector4 h = new Vector4(v.x, v.y, v.z, 1f);
            Vector4 result = MultiplyMatrixVectorRaw(M, h);

            verts[i] = new Vector3(result.x, result.y, result.z);
        }

        diamondMesh.SetVertices(verts);
    }

    float[,] ShearMatrixRaw(float k)
    {
        return new float[,] {
            { 1f, k,  0f, 0f },
            { 0f, 1f, 0f, 0f },
            { 0f, 0f, 1f, 0f },
            { 0f, 0f, 0f, 1f }
        };
    }

    Vector4 MultiplyMatrixVectorRaw(float[,] M, Vector4 v)
    {
        float[] input = { v.x, v.y, v.z, v.w };
        float[] result = new float[4];

        for (int row = 0; row < 4; row++)
            for (int col = 0; col < 4; col++)
                result[row] += M[row, col] * input[col];

        return new Vector4(
            result[0], result[1], result[2], result[3]
        );
    }

    void OnValidate()
    {
        Vector3 top = new Vector3(0.5f, 1f, 0.5f);

        float newX = top.x + k * top.y;

        Debug.Log(
            $"k = {k}, 꼭대기 정점: {top} → ({newX}, {top.y}, {top.z})",
            this
        );
    }
}