// 中清龙图Unity手游项目专用Shader 
// author:王宏亮
// 此Shader用于自发光物体的渲染，此Shader影响光贴图
// 参数 _MainTex 这个是发光贴图
//     _Tex 纹理贴图
//     _Color 发光颜色
//     _EmissionLM 发光强度

Shader "Self-Illumin/ZQLTEmission" {
Properties {
	_MainTex ("Light (RGB)", 2D) = "white" {}
	_Tex("Texture(RGB)", 2D) = "white"{}
	_Color("Illumin Color", Color) = (1,1,1,1)
	_EmissionLM("Emission", Float) = 1
}

SubShader {
	Tags{"RenderType" = "Opaque" "IgnoreProjector"="True"}
	Pass
	{// 用于烘焙自发光
		Tags{"LightMode" = "Vertex" }
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"
		#include "Lighting.cginc"
		
		uniform float4 _Color;
		struct vertOut
		{
			float4 pos:SV_POSITION;
			float4 color:COLOR;
		};
		
		vertOut vert(appdata_base v)
		{
			float3 c = ShadeVertexLights(v.vertex, v.normal);
			vertOut o;
			o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
			o.color = _Color * float4(c,1);
			return o;
		}
		
		float4 frag(vertOut i):COLOR
		{
			return i.color ;
		}
		
		ENDCG
	}
	
	Pass
	{// 用于PC显示贴图
		Tags{"LightMode" = "VertexLMRGBM"}
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"
		sampler2D _MainTex;
		float4 _MainTex_ST;
		sampler2D _Tex;
		float4 _Tex_ST;
		sampler2D unity_Lightmap;
		float4 unity_LightmapST;
		
		float _EmissionLM;
		
		struct appdata
		{
			float4 vertex : POSITION;
			float2 texcoord: TEXCOORD0;
			float2 texcoord1:TEXCOORD1;
		};
		struct v2f
		{
			float4 pos : SV_POSITION;
			float2 txuv: TEXCOORD0;
			float2 lmuv: TEXCOORD1;
		};
		
		v2f vert(appdata v)
		{
			v2f o;
			o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
			o.txuv = TRANSFORM_TEX(v.texcoord.xy, _Tex);
			o.lmuv = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
			return o;
		}
		
		half4 frag(v2f i):COLOR
		{
			half4 lightColor = tex2D(_MainTex, i.txuv.xy);
			half4 col = tex2D(_Tex, i.txuv.xy);
			half4 lm = tex2D(unity_Lightmap, i.lmuv.xy);
			col.rgb = col.rgb * DecodeLightmap(lm) + lightColor * _EmissionLM / 8.0;
			return col;
		}
		
		ENDCG
	}
	
	Pass
	{// 用于移动设备显示贴图
		Tags{"LightMode" = "VertexLM"}
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"
		sampler2D _MainTex;
		float4 _MainTex_ST;
		sampler2D _Tex;
		float4 _Tex_ST;
		sampler2D unity_Lightmap;
		float4 unity_LightmapST;
		
		float _EmissionLM;
		
		struct appdata
		{
			float4 vertex : POSITION;
			float2 texcoord: TEXCOORD0;
			float2 texcoord1:TEXCOORD1;
		};
		struct v2f
		{
			float4 pos : SV_POSITION;
			float2 txuv: TEXCOORD0;
			float2 lmuv: TEXCOORD1;
		};
		
		v2f vert(appdata v)
		{
			v2f o;
			o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
			o.txuv = TRANSFORM_TEX(v.texcoord.xy, _Tex);
			o.lmuv = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
			return o;
		}
		
		half4 frag(v2f i):COLOR
		{
			half4 lightColor = tex2D(_MainTex, i.txuv.xy);
			half4 col = tex2D(_Tex, i.txuv.xy);
			half4 lm = tex2D(unity_Lightmap, i.lmuv.xy);
			col.rgb = col.rgb * DecodeLightmap(lm) + lightColor * _EmissionLM / 8.0;
			return col;
		}
		
		ENDCG
	}
}
Fallback "Mobile/VertexLit"
}
