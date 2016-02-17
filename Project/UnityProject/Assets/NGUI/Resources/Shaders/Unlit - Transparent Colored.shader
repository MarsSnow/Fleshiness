Shader "Unlit/Transparent Colored"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "black" {}
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

			
			//add by jonny
			sampler2D _AlphaTex;
			int _UseMainTexAlpha;
			
			struct appdata_t
			{
				float4 vertex : POSITION;
				half4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : POSITION;
				half4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			v2f o;

			v2f vert (appdata_t v)
			{
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.color = v.color;
				o.texcoord = v.texcoord;
				return o;
			}

			half4 frag (v2f IN) : COLOR
			{
			
				// Sample the texture
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
				return col;
			}
			ENDCG
		}
	}
	
	SubShader
	{
		LOD 100

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
			Fog { Mode Off }
			ColorMask RGB
			Blend SrcAlpha OneMinusSrcAlpha
			ColorMaterial AmbientAndDiffuse
			
			SetTexture [_MainTex]
			{
				Combine Texture * Primary
			}
		}
	}
}
