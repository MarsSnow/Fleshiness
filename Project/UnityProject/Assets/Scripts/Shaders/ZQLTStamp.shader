// 中清龙图Unity手游项目专用Shader 
// author:王宏亮
// 此Shader用于显示半透明物体，一般用于地表印花的渲染

Shader "Transparent/ZQLTStamp" {
Properties {
	_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
}

SubShader {
	Tags {"Queue"="AlphaTest"  "RenderType"="Transparent"}
	LOD 150

CGPROGRAM
#pragma surface surf Lambert alpha

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

Fallback "Transparent/VertexLit"
}
