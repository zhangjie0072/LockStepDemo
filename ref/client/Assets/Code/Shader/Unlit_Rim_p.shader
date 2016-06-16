Shader "Unlit/Rim_P" 
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

	//per-pix
	struct vex_output 
	{
		float4 pos : POSITION;
		float3 normal : TEXCOORD0;
		float3 viewDir : TEXCOORD1;
	};
	vex_output vert(appdata v) 
	{
		vex_output o;
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
		o.viewDir = ObjSpaceViewDir( v.vertex );
		o.normal = v.normal;
		
		return o;
	}
	half4 frag(vex_output i) :COLOR 
	{
		half rim = 1.0 - saturate(dot (normalize(i.viewDir), i.normal));
		return float4(_RimColor.rgb * pow (rim, _RimPower),1.0);
	}
	
    ENDCG
    
    SubShader 
    {
		Tags { "RenderType" = "Opaque" } 
		Pass 
		{
			Name "RIM_P"
			
			ZWrite off
			Offset 0, -1
			Blend OneMinusDstColor One
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			ENDCG
		}
    } 
    Fallback "Diffuse"
}
