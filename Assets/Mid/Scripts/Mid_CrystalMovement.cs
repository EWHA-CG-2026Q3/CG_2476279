
using UnityEngine;

public class Mid_CrystalMovement : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 30f;

    [Header("Floating")]
    [SerializeField] private float floatHeight = 0.15f;
    [SerializeField] private float floatSpeed = 1.5f;

    private Vector3 startPosition;
    private Quaternion startRotation;

    void Start()
    {
        // 크리스탈의 최초 위치와 회전값 저장
        startPosition = transform.localPosition;
        startRotation = transform.localRotation;
    }

    void Update()
    {
        // 시간에 따른 Y축 회전 각도 계산
        float angle = rotationSpeed * Time.time * Mathf.Deg2Rad;

        // 회전 행렬을 직접 작성 (Y축 기준)
        Matrix4x4 rotationMatrix = Matrix4x4.identity;

        rotationMatrix.m00 = Mathf.Cos(angle);
        rotationMatrix.m02 = Mathf.Sin(angle);
        rotationMatrix.m20 = -Mathf.Sin(angle);
        rotationMatrix.m22 = Mathf.Cos(angle);

        // 행렬에서 회전값 추출 후 적용
        Quaternion rotation = rotationMatrix.rotation;
        transform.localRotation = startRotation * rotation;

        // Sin 함수를 이용한 위아래 움직임
        float offset = Mathf.Sin(Time.time * floatSpeed) * floatHeight;

        // 최초 위치를 기준으로 높이만 변경
        transform.localPosition = startPosition + Vector3.up * offset;
    }
}
