// 中清龙图Unity手游项目专用Shader 
// author:王宏亮
// 此Shader用于显示AlphaTest穿透物体，例如树木，草等，不可用于半透明物体
// 参数：_Cutoff Alpha测试穿透比例默认0.5，也就是贴图Alpha值0.5一下的部分将被穿透

Shader "Transparent/ZQLTAlphaTest" {
Properties {
	_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
}

SubShader {
	Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout"}
	LOD 150
	Cull Off
CGPROGRAM
#pragma surface surf Lambert alphatest:_Cutoff

sampler2D _MainTex;

struct Input {
	float2 uv_MainTex;
};

void surf (Input IN, inout SurfaceOutput o) {
	fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
	o.Albedo = c.rgb;
	o.Alpha = c.a;
}
ENDCG
}

Fallback "Transparent/Cutout/VertexLit"
}
