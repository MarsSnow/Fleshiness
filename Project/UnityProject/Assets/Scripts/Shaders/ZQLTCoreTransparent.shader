// 中清龙图Unity手游项目专用Shader 
// author:王宏亮
// 此Shader用于中心透明制作

Shader "Particles/ZQLTCoreTransparent" {
Properties {
	_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,1.0)
	_MainTex ("Base (RGB) Gloss (A)", 2D) = "white" {}
    _RimPower ("Rim Power", Range(0.1,8.0)) = 2.0
    _Strength ("Rim Strength", Range(0, 10.0)) = 0
    _Alpha("Overall Alpha", Range(0, 1.0)) = 1.0
}

SubShader {
	Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	LOD 150
	
	//ZWrite Off
	Blend SrcAlpha OneMinusSrcAlpha 
	
	CGPROGRAM
	#pragma surface surf Lambert

	sampler2D _MainTex;
	float _RimPower;
	float _Strength;
	float _Alpha;
	fixed4 _TintColor;

	struct Input {
		float2 uv_MainTex;
		float3 viewDir;
		float3 worldNormal; 
	};

	void surf (Input IN, inout SurfaceOutput o) {
		fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
		if(_Strength == 0)
		{
			o.Emission = tex.rgb * _TintColor * 2;
			o.Alpha = tex.a * _Alpha;
		}
		else
		{
			o.Emission = tex.rgb * _TintColor * 2;
			half rim = saturate(dot (normalize(IN.viewDir), IN.worldNormal));
			o.Alpha = tex.a * _Alpha * (1 - pow (rim, _RimPower) * _Strength);
		}
	}
	ENDCG
}

Fallback "VertexLit"
}
