// 中清龙图Unity手游项目专用Shader 
// author:王宏亮
// 此Shader用于特效流光效果，将Mask贴图的颜色坐标作为Main贴图的贴图坐标，进行坐标变换。
// 参数：1,_Alpha 透明度
//     2,_Color 颜色叠加值
//     3,_MainTex 主贴图，用于颜色采集
//     4,_MaskTex 遮罩贴图，用于贴图坐标采集
// 	   5,_Offset 坐标便宜，用于贴图动画
// 	   6,_Mut 亮度增倍系数，默认4

Shader "Particles/ZQLTTextureExtEffect" {
Properties {
	_Alpha ("Alpha", Range(0,1.0)) = 1.0
	_Color ("Color", Color) = (0.5,0.5,0.5,1.0)
	_MainTex ("Base Texture", 2D) = "white" {}	// 颜色贴图
	_MaskTex ("Mask",2D) = "white" {}			// 黑白贴图
	_Offset ("Offset", Range(0,100.0)) = 0
	_Mut("Mut", Range(0,10.0)) = 4
}
Category {
	Tags { "Queue"="Transparent+49" "IgnoreProjector"="True" "RenderType"="Transparent" }
	Blend SrcAlpha One
	ColorMask RGB
	Cull Off 
	Lighting Off 
	ZWrite Off 
	Fog { Color (0,0,0,0) }
	
	SubShader {
		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest

			#define TRANSFORM_TEX(tex,name) (tex.xy * name##_ST.xy + name##_ST.zw)

			sampler2D _MainTex;
			sampler2D _MaskTex;
			fixed4 _Color;
			float _Alpha;
			float _Offset;
			float _Mut;
			
			struct appdata_t {
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				float4 texcoord : TEXCOORD0;
				float2 texcoord1 : TEXCOORD1;
			};

			struct v2f {
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
				float2 texcoord1 : TEXCOORD1;
			};

			float4 _MainTex_ST;
			float4 _MaskTex_ST;

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.color = v.color;
				o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
				o.texcoord1 = TRANSFORM_TEX(v.texcoord1,_MaskTex);
				return o;
			}
			
			fixed4 frag (v2f i) : COLOR
			{
				fixed4 maskTex = tex2D(_MaskTex, i.texcoord1);
				float2 maskuv = float2(maskTex.x + _Offset, maskTex.x + _Offset);
				fixed4 mainTex = tex2D(_MainTex, maskuv);
				
				fixed4 outColor = lerp(fixed4(0,0,0,0),mainTex,maskTex);

				return _Mut * i.color * _Alpha * _Color * outColor;
			}
			ENDCG 
		}
	} 	
}
}
