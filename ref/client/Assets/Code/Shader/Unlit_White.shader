Shader "Unlit/PureColor" 
{
    Properties 
    {
		_Color ("Color", Color) = (1.0, 1.0, 1.0, 1.0)
    }
    
    CGINCLUDE
	#include "UnityCG.cginc"

	uniform float4 	_Color;

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
		o.color = _Color;
		
		return o;
	}
    ENDCG
    
    SubShader 
    {
		Tags { "RenderType" = "Opaque" } 
		Pass 
		{
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
