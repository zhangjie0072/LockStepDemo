#warning Upgrade NOTE: unity_Scale shader variable was removed; replaced 'unity_Scale.w' with '1.0'

// Shader created with Shader Forge Beta 0.34 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.34;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:True,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,blpr:0,bsrc:0,bdst:0,culm:0,dpts:2,wrdp:True,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:31696,y:32397|diff-10028-OUT,spec-4921-OUT,normal-9985-OUT;n:type:ShaderForge.SFN_Tex2d,id:151,x:32720,y:31935,ptlb:Normal1,ptin:_Normal1,tex:fb6566c21f717904f83743a5a76dd0b0,ntxv:3,isnm:True|UVIN-9367-UVOUT;n:type:ShaderForge.SFN_Vector1,id:4921,x:32103,y:32342,v1:1;n:type:ShaderForge.SFN_Vector1,id:8608,x:33475,y:32372,v1:90;n:type:ShaderForge.SFN_Color,id:9036,x:32175,y:31985,ptlb:colour,ptin:_colour,glob:False,c1:1,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Panner,id:9367,x:33014,y:31839,spu:0.01,spv:0.01;n:type:ShaderForge.SFN_Tex2d,id:9581,x:32720,y:32131,ptlb:Normal2,ptin:_Normal2,tex:fb6566c21f717904f83743a5a76dd0b0,ntxv:3,isnm:True|UVIN-9713-UVOUT;n:type:ShaderForge.SFN_Panner,id:9713,x:33056,y:32105,spu:0.01,spv:0.01|UVIN-9882-UVOUT;n:type:ShaderForge.SFN_Rotator,id:9882,x:33276,y:32236|UVIN-9974-UVOUT,ANG-8608-OUT;n:type:ShaderForge.SFN_TexCoord,id:9974,x:33448,y:32141,uv:0;n:type:ShaderForge.SFN_Add,id:9985,x:32462,y:31968|A-151-RGB,B-9581-RGB,C-10220-OUT;n:type:ShaderForge.SFN_Add,id:10028,x:31784,y:32056|A-9036-RGB,B-10060-OUT;n:type:ShaderForge.SFN_ScreenPos,id:10039,x:32420,y:32104,sctp:0;n:type:ShaderForge.SFN_Multiply,id:10060,x:32040,y:32119|A-10039-V,B-10072-OUT;n:type:ShaderForge.SFN_Slider,id:10072,x:32354,y:32364,ptlb:lightRate,ptin:_lightRate,min:-1,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:10220,x:32720,y:32330,ptlb:specularRate,ptin:_specularRate,min:-1,cur:0,max:1;proporder:151-9581-9036-10072-10220;pass:END;sub:END;*/

Shader "Assets/water" {
    Properties {
        _Normal1 ("Normal1", 2D) = "bump" {}
        _Normal2 ("Normal2", 2D) = "bump" {}
        _colour ("colour", Color) = (1,0,0,1)
        _lightRate ("lightRate", Range(-1, 1)) = 0
        _specularRate ("specularRate", Range(-1, 1)) = 0
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _Normal1; uniform float4 _Normal1_ST;
            uniform float4 _colour;
            uniform sampler2D _Normal2; uniform float4 _Normal2_ST;
            uniform float _lightRate;
            uniform float _specularRate;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 binormalDir : TEXCOORD4;
                float4 screenPos : TEXCOORD5;
                LIGHTING_COORDS(6,7)
                float3 shLight : TEXCOORD8;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.shLight = ShadeSH9(float4(mul(_Object2World, float4(v.normal,0)).xyz * 1.0,1)) * 0.5;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.binormalDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.screenPos = o.pos;
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
/////// Normals:
                float4 node_10248 = _Time + _TimeEditor;
                float2 node_9367 = (i.uv0.rg+node_10248.g*float2(0.01,0.01));
                float node_9882_ang = 90.0;
                float node_9882_spd = 1.0;
                float node_9882_cos = cos(node_9882_spd*node_9882_ang);
                float node_9882_sin = sin(node_9882_spd*node_9882_ang);
                float2 node_9882_piv = float2(0.5,0.5);
                float2 node_9882 = (mul(i.uv0.rg-node_9882_piv,float2x2( node_9882_cos, -node_9882_sin, node_9882_sin, node_9882_cos))+node_9882_piv);
                float2 node_9713 = (node_9882+node_10248.g*float2(0.01,0.01));
                float3 normalLocal = (UnpackNormal(tex2D(_Normal1,TRANSFORM_TEX(node_9367, _Normal1))).rgb+UnpackNormal(tex2D(_Normal2,TRANSFORM_TEX(node_9713, _Normal2))).rgb+_specularRate);
                float3 normalDirection =  normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 diffuse = max( 0.0, NdotL) * attenColor;
///////// Gloss:
                float gloss = 0.5;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                NdotL = max(0.0, NdotL);
                float node_4921 = 1.0;
                float3 specularColor = float3(node_4921,node_4921,node_4921);
                float3 specular = (floor(attenuation) * _LightColor0.xyz) * pow(max(0,dot(halfDirection,normalDirection)),specPow) * specularColor;
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                diffuseLight += i.shLight; // Per-Vertex Light Probes / Spherical harmonics
                finalColor += diffuseLight * (_colour.rgb+(i.screenPos.g*_lightRate));
                finalColor += specular;
/// Final Color:
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ForwardAdd"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            Fog { Color (0,0,0,0) }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _Normal1; uniform float4 _Normal1_ST;
            uniform float4 _colour;
            uniform sampler2D _Normal2; uniform float4 _Normal2_ST;
            uniform float _lightRate;
            uniform float _specularRate;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 binormalDir : TEXCOORD4;
                float4 screenPos : TEXCOORD5;
                LIGHTING_COORDS(6,7)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.binormalDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.screenPos = o.pos;
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
/////// Normals:
                float4 node_10250 = _Time + _TimeEditor;
                float2 node_9367 = (i.uv0.rg+node_10250.g*float2(0.01,0.01));
                float node_9882_ang = 90.0;
                float node_9882_spd = 1.0;
                float node_9882_cos = cos(node_9882_spd*node_9882_ang);
                float node_9882_sin = sin(node_9882_spd*node_9882_ang);
                float2 node_9882_piv = float2(0.5,0.5);
                float2 node_9882 = (mul(i.uv0.rg-node_9882_piv,float2x2( node_9882_cos, -node_9882_sin, node_9882_sin, node_9882_cos))+node_9882_piv);
                float2 node_9713 = (node_9882+node_10250.g*float2(0.01,0.01));
                float3 normalLocal = (UnpackNormal(tex2D(_Normal1,TRANSFORM_TEX(node_9367, _Normal1))).rgb+UnpackNormal(tex2D(_Normal2,TRANSFORM_TEX(node_9713, _Normal2))).rgb+_specularRate);
                float3 normalDirection =  normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 diffuse = max( 0.0, NdotL) * attenColor;
///////// Gloss:
                float gloss = 0.5;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                NdotL = max(0.0, NdotL);
                float node_4921 = 1.0;
                float3 specularColor = float3(node_4921,node_4921,node_4921);
                float3 specular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow) * specularColor;
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                finalColor += diffuseLight * (_colour.rgb+(i.screenPos.g*_lightRate));
                finalColor += specular;
/// Final Color:
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
