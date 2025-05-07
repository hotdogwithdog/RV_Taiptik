#pragma vertex vert
#pragma fragment frag
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

CBUFFER_START(UnityPerMaterial)
    uniform float4 BaseColor;
    uniform float4 EdgeColor;
CBUFFER_END

struct appdata
{
    float4 pos : POSITION; 
    float2 uv : TEXCOORD0;
};

struct v2f
{
    float4 pos : SV_POSITION;
    float2 uv : TEXCOORD0;
};


v2f vert(appdata v)
{
    v2f o;

    o.pos = TransformObjectToHClip(v.pos.xyz);

    o.uv = v.uv;

    return o;
}


float4 frag(v2f i) : SV_Target
{
    float condition = (0.1 <= i.uv.x && i.uv.x <= 0.9 && 0.1 <= i.uv.y && i.uv.y <= 0.9);

    float4 outColor = lerp(EdgeColor, BaseColor, condition);
    //float4 outColor = float4(condition, condition, condition, 1.0);
    //float4 outColor = float4(i.uv, 0.0, 1.0);

    return outColor;
}
