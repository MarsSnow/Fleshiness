// 中清龙图Unity手游项目专用Shader 
// author:王宏亮
// 此Shader用于按照贴图亮度渐从MainTex变为OtherTex

Shader "Particles/ZQLTBrightTransfrom" {
Properties {
	_MainTex ("Base Texture", 2D) = "white" {}		// 颜色贴图
	_OtherTex("Other Texture", 2D) = "white" {}
	_Progess("Progess", Range(-0.05,1.05)) = -0.05	// 变化进度
	_Width("Width", Range(0, 0.4)) = 0.05			// 渐变区域容差
	_EdgeColor ("Edge Color", Color) = (1,0,0,1)	// 渐变区域颜色
	_Alpha("Alpha", Range(0,1)) = 1
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
			sampler2D _OtherTex;
			
			float _Progess;
			float _Width;
			float4 _EdgeColor;
			float _Alpha;
			
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
				o.texcoord = v.texcoord;
				return o;
			}
			
			float4 frag (v2f i) : COLOR
			{
				float4 mainTex = tex2D(_MainTex, i.texcoord);
				float4 otherTex = tex2D(_OtherTex, i.texcoord);
				float progess = (_Progess)*(1+2*_Width) - _Width;
				
		    	float grey = dot(mainTex.rgb, float3(0.299, 0.587, 0.114));
		    	
		    	if(grey < progess - _Width)
		    	{// 亮度值大于进度值
		    		mainTex = otherTex;
		    	}
		    	else
		    	{
			    	if(grey < progess + _Width && grey > progess - _Width)
			    	{
			    		float scale = 1 - abs(grey - progess) / _Width;
			    		float alpha = 1;
			    		if(grey < progess)
			    		{
			    			alpha = scale;
			    		}
			    		
			    		mainTex = _EdgeColor * scale + otherTex*(1-scale);
			    		mainTex.a = otherTex.a;
			    	}
		    	}
		    	
		    	mainTex.a *= _Alpha;

				return mainTex;
			}
			ENDCG 
		}
	} 	
}
}
