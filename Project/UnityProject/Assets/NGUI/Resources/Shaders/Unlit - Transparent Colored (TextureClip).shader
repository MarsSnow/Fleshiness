// Community contribution: http://www.tasharen.com/forum/index.php?topic=9268.0
Shader "Hidden/Unlit/Transparent Colored (TextureClip)"
{
	Properties
	{
		_MainTex ("Base (RGB), Alpha (A)", 2D) = "black" {}
		//add by jonny
		_AlphaTex ("Alpha (A)", 2D) = "black" {}
		_UseMainTexAlpha ("UseMainTexAlpha", int ) = 0
	}

	SubShader
	{
		LOD 200

		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
		}

		Pass
		{
			Cull Off
			Lighting Off
			ZWrite Off
			Offset -1, -1
			Fog { Mode Off }
			ColorMask RGB
			Blend SrcAlpha OneMinusSrcAlpha
		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			#include "MyShaderDefine.cginc"

			sampler2D _MainTex;
			sampler2D _ClipTex;
			
			//add by jonny
			sampler2D _AlphaTex;
			int _UseMainTexAlpha;
			
			float4 _ClipRange0 = float4(0.0, 0.0, 1.0, 1.0);

			struct appdata_t
			{
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
				half4 color : COLOR;
			};

			struct v2f
			{
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
				float2 clipUV : TEXCOORD1;
				half4 color : COLOR;
			};

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.color = v.color;
				o.texcoord = v.texcoord;
				o.clipUV = (v.vertex.xy * _ClipRange0.zw + _ClipRange0.xy) * 0.5 + float2(0.5, 0.5);
				return o;
			}

			half4 frag (v2f IN) : COLOR
			{
				half4 col = tex2D(_MainTex, IN.texcoord);
				
				//add by jonny
				if(_UseMainTexAlpha == 0)	
				{
					col.a = tex2D(_AlphaTex, IN.texcoord).r;
				}
				
				if(IN.color.r < 0.001)
			    {
			    	float grey = dot(col.rgb, float3(0.299, 0.587, 0.114));
			    	col.rgb = float3(grey, grey, grey);
			    }
			    else
			    {
					col = col * IN.color;
				}
				col.a *= (1-tex2D(_ClipTex, IN.clipUV).a);
				return col;
			}
			ENDCG
		}
	}
	Fallback "Unlit/Transparent Colored"
}
