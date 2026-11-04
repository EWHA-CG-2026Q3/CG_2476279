Shader "CG/S16_MyCoal"
{
    // 재질 Inspector에서 색상을 설정할 수 있도록 속성 추가
    Properties
    {
        _BaseColor ("Base Color", Color) = (0, 0, 0, 1)
        [HDR] _EmissionColor ("Emission Color", Color) = (1, 1, 1, 1)
    }

    SubShader
    {
        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            // 재질에서 설정한 색상 값을 받는 변수
            float4 _BaseColor;
            float4 _EmissionColor;

            // 정점의 좌표를 화면에 그릴 좌표로 변환
            float4 vert(float3 positionOS : POSITION) : SV_POSITION
            {
                return TransformObjectToHClip(positionOS);
            }

            // 기본 색상과 발광 색상을 더해 최종 픽셀 색상 결정
            float4 frag() : SV_Target
            {
                return float4(_BaseColor.rgb + _EmissionColor.rgb, 1);
            }

            ENDHLSL
        }
    }
}