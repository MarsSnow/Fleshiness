// 中清龙图Unity手游项目专用Shader 
// author:王宏亮
// 此Shader用于渲染特效半透明材质
// 参数：1，带有Alpha通道的贴图一张 2，主颜色与贴图颜色为乘的关系

Shader "Particles/ZQLTAlphaBlended" {
Properties {
	_Alpha ("Alpha", Range(0,1.0)) = 1.0
	_TintColor("Tint Color", Color) = (0.5,0.5,0.5,1)
	_MainTex ("Texture(RGBA)", 2D) = "white" {}
}

Category {
	Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
	Blend SrcAlpha OneMinusSrcAlpha
	Cull Off Lighting Off ZWrite Off Fog { Color (0,0,0,0) }
	
	BindChannels {
		Bind "Color", color
		Bind "Vertex", vertex
		Bind "TexCoord", texcoord
	}
	
	SubShader {
		Pass {
			SetTexture [_MainTex] {
				combine texture * primary
			}
			
			SetTexture [_] {
				constantColor[_TintColor]
				combine previous * constant double
			}
			
			SetTexture [_] {
				constantColor(1,1,1,[_Alpha])
				combine previous * constant
			}
		}
	}
}
}
