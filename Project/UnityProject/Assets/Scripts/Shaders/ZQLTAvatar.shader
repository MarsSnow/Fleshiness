// 中清龙图Unity手游项目专用Shader 
// author:王宏亮
// 此Shader用于渲染不透明角色

Shader "ZQLT/Avatar" {
Properties {
	_MainTex ("Base (RGB) Gloss (A)", 2D) = "white" {}
	_RimColor ("Rim Color", Color) = (1,1,1,1)
    _RimPower ("Rim Power", Range(0.1,8.0)) = 2.0
    _Strength ("Rim Strength", Range(0, 10.0)) = 0
}

SubShader {
	Tags { "RenderType"="Opaque" "IgnoreProjector"="True" }
	LOD 150
		
	CGPROGRAM
	#pragma surface surf Lambert

	sampler2D _MainTex;
	float4 _RimColor;
	float _RimPower;
	float _Strength;

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
			o.Emission = texColor + _RimColor.rgb * pow (rim, _RimPower) * _Strength;
		}
	}
	ENDCG
}

Fallback "VertexLit"
}
