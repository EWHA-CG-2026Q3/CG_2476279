using UnityEngine;

public class Mid_MagicFlower : MonoBehaviour
{
    [Header("Flower Settings")]
    public int petalCount = 5;
    public float petalSize = 0.2f;
    public float stemHeight = 0.5f;
    public float rotationSpeed = 20f;

    [Header("Flower Materials")]
    public Material petalMaterial;
    public Material centerMaterial;
    public Material stemMaterial;

    void Start()
    {
        // 꽃 줄기 생성
        GameObject stem = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        stem.name = "Stem";
        stem.transform.SetParent(transform, false);
        stem.transform.localPosition = new Vector3(0, stemHeight / 2f, 0);
        stem.transform.localScale = new Vector3(0.04f, stemHeight / 2f, 0.04f);
        stem.GetComponent<MeshRenderer>().sharedMaterial = stemMaterial;

        // 꽃 중심 생성
        GameObject center = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        center.name = "FlowerCenter";
        center.transform.SetParent(transform, false);
        center.transform.localPosition = new Vector3(0, stemHeight, 0);
        center.transform.localScale = Vector3.one * 0.15f;
        center.GetComponent<MeshRenderer>().sharedMaterial = centerMaterial;

        // 반복문으로 꽃잎을 원형 배치
        for (int i = 0; i < petalCount; i++)
        {
            float angle = 2f * Mathf.PI * i / petalCount;

            GameObject petal = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            petal.name = "Petal_" + i;
            petal.transform.SetParent(transform, false);

            petal.transform.localPosition = new Vector3(
                Mathf.Cos(angle) * petalSize,
                stemHeight,
                Mathf.Sin(angle) * petalSize
            );

            petal.transform.localScale = new Vector3(
                petalSize,
                0.08f,
                petalSize
            );

            petal.GetComponent<MeshRenderer>().sharedMaterial = petalMaterial;
        }
    }

    void Update()
    {
        // 꽃 전체를 천천히 회전
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}