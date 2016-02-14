Shader "Spine/Skeleton" {
	Properties {
		_Cutoff ("Shadow alpha cutoff", Range(0,1)) = 0.1
		_MainTex ("Texture to blend", 2D) = "black" {}
		_LightColor("Light Color", Color) = (0,0,0,0)
		_Stone("Stone", Range(0,1)) = 0
		_OutlineWidth("Outline Width", Range(0, 1)) = 0
		_OutlineColor("Outline Color", Color) = (0,0,0,0)
	}
	// 2 texture stage GPUs
	SubShader {
		Tags { "Queue"="Transparent+100" "IgnoreProjector"="True" "RenderType"="Transparent" }
		LOD 100

		Cull Off
		ZWrite Off
		Blend One OneMinusSrcAlpha
		Lighting Off

		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile DUMMY PIXELSNAP_ON
			#include "UnityCG.cginc"
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				half2 texcoord  : TEXCOORD0;
			};

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color;

				return OUT;
			}

			sampler2D _MainTex;
			fixed4 _LightColor;
			fixed _Stone;
			fixed _OutlineWidth;
			fixed4 _OutlineColor;

			fixed4 frag(v2f IN) : COLOR
			{
				fixed4 texColor = tex2D(_MainTex, IN.texcoord) * IN.color;
				fixed4 finalColor = texColor + _LightColor * texColor.a;
				if(_Stone != 0)
				{
				    fixed grey = dot(texColor.rgb, float3(0.299, 0.587, 0.114));
				    fixed4 greyColor = fixed4(grey + 0.1, grey + 0.05, grey, 1) * texColor.a;
					finalColor = finalColor * (1 - _Stone) + greyColor * _Stone;
				}
				
				if(_OutlineWidth != 0)
				{
					fixed width = _OutlineWidth * 0.005;

					fixed out1 = tex2D(_MainTex, IN.texcoord + float2(width,0)).a - texColor.a;
					fixed out2 = tex2D(_MainTex, IN.texcoord + float2(-width,0)).a - texColor.a;
					fixed out3 = tex2D(_MainTex, IN.texcoord + float2(0,width)).a - texColor.a;
					fixed out4 = tex2D(_MainTex, IN.texcoord + float2(0,-width)).a - texColor.a;
					
					fixed myAlpha = (abs(out1) + abs(out2) + abs(out3) + abs(out4));
					
					finalColor = finalColor + fixed4(myAlpha,myAlpha,myAlpha,myAlpha) * IN.color * _OutlineColor;
					
				}

				return finalColor;

			}
		ENDCG
		}


		Pass {
			Name "Caster"
			Tags { "LightMode"="ShadowCaster" }
			Offset 1, 1
			
			Fog { Mode Off }
			ZWrite On
			ZTest LEqual
			Cull Off
			Lighting Off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_shadowcaster
			#pragma fragmentoption ARB_precision_hint_fastest
			#include "UnityCG.cginc"
			struct v2f { 
				V2F_SHADOW_CASTER;
				float2  uv : TEXCOORD1;
			};

			uniform float4 _MainTex_ST;

			v2f vert (appdata_base v) {
				v2f o;
				TRANSFER_SHADOW_CASTER(o)
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
				return o;
			}

			uniform sampler2D _MainTex;
			uniform fixed _Cutoff;

			float4 frag (v2f i) : COLOR {
				fixed4 texcol = tex2D(_MainTex, i.uv);
				clip(texcol.a - _Cutoff);
				SHADOW_CASTER_FRAGMENT(i)
			}
			ENDCG
		}
	}
	// 1 texture stage GPUs
	SubShader {
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
		LOD 100

		Cull Off
		ZWrite Off
		Blend One OneMinusSrcAlpha
		Lighting Off

		Pass {
			ColorMaterial AmbientAndDiffuse
			SetTexture [_MainTex] {
				Combine texture * primary DOUBLE, texture * primary
			}
		}
	}
}