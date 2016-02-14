
Shader "ZQLT/LightDFAvatar" {
    Properties {
        _ramp_tex ("ramp_tex", 2D) = "white" {}
        _cubemap ("cubemap", Cube) = "_Skybox" {}
        _Flambert ("Flambert", Range(0, 1.5)) = 1.5
        _diffuse ("diffuse", 2D) = "white" {}
        _rimlight ("rim light", Color) = (0.1,0.4,0.7,1)
        _Rimthickness ("Rim thickness", Range(0, 4)) = 0
        _Rimpower ("Rim power", Range(0, 2)) = 1.017094
        _Rimpower2 ("Rim power2", Range(0, 2)) = 1.324786
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _ramp_tex; uniform float4 _ramp_tex_ST;
            uniform sampler2D _diffuse; uniform float4 _diffuse_ST;
            uniform samplerCUBE _cubemap;
            uniform float _Flambert;
            uniform float4 _rimlight;
            uniform float _Rimthickness;
            uniform float _Rimpower;
            uniform float _Rimpower2;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
/////// Normals:
                float3 normalDirection =  i.normalDir;
                
                float nSign = sign( dot( viewDirection, i.normalDir ) ); // Reverse normal if this is a backface
                i.normalDir *= nSign;
                normalDirection *= nSign;
                
                float2 node_314 = i.uv0;
                float4 node_19 = tex2D(_diffuse,TRANSFORM_TEX(node_314.rg, _diffuse));
                clip(node_19.a - 0.5);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float node_95 = 0.5;
                float3 node_4 = i.normalDir;
                float node_2 = dot(lightDirection,node_4);
                float2 node_13 = (pow(max((node_95+(node_95*node_2)),0.0),_Flambert)*float2(1,1));
                float3 finalColor = ((((attenuation*_LightColor0.rgb*tex2D(_ramp_tex,TRANSFORM_TEX(node_13, _ramp_tex)).rgb)+texCUBE(_cubemap,node_4).rgb)*node_19.rgb)+pow(((node_2.r*(_rimlight.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),_Rimthickness)*_Rimpower))*_Rimpower2),2.0));
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
            Cull Off
            
            
            Fog { Color (0,0,0,0) }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _ramp_tex; uniform float4 _ramp_tex_ST;
            uniform sampler2D _diffuse; uniform float4 _diffuse_ST;
            uniform samplerCUBE _cubemap;
            uniform float _Flambert;
            uniform float4 _rimlight;
            uniform float _Rimthickness;
            uniform float _Rimpower;
            uniform float _Rimpower2;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
/////// Normals:
                float3 normalDirection =  i.normalDir;
                
                float nSign = sign( dot( viewDirection, i.normalDir ) ); // Reverse normal if this is a backface
                i.normalDir *= nSign;
                normalDirection *= nSign;
                
                float2 node_315 = i.uv0;
                float4 node_19 = tex2D(_diffuse,TRANSFORM_TEX(node_315.rg, _diffuse));
                clip(node_19.a - 0.5);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float node_95 = 0.5;
                float3 node_4 = i.normalDir;
                float node_2 = dot(lightDirection,node_4);
                float2 node_13 = (pow(max((node_95+(node_95*node_2)),0.0),_Flambert)*float2(1,1));
                float3 finalColor = ((((attenuation*_LightColor0.rgb*tex2D(_ramp_tex,TRANSFORM_TEX(node_13, _ramp_tex)).rgb)+texCUBE(_cubemap,node_4).rgb)*node_19.rgb)+pow(((node_2.r*(_rimlight.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),_Rimthickness)*_Rimpower))*_Rimpower2),2.0));
/// Final Color:
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCollector"
            Tags {
                "LightMode"="ShadowCollector"
            }
            Cull Off
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCOLLECTOR
            #define SHADOW_COLLECTOR_PASS
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcollector
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform sampler2D _diffuse; uniform float4 _diffuse_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_COLLECTOR;
                float2 uv0 : TEXCOORD5;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_SHADOW_COLLECTOR(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                float2 node_316 = i.uv0;
                float4 node_19 = tex2D(_diffuse,TRANSFORM_TEX(node_316.rg, _diffuse));
                clip(node_19.a - 0.5);
                SHADOW_COLLECTOR_FRAGMENT(i)
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Cull Off
            Offset 1, 1
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform sampler2D _diffuse; uniform float4 _diffuse_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                float2 node_317 = i.uv0;
                float4 node_19 = tex2D(_diffuse,TRANSFORM_TEX(node_317.rg, _diffuse));
                clip(node_19.a - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "CustomMaterialInspector"
}
