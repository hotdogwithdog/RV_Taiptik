#pragma vertex vert
#pragma fragment frag
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

CBUFFER_START(UnityPerMaterial)
    uniform float4 BaseColor;
    uniform float4 EdgeColor;
    uniform float EdgeWidht;
CBUFFER_END

struct appdata
{
    float4 pos : POSITION;
    float3 n : NORMAL;
};

struct v2f
{
    float4 pos : SV_POSITION;
    float3 N : NORMAL;
};


v2f vert(appdata v)
{
    v2f o;

    o.pos = TransformObjectToHClip(v.pos.xyz);

    o.N = mul(UNITY_MATRIX_IT_MV, float4(v.n, 0.0));

    return o;
}


float4 frag(v2f i) : SV_Target
{
    float condition = dot(i.N, float3(0.0, 0.0, 1.0)) > EdgeWidht;

    float4 outColor = lerp(EdgeColor, BaseColor, condition);
    //float4 outColor = float4(condition, condition, condition, 1.0);
    //float4 outColor = float4(i.uv, 0.0, 1.0);

    return outColor;
}
