Shader "ProFlares/Demo/LeavesShader" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_Shininess ("Shininess", Range (0.01, 1)) = 0.078125
	_MainTex ("Base (RGB) Alpha (A)", 2D) = "white" {}
	_BumpMap ("Normalmap", 2D) = "bump" {}
	_GlossMap ("Gloss (A)", 2D) = "black" {}
	_TranslucencyMap ("Translucency (A)", 2D) = "white" {}
	_ShadowOffset ("Shadow Offset (A)", 2D) = "black" {}
	
	// These are here only to provide default values
	_Cutoff ("Alpha cutoff", Range(0,1)) = 0.3
	_Scale ("Scale", Vector) = (1,1,1,1)
	_Amount ("Amount", Float) = 1
	
	_Wind("Wind params",Vector) = (1,1,1,1)
	_WindEdgeFlutter("Wind edge fultter factor", float) = 0.5
	_WindEdgeFlutterFreqScale("Wind edge fultter freq scale",float) = 0.5
}

SubShader { 
	Tags { "IgnoreProjector"="True" "RenderType"="TreeLeaf" }
	LOD 200
	Cull Off
CGPROGRAM
#pragma surface surf TreeLeaf alphatest:_Cutoff  vertex:vert addshadow nolightmap
//#pragma surface surf TreeLeaf alphatest:_Cutoff vertex:TreeVertLeaf addshadow nolightmap
#include "TerrainEngine.cginc"
#pragma target 3.0
#pragma exclude_renderers flash
#pragma glsl_no_auto_normalization
//#include "Tree.cginc"

sampler2D _MainTex;
sampler2D _BumpMap;
sampler2D _GlossMap;
sampler2D _TranslucencyMap;
half _Shininess;


fixed4 _Color;
fixed3 _TranslucencyColor;
fixed _TranslucencyViewDependency;
half _ShadowStrength;

float _WindEdgeFlutter;
float _WindEdgeFlutterFreqScale;

struct Input {
	float2 uv_MainTex;
	fixed4 color : COLOR; // color.a = AO
};

struct LeafSurfaceOutput {
	fixed3 Albedo;
	fixed3 Normal;
	fixed3 Emission;
	fixed3 Translucency;
	half Specular;
	fixed Gloss;
	fixed Alpha;
};

float _Amount;

inline float4 AnimateVertex2(float4 pos, float3 normal, float4 animParams,float4 wind,float2 time)
{	
	// animParams stored in color
	// animParams.x = branch phase
	// animParams.y = edge flutter factor
	// animParams.z = primary factor
	// animParams.w = secondary factor

	float fDetailAmp = 0.1f;
	float fBranchAmp = 0.3f;
	
	// Phases (object, vertex, branch)
	float fObjPhase = dot(_Object2World[3].xyz, 1);
	float fBranchPhase = fObjPhase + animParams.x;
	
	float fVtxPhase = dot(pos.xyz, animParams.y + fBranchPhase);
	
	// x is used for edges; y is used for branches
	float2 vWavesIn = time  + float2(fVtxPhase, fBranchPhase );
	
	// 1.975, 0.793, 0.375, 0.193 are good frequencies
	float4 vWaves = (frac( vWavesIn.xxyy * float4(1.975, 0.793, 0.375, 0.193) ) * 2.0 - 1.0);
	
	vWaves = SmoothTriangleWave( vWaves );
	float2 vWavesSum = vWaves.xz + vWaves.yw;

	// Edge (xz) and branch bending (y)
	float3 bend = animParams.y * fDetailAmp * normal.xyz;
	bend.y = animParams.w * fBranchAmp;
	pos.xyz += ((vWavesSum.xyx * bend) + (wind.xyz * vWavesSum.y * animParams.w)) * wind.w; 

	// Primary bending
	// Displace position
	pos.xyz += animParams.z * wind.xyz;
	
	return pos;
}

void vert (inout appdata_full v) {
  	//v.vertex.xyz += v.normal * _Amount;
  
  
	 float4	wind;
		
	float bendingFact	= v.color.a;//_Amount;//v.color.a;
	
	wind.xyz	= mul((float3x3)_World2Object,_Wind.xyz);
	wind.w		= _Wind.w  * bendingFact;
	
	
	float4	windParams	= float4(0,_WindEdgeFlutter,bendingFact.xx);
	float 	windTime 	= _Time.y * float2(_WindEdgeFlutterFreqScale,1);
	float4	mdlPos		= AnimateVertex2(v.vertex,v.normal,windParams,wind,windTime);
	
	//o.pos = mul(UNITY_MATRIX_MVP,mdlPos);
	//o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
  	
	  v.vertex.xyz = mdlPos;//mul(UNITY_MATRIX_MVP,mdlPos);
  	//v.vertex.y = v.vertex.y + _Amount;
}



void surf (Input IN, inout LeafSurfaceOutput o) {
	fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
	//o.Albedo = c.rgb * _Color.rgb * IN.color.a;
	o.Albedo = c.rgb * _Color.rgb * IN.color;
	o.Translucency = tex2D(_TranslucencyMap, IN.uv_MainTex).rgb;
	o.Gloss = _Shininess;
	o.Alpha = c.a;
	o.Specular = UNITY_SAMPLE_1CHANNEL(_GlossMap, IN.uv_MainTex);
	o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
}

inline half4 LightingTreeLeaf_PrePass (LeafSurfaceOutput s, half4 light)
{
	fixed spec = light.a * s.Gloss;
	
	fixed4 c;
	c.rgb = (s.Albedo * light.rgb + light.rgb * _SpecColor.rgb * spec);
	c.a = s.Alpha + spec * _SpecColor.a;
	return c;
}


inline half4 LightingTreeLeaf_DirLightmap (LeafSurfaceOutput s, fixed4 color, fixed4 scale, half3 viewDir, bool surfFuncWritesNormal, out half3 specColor)
{
	UNITY_DIRBASIS
	half3 scalePerBasisVector;
	
	half3 lm = DirLightmapDiffuse (unity_DirBasis, color, scale, s.Normal, surfFuncWritesNormal, scalePerBasisVector);
	
	half3 lightDir = normalize (scalePerBasisVector.x * unity_DirBasis[0] + scalePerBasisVector.y * unity_DirBasis[1] + scalePerBasisVector.z * unity_DirBasis[2]);
	half3 h = normalize (lightDir + viewDir);

	float nh = max (0, dot (s.Normal, h));
	float spec = pow (nh, s.Specular * 128.0);
	
	// specColor used outside in the forward path, compiled out in prepass
	specColor = lm * _SpecColor.rgb * s.Gloss * spec;
	
	// spec from the alpha component is used to calculate specular
	// in the Lighting*_Prepass function, it's not used in forward
	return half4(lm, spec);
}


half4 LightingTreeLeaf (LeafSurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
{
	half3 h = normalize (lightDir + viewDir);
	
	half nl = dot (s.Normal, lightDir);
	
	half nh = max (0, dot (s.Normal, h));
	half spec = pow (nh, s.Specular * 128.0) * s.Gloss;
	
	// view dependent back contribution for translucency
	fixed backContrib = saturate(dot(viewDir, -lightDir));
	
	// normally translucency is more like -nl, but looks better when it's view dependent
	backContrib = lerp(saturate(-nl), backContrib, _TranslucencyViewDependency);
	
	fixed3 translucencyColor = backContrib * s.Translucency * _TranslucencyColor;
	
	// wrap-around diffuse
	nl = max(0, nl * 0.6 + 0.4);
	
	fixed4 c;
	c.rgb = s.Albedo * (translucencyColor * 2 + nl);
	c.rgb = c.rgb * _LightColor0.rgb + spec;
	
	
	c.rgb = c.rgb * _LightColor0.rgb;
	
	// For directional lights, apply less shadow attenuation
	// based on shadow strength parameter.
	#if defined(DIRECTIONAL) || defined(DIRECTIONAL_COOKIE)
	c.rgb *= lerp(2, atten * 2, _ShadowStrength);
	#else
	c.rgb *= 2*atten;
	#endif
	
	return c;
}

ENDCG
}

 FallBack "Transparent/Cutout/Diffuse"
}
