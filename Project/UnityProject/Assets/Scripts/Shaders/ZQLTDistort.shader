// author:王谦
// 利用一个法线图对后面的背景造成扭曲效果

Shader "effect/Distort" {
Properties {
	_BumpAmt  ("Distortion", range (0,128)) = 10
	_BumpMap ("Normalmap", 2D) = "bump" {}
}

CGINCLUDE
#pragma fragmentoption ARB_precision_hint_fastest
#pragma fragmentoption ARB_fog_exp2
#include "UnityCG.cginc"

sampler2D _GrabTexture : register(s0);
float4 _GrabTexture_TexelSize;
sampler2D _BumpMap : register(s1);

struct v2f {
	float4 vertex : POSITION;
	float4 uvgrab : TEXCOORD0;
	float2 uvbump : TEXCOORD1;
};

uniform float _BumpAmt;


half4 frag( v2f i ) : COLOR
{
	// 计算扰动的坐标
	
	half2 bump = (UnpackNormal(tex2D( _BumpMap, i.uvbump)).r < 0.1 && UnpackNormal(tex2D( _BumpMap, i.uvbump)).g < 0.1) ?float2(0,0): UnpackNormal(tex2D( _BumpMap, i.uvbump )).rg; // we could optimize this by just reading the x & y without reconstructing the Z
	float2 offset = bump * _BumpAmt * _GrabTexture_TexelSize.xy;
	i.uvgrab.xy = offset * i.uvgrab.z + i.uvgrab.xy;
	
	return tex2Dproj( _GrabTexture, i.uvgrab );
}
ENDCG

Category {

	// 透明，在其它物体之后渲染
	Tags {"Queue"="Transparent+100" "RenderType"="Opaque" }


	SubShader {

		// 抓取屏幕相应位置的纹理
		GrabPass {							
			Name "BASE"
			Tags { "LightMode" = "Always" }
 		}
 		
 		// Main pass: 根据法线图对抓取的图片作像素的偏移
		Pass {
			Name "BASE"
			Tags { "LightMode" = "Always" }
			
CGPROGRAM
#pragma vertex vert
#pragma fragment frag

struct appdata_t {
	float4 vertex : POSITION;
	float2 texcoord: TEXCOORD0;
};

v2f vert (appdata_t v)
{
	v2f o;
	o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
	#if UNITY_UV_STARTS_AT_TOP
	float scale = -1.0;
	#else
	float scale = 1.0;
	#endif
	o.uvgrab.xy = (float2(o.vertex.x, o.vertex.y*scale) + o.vertex.w) * 0.5;
	o.uvgrab.zw = o.vertex.zw;
	o.uvbump = MultiplyUV( UNITY_MATRIX_TEXTURE1, v.texcoord );
	return o;
}
ENDCG
		}
	}

	// ------------------------------------------------------------------
	// 用于支持老版本的显卡
	
	SubShader {
		Blend DstColor Zero
		Pass {
			Name "BASE"
			SetTexture [_MainTex] {	combine texture }
		}
	}
}

}
