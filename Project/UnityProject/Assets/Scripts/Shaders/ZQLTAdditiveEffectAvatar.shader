// 中清龙图Unity手游项目专用Shader 
// author:王宏亮
// 此Shader用于渲染不透明角色并叠加带有通道的自发光贴图
// _MainTex 与_Channel共享贴图坐标
// _AddTex 使用独立的贴图坐标，贴图动画在_AddTex上实现

Shader "ZQLT/AdditiveEffectAvatar" {
Properties {
	_MainTex ("Base (RGB) Gloss (A)", 2D) = "white" {}
	_Channel("Channel", 2D) = "white"{}
	_AddTex("Add Texture", 2D) = "black"{}
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
	sampler2D _Channel;
	sampler2D _AddTex;
	
	float4 _RimColor;
	float _RimPower;
	float _Strength;

	struct Input {
		float2 uv_MainTex;
		float2 uv_AddTex;
		float3 viewDir;
		float3 worldNormal; 
	};

	void surf (Input IN, inout SurfaceOutput o) {
		fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
		fixed4 channelTex = tex2D(_Channel, IN.uv_MainTex);
		fixed4 addTex = tex2D(_AddTex, IN.uv_AddTex);
		
		fixed angle = 4.3 * 3.14159 / 7;
		fixed3 tempDir = fixed3(IN.viewDir.z*sin(angle) + IN.viewDir.x * cos(angle), IN.viewDir.y  , IN.viewDir.z*cos(angle) - IN.viewDir.x * sin(angle));
		half diss = saturate(dot (normalize(tempDir), IN.worldNormal));
		fixed3 texColor = tex.rgb + tex.rgb * pow(diss, 1.1);
		
		if(_Strength == 0)
		{
			o.Emission = texColor + addTex * channelTex.r;
		}
		else
		{
			half rim = 1.0 - saturate(dot (normalize(IN.viewDir), IN.worldNormal));
			o.Emission = texColor + _RimColor.rgb * pow (rim, _RimPower) * _Strength + addTex * channelTex.r;
		}
	}
	ENDCG
}

Fallback "VertexLit"
}
