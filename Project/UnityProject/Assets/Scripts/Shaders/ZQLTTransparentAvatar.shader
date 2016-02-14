// 中清龙图Unity手游项目专用Shader 
// author:王宏亮
// 此Shader用于渲染半透明角色

Shader "ZQLT/TransparentAvatar" {
Properties {
	_MainTex ("Base (RGB) Gloss (A)", 2D) = "white" {}
	_RimColor ("Rim Color", Color) = (1,1,1,1)
    _RimPower ("Rim Power", Range(0.1,8.0)) = 2.0
    _Strength ("Rim Strength", Range(0, 10.0)) = 0
    _Alpha("Overall Alpha", Range(0, 1.0)) = 1.0
}

SubShader {
	Tags {"Queue"="Transparent-8" "IgnoreProjector"="True" "RenderType"="Transparent"}
	LOD 150
	
	ZWrite On
	Blend SrcAlpha OneMinusSrcAlpha 
	
	CGPROGRAM
	#pragma surface surf Lambert

	sampler2D _MainTex;
	float4 _RimColor;
	float _RimPower;
	float _Strength;
	float _Alpha;

	struct Input {
		float2 uv_MainTex;
		float3 viewDir;
		float3 worldNormal; 
	};

	void surf (Input IN, inout SurfaceOutput o) {
		fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
		
		fixed angle = 4.3 * 3.14159 / 7;
		fixed3 tempDir = fixed3(IN.viewDir.z*sin(angle) + IN.viewDir.x * cos(angle), IN.viewDir.y  , IN.viewDir.z*cos(angle) - IN.viewDir.x * sin(angle));
		half diss = saturate(dot (normalize(tempDir), IN.worldNormal));
		fixed3 texColor = tex.rgb + tex.rgb * pow(diss, 1.1);
		
		if(_Strength == 0)
		{
			o.Emission = texColor;
		}
		else
		{
			half rim = 1.0 - saturate(dot (normalize(IN.viewDir), IN.worldNormal));
			o.Emission = texColor +  _RimColor.rgb * pow (rim, _RimPower) * _Strength;
		}

		o.Alpha = tex.a * _Alpha;
	}
	ENDCG
}

Fallback "VertexLit"
}
