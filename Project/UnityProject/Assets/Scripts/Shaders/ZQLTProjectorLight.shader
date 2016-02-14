// 中清龙图Unity手游项目专用Shader 
// author:王宏亮
// 此Shader用于显示显示投射贴图,需要使用正交投影方式Orthographic

Shader "ZQLT/ProjectorLight" {
  Properties {
     _ShadowTex ("Cookie", 2D) = "" { TexGen ObjectLinear }
  }
  Subshader {
     Pass {
        ZWrite off
        Fog { Color (0, 0, 0) }
        Color (1,1,1,1)
        ColorMask RGB
        Blend One One
		Offset -1, -1
        SetTexture [_ShadowTex] {
		   combine texture * primary , ONE - texture
           Matrix [_Projector]
        }
     }
  }
}