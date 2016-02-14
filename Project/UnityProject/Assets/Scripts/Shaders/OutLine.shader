Shader "ImageEffect/OutLine" {

Properties {
	_MainTex ("", 2D) = "white" {}
}

Category {
	ZTest Always Cull Off ZWrite Off Fog { Mode Off }

	Subshader {
		Pass {
			CGPROGRAM
// Upgrade NOTE: excluded shader from DX11 and Xbox360; has structs without semantics (struct v2f members off)
#pragma exclude_renderers d3d11 xbox360
				#pragma vertex vert
				#pragma fragment frag
				#pragma fragmentoption ARB_precision_hint_fastest

				#include "UnityCG.cginc"

				struct v2f {
					float4 pos : POSITION;
					half2 uv : TEXCOORD0;
					float2 off;
				};

				float4 _MainTex_TexelSize;
				float4 _BlurOffsets;

				v2f vert (appdata_img v)
				{
					v2f o;
					float offX = _MainTex_TexelSize.x * _BlurOffsets.x;
					float offY = _MainTex_TexelSize.y * _BlurOffsets.y;

					o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
					o.uv = MultiplyUV (UNITY_MATRIX_TEXTURE0, v.texcoord.xy-float2(offX, offY));
					o.off.x = offX;
					o.off.y = offY;
					return o;
				}
				
				sampler2D _MainTex;

				fixed4 frag( v2f i ) : COLOR
				{
					fixed4 texColor = tex2D(_MainTex, i.uv);
					fixed width = 0.001;

					fixed out1 = tex2D(_MainTex, i.uv + float2(i.off.x,0)).a - texColor.a;
					fixed out2 = tex2D(_MainTex, i.uv + float2(-i.off.x,0)).a - texColor.a;
					fixed out3 = tex2D(_MainTex, i.uv + float2(0,i.off.y)).a - texColor.a;
					fixed out4 = tex2D(_MainTex, i.uv + float2(0,-i.off.y)).a - texColor.a;
					
					fixed myAlpha = (abs(out1) + abs(out2) + abs(out3) + abs(out4));
					
					fixed4 finalColor = fixed4(myAlpha,myAlpha,myAlpha,myAlpha);
					
					return finalColor;
					
				}
			ENDCG
		}
		
	}
}

Fallback off

}
