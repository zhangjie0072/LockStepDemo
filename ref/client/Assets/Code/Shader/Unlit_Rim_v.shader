Shader "Unlit/Rim_V" 
{
    Properties 
    {
		_RimColor ("Rim Color", Color) = (0.26,0.19,0.16,0.0)
		_RimPower ("Rim Power", Range(0.5,8.0)) = 3.0
    }
    
    CGINCLUDE
	#include "UnityCG.cginc"

	uniform float 	_RimPower;
	uniform float4 	_RimColor;
	
	struct appdata 
	{
		float4 vertex : POSITION;
		float3 normal : NORMAL;
	};
    //per-vertex
    struct vex_output 
	{
		float4 pos : POSITION;
		float4 color : COLOR;
	};
    vex_output vert_v(appdata v) 
	{
		vex_output o;
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
		
		float3 viewDir = ObjSpaceViewDir( v.vertex );
		half rim = 1.0 - saturate(dot (normalize(viewDir), v.normal));
		o.color = float4(_RimColor.rgb * pow (rim, _RimPower),1.0);
		
		return o;
	}
	half4 frag_v(vex_output i) :COLOR 
	{
		return i.color;
	}

    ENDCG
    
    SubShader 
    {
		Tags { "RenderType" = "Opaque" } 
		Pass 
		{
			Name "RIM_V"
			  
			ZWrite off
			Offset 0, -1
			Blend OneMinusDstColor One 
			CGPROGRAM
			#pragma vertex vert_v
			#pragma fragment frag_v
			ENDCG
		}
    } 
    Fallback "Diffuse"
}
