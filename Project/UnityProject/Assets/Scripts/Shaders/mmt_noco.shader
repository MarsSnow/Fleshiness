

Shader "ZQLT/mmt_No_Culling" {
    Properties {
        //_ramp_tex ("ramp_tex", 2D) = "white" {}
        _cubemap ("cubemap", Cube) = "_Skybox" {}
        _hf_s ("hf_s", Range(0, 2)) = 0.5
        _flambert_c ("flambert_c", Range(0, 1.5)) = 1.5
        _diffuse_tex ("diffuse_tex", 2D) = "white" {}
        _g_rimpower ("g_rimpower", Range(0, 2)) = 0.5516128
        _node_51 ("node_51", Vector) = (1,1,0.3,0)
        _g_rimpara ("g_rimpara", Range(0, 2)) = 2
        _rim_color ("rim_color", Color) = (0.1911765,0.479919,0.7647059,1)
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
            Cull off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers d3d11 xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            //uniform sampler2D _ramp_tex; uniform float4 _ramp_tex_ST;
            uniform sampler2D _diffuse_tex; uniform float4 _diffuse_tex_ST;
            uniform samplerCUBE _cubemap;
            uniform float _flambert_c;
            uniform float4 _node_51;
            uniform float _g_rimpower;
            uniform float _g_rimpara;
            uniform float4 _rim_color;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 uv0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.uv0;
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
                float2 node_13 = (pow(max((node_95+(node_95*dot(lightDirection,node_4))),0.0),_flambert_c)*float2(1,1));
                float2 node_115 = i.uv0;
                float3 finalColor = ((((_LightColor0.rgb*attenuation)+texCUBE(_cubemap,node_4).rgb)*tex2D(_diffuse_tex,TRANSFORM_TEX(node_115.rg, _diffuse_tex)).rgb)+(clamp(dot(node_4,_node_51.rgb), 0.0, 1.0)*clamp((1.0-pow(dot(node_4,viewDirection),_g_rimpower)), 0.0, 1.0)*_g_rimpara*_rim_color.rgb));
/// Final Color:
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    
}
