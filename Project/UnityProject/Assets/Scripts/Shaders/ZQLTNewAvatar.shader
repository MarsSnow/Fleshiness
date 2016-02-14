

Shader "ZQLT/NewAvatar" {
    Properties {
        _node_51 ("node_51", Vector) = (1,1,0.3,0)
        _flambert ("flambert", Range(0, 1.5)) = 1.5
        _ramp_tex ("ramp_tex", 2D) = "white" {}
        _cubemap ("cubemap", Cube) = "_Skybox" {}
        _diffuse ("diffuse", 2D) = "white" {}
        _rim_light ("color", Color) = (0.1911765,0.479919,0.7647059,1)
        _rim_thinkness ("thinkness", Range(0, 2)) = 0.5516128
        _rim_strenth ("strenth", Range(0, 2)) = 2
        _rim_power ("power", Range(0, 2)) = 1.602302
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
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _ramp_tex; uniform float4 _ramp_tex_ST;
            uniform sampler2D _diffuse; uniform float4 _diffuse_ST;
            uniform samplerCUBE _cubemap;
            uniform float _flambert;
            uniform float4 _node_51;
            uniform float _rim_thinkness;
            uniform float _rim_strenth;
            uniform float4 _rim_light;
            uniform float _rim_power;
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
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float node_95 = 0.5;
                float3 node_4 = i.normalDir;
                float2 node_13 = (pow(max((node_95+(node_95*dot(lightDirection,node_4))),0.0),_flambert)*float2(1,1));
                float2 node_281 = i.uv0;
                float3 finalColor = ((((_LightColor0.rgb*attenuation*tex2D(_ramp_tex,TRANSFORM_TEX(node_13, _ramp_tex)).rgb)+texCUBE(_cubemap,node_4).rgb)*tex2D(_diffuse,TRANSFORM_TEX(node_281.rg, _diffuse)).rgb)+pow((saturate(dot(node_4,_node_51.rgb))*saturate((1.0-pow(dot(node_4,viewDirection),_rim_thinkness)))*_rim_strenth*_rim_light.rgb),_rim_power));
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
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _ramp_tex; uniform float4 _ramp_tex_ST;
            uniform sampler2D _diffuse; uniform float4 _diffuse_ST;
            uniform samplerCUBE _cubemap;
            uniform float _flambert;
            uniform float4 _node_51;
            uniform float _rim_thinkness;
            uniform float _rim_strenth;
            uniform float4 _rim_light;
            uniform float _rim_power;
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
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float node_95 = 0.5;
                float3 node_4 = i.normalDir;
                float2 node_13 = (pow(max((node_95+(node_95*dot(lightDirection,node_4))),0.0),_flambert)*float2(1,1));
                float2 node_282 = i.uv0;
                float3 finalColor = ((((_LightColor0.rgb*attenuation*tex2D(_ramp_tex,TRANSFORM_TEX(node_13, _ramp_tex)).rgb)+texCUBE(_cubemap,node_4).rgb)*tex2D(_diffuse,TRANSFORM_TEX(node_282.rg, _diffuse)).rgb)+pow((saturate(dot(node_4,_node_51.rgb))*saturate((1.0-pow(dot(node_4,viewDirection),_rim_thinkness)))*_rim_strenth*_rim_light.rgb),_rim_power));
/// Final Color:
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    
}
