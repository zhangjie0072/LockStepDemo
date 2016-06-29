Shader "Custom/TransparentShadowShader" {

Properties
{
    _ShadowColor ("Shadow Color", Color) = (1,1,1,1)
}

Category {

Blend  SrcAlpha OneMinusSrcAlpha

Lighting Off
Zwrite Off
LOD 200


SubShader
{
    Tags { "RenderType"="Transparent" }

    CGPROGRAM
    #pragma surface surf Custom

    struct Input {
        float2 pos : POSITION;
    };

    uniform float4 _ShadowColor;

    void surf(Input IN, inout SurfaceOutput o)
    {
        //Pass through shadow colour to lighting model
        o.Albedo = _ShadowColor.rgb;
        o.Alpha  = _ShadowColor.a;
    }

    half4 LightingCustom(SurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
    {
        half4 c;

        //Inverse illumination - atten accounts for shadowing
        c.rgb = s.Albedo.rgb - atten;
        c.a   = s.Alpha - atten;

        return c;
    }
    ENDCG
}
}

Fallback "VertexLit", 2

}