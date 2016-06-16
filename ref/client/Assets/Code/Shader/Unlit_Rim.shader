Shader "Unlit/Rim" 
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

	struct v2f 
	{
		float4 pos : POSITION;
		float4 color : COLOR;
	};
    
    v2f vert(appdata v) 
	{
		v2f o;
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
		
		float3 viewDir = WorldSpaceViewDir( v.vertex );
		half rim = 1.0 - saturate(dot (normalize(viewDir), v.normal));
		o.color = float4(_RimColor.rgb * pow (rim, _RimPower),1.0);
		
		return o;
	}
    ENDCG
    
    SubShader 
    {
		Tags { "RenderType" = "Opaque" } 
		Pass 
		{
			Name "RIM"
			
			Blend One One 
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			half4 frag(v2f i) :COLOR 
			{
				return i.color;
			}
			ENDCG
		}
    } 
    Fallback "Diffuse"
}
