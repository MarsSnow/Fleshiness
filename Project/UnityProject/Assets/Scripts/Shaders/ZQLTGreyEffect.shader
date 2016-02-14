// 中清龙图Unity手游项目专用Shader 
// author:王宏亮
// 此Shader用于颜色由黑白图变彩色图效果效果，根据像素亮度值判读变幻过程
// 参数：1,_Alpha 透明度
//     2,_Color 颜色叠加值
//     3,_MainTex 主贴图，用于颜色采集
//     4,_MaskTex 遮罩贴图，用于贴图坐标采集
// 	   5,_Offset 坐标便宜，用于贴图动画
// 	   6,_Mut 亮度增倍系数，默认4

Shader "Particles/ZQLTGreyEffect" {
Properties {
	_Alpha ("Alpha", Range( 0, 1 )) = 1             //透明度
	_MainTex ("Base Texture", 2D) = "white" {}		// 颜色贴图
	_Progess("Progess", Range(-0.05,1.05)) = -0.05	// 变化进度
	_Width("Width", Range(0, 0.4)) = 0.05			// 渐变区域容差
	_EdgeColor ("Edge Color", Color) = (1,0,0,1)	// 渐变区域颜色
}

Category {
	Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
	Blend SrcAlpha OneMinusSrcAlpha 
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
			float _Progess;
			float _Width;
			float4 _EdgeColor;
			half _Alpha;
			
			struct appdata_t {
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.color = v.color;
				o.texcoord = v.texcoord;//TRANSFORM_TEX(,_MainTex);
				return o;
			}
			
			float4 frag (v2f i) : COLOR
			{
				float4 mainTex = tex2D(_MainTex, i.texcoord);
				
				float progess = (1-_Progess)*(1+2*_Width) - _Width;
				
		    	float grey = dot(mainTex.rgb, float3(0.299, 0.587, 0.114));
		    	
		    	if(grey < progess - _Width)
		    	{// 亮度值大于进度值
		    		mainTex.rgb = float3(grey, grey, grey);
		    	}
		    	else
		    	{
			    	if(grey < progess + _Width && grey > progess - _Width)
			    	{
			    		float scale = 1 - abs(grey - progess) / _Width;
			    		mainTex.rgb += _EdgeColor * float3(scale, scale, scale);
			    	}
		    	}
				mainTex.a = mainTex.a * _Alpha;
				
				return mainTex;
			}
			ENDCG 
		}
	} 	
}
}
