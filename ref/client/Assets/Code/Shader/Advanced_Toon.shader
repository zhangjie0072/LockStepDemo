Shader "Toon/Advanced Outline" 
{
    Properties 
    {
        _Color ("Main Color", Color) = (1,1,1,1)

        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        
        _Outline ("Outline width", float) = 0.01
        _OutlineThreshold ("Outline Threshold", float) = 0.0
        
        _MainTex ("Base (RGB)", 2D) = "white"
		//_Ramp ("Toon Ramp (RGB)", 2D) = "gray" {} 
		
		_Grey ("Grey Color", float) = 0.0
		_ToonShade ("ToonShader Cubemap(RGB)", CUBE) = "" { Texgen CubeNormal }
    }
 
    SubShader 
    {
        Tags { "Queue" = "Transparent" }
        Tags { "RenderType"="Opaque" }
        UsePass "Toon/Basic/BASE"
        Pass 
        {
            Name "OUTLINE_NO_Z"
            Tags { "LightMode" = "Always" }
            
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			 
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
			uniform float 	_Outline;
			uniform float4 	_OutlineColor;
			uniform float 	_OutlineThreshold;
			 
			v2f vert(appdata v) 
			{
			    v2f o;
			    o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
			    
			    float3 norm = mul ((float3x3)UNITY_MATRIX_MV, v.normal);
			    norm.x *= UNITY_MATRIX_P[0][0];
			    norm.y *= UNITY_MATRIX_P[1][1];
			    
			    norm.x = sign(norm.x) * max(abs(norm.x) - _OutlineThreshold, 0.0f);
			    norm.y = sign(norm.y) * max(abs(norm.y) - _OutlineThreshold, 0.0f);
			    
			    o.pos.xy += norm.xy * _Outline;
			   
			    o.color = _OutlineColor;
			    return o;
			}
			
			half4 frag(v2f i) :COLOR 
			{
				return i.color;
			}
			
			ENDCG
			
            //Color (0,0,0,0)
            Cull Front
            ZWrite On
            ColorMask RGB
            Blend SrcAlpha OneMinusSrcAlpha
            SetTexture [_MainTex] { combine primary }
        }
    }
   
    Fallback "Toon/Basic"
}