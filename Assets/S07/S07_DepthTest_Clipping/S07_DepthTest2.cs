using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class S07_DepthTest2 : MonoBehaviour
{
    [SerializeField] private int canvasWidth = 256;
    [SerializeField] private int canvasHeight = 256;
    //캔버스의 크기

    [SerializeField] private Vector3 vertexA1 = new Vector3(100, 180, 0.3f);
    [SerializeField] private Vector3 vertexB1 = new Vector3(60, 80, 0.3f);
    [SerializeField] private Vector3 vertexC1 = new Vector3(180, 80, 0.3f);
    [SerializeField] private Color color1 = new Color(1f, 0.4f, 0.2f, 1f);
    // 삼각형 하나 만듦(깊이가 모두 같음)
    //color1 선언 주황색 (r,g,b,a)

    [SerializeField] private Vector3 vertexA2 = new Vector3(150, 200, 0.0f);
    [SerializeField] private Vector3 vertexB2 = new Vector3(90, 60, 0.6f);
    [SerializeField] private Vector3 vertexC2 = new Vector3(220, 60, 0.6f);
    [SerializeField] private Color color2 = new Color(0.2f, 0.5f, 1f, 1f);
    //삼각형2
    //color2 선언 파란색

    [SerializeField] private Vector3 vertexA3 = new Vector3(210, 220, 0.3f);
    [SerializeField] private Vector3 vertexB3 = new Vector3(160, 50, 0.6f);
    [SerializeField] private Vector3 vertexC3 = new Vector3(250, 50, 0.9f);
    [SerializeField] private Color color3 = new Color(0.3f, 0.9f, 0.4f, 1f);
    //삼각형3 (A3가 가장 앞, B3, C3 순)
    //color3 선언 초록색

    private Texture2D canvasTexture;
    private RawImage targetImage; 
    //ui 요소 (canvasTexture에 그린 그림이 unity 화면에 나타나기 위함)

    private float[,] depthBuffer; 
    //깊이를 저장하는 2차원 배열 선언(쉼표 개수 1개)

    void OnEnable() { RedrawAll(); } //컴포넌트 활성화시 호출함수
    void OnValidate() { RedrawAll(); }//inspector 값 변경시 호출함수

    private void RedrawAll()
    {
        targetImage = GetComponent<RawImage>();
        //GameObject에서 RawImage 컴포넌트 가져오는 것
        if (targetImage == null) return;

        if (canvasTexture == null || canvasTexture.width != canvasWidth || canvasTexture.height != canvasHeight)
        {//위의 조건이면 cavasTexture 새로 만들어라
            canvasTexture = new Texture2D(canvasWidth, canvasHeight);
            canvasTexture.filterMode = FilterMode.Point;
            //Texture가 확대될 때 섞지 말고 픽셀 그대로 선명하게 보여달라는 것
        }

        depthBuffer = new float[canvasWidth, canvasHeight];
        //위에서 선언한 배열 실제 생성
        //모든 픽셀마다 z값 저장할 공간 만들기
        for (int x = 0; x < canvasWidth; x++)
            for (int y = 0; y < canvasHeight; y++)
                depthBuffer[x, y] = float.MaxValue; 
        //모든 픽셀 무한대로 초기화

        // TODO 0: 아래 세 줄의 순서를 원하는 대로 바꿔보세요.
        DrawTriangle(vertexA1, vertexB1, vertexC1, color1); //주황
        DrawTriangle(vertexA2, vertexB2, vertexC2, color2); //파랑
        DrawTriangle(vertexA3, vertexB3, vertexC3, color3); //초록

        canvasTexture.Apply(); 
        //SetPixel()로 변경한 픽셀 정보 texture에 최종 반영
    
        targetImage.texture = canvasTexture; 
        //만든 cavastexture을 rawImage(ui 요소)에 보이라는 뜻
    }

    private bool GetBarycentric(Vector2 p, Vector2 a, Vector2 b, Vector2 c, out float w1, out float w2, out float w3)
    { //픽셀이 삼각형 안인지 확인(w1,w2,w3는 P가 각 꼭짓점의 영향을 얼마나 받는지 보여줌)
        float denom = a.x * (b.y - c.y) + b.x * (c.y - a.y) + c.x * (a.y - b.y);
        w1 = (p.x * (b.y - c.y) + b.x * (c.y - p.y) + c.x * (p.y - b.y)) / denom;
        w2 = (a.x * (p.y - c.y) + p.x * (c.y - a.y) + c.x * (a.y - p.y)) / denom;
        w3 = 1f - w1 - w2;
        return w1 >= 0f && w2 >= 0f && w3 >= 0f;
        //세 값이 모두 0 이상이면 P가 삼각형 안에 있음.
    }

    private void DrawTriangle(Vector3 a, Vector3 b, Vector3 c, Color color)
    {
        Vector2 a2 = new Vector2(a.x, a.y);
        Vector2 b2 = new Vector2(b.x, b.y);
        Vector2 c2 = new Vector2(c.x, c.y);

        for (int x = 0; x < canvasWidth; x++)
        {
            for (int y = 0; y < canvasHeight; y++)
            {
                Vector2 pixelCenter = new Vector2(x + 0.5f, y + 0.5f);
                float w1, w2, w3;
                bool isInside = GetBarycentric(pixelCenter, a2, b2, c2, out w1, out w2, out w3);

                if (isInside)
                {
                    // TODO 1: w1, w2, w3와 a.z, b.z, c.z를 이용해 보간된 z를 계산하세요.
                    // 삼각형 내부의 픽셀의 깊이 계산
                    float interpolatedZ = w1 * a.z + w2 * b.z + w3 * c.z;

                    // TODO 2: interpolatedZ가 depthBuffer[x, y]보다 작을 때만 갱신하세요.
                    // 새 픽셀이 더 앞이면(가까우면) 삼각형 색 칠하기
                    if (interpolatedZ < depthBuffer[x, y])
                    {
                        canvasTexture.SetPixel(x, y, color); //해당 위치의 색 저장
                        depthBuffer[x, y] = interpolatedZ; //해당 위치의 깊이 저장
                    }
                }
            }
        }
    }
}