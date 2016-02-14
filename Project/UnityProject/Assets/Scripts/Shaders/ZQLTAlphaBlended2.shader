// 中清龙图Unity手游项目专用Shader 
// author:王谦
// 此Shader用于渲染特效半透明材质
// 参数：1，带有Alpha通道的贴图一张 2，主颜色与贴图颜色为乘的关系
Shader "Custom/ZQLTAlphaBlend" {
	Properties {
		_Alpha ("Alpha", Range(0,1.0)) = 1.0
		_TintColor("Tint Color", Color) = (0.5,0.5,0.5,1)
		_MainTex ("Texture(RGBA)", 2D) = "white" {}
	}
	SubShader {
		Tags { "Queue"="Transparent" "IgnoreProjector" = "True" "RenderType"="Transparent" }
		LOD 200
		
		Blend SrcAlpha OneMinusSrcAlpha
		AlphaTest Greater .01
		Cull Off
		Lighting Off
		ZWrite Off
		Fog { Color (0, 0, 0, 0) }
		
		pass {
			SetTexture [_MainTex] {
				constantColor ( 1, 1, 1, [_Alpha])
				Combine texture * constant
			}
			
			SetTexture [_] {
				constantColor [_TintColor]
				Combine previous * constant double
			}
		} 
	}
	FallBack "Diffuse"
}
