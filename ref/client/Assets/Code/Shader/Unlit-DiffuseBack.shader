Shader "Unlit/DiffuseBack" 
{
	Properties 
	{
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	}

	SubShader 
	{
		Tags {"Queue"="Transparent" "IgnoreProjector"="True"}
		Cull front
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert vertex:vert alpha

		void vert (inout appdata_full v) 
		{
			v.normal = -v.normal;
		}

		sampler2D _MainTex;
		fixed4 _Color;

		struct Input 
		{
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) 
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		
		ENDCG
	}

	Fallback "Unlit/Transparent"
}
