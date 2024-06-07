Shader "Custom/ScreenFadeShader"
{
    Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
	    Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline"}
		ZTest Always ZWrite Off Cull Off

        Pass
        {
            HLSLPROGRAM

			#pragma vertex vert
            #pragma fragment frag

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/SurfaceInput.hlsl"
            
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Amount;

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

            struct v2f
            {
	            float4 vertex : SV_POSITION;
            	float2 uv : TEXCOOORD0;
            };

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = TransformObjectToHClip(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
            float4 frag(v2f i) : SV_Target
            {
                float4 col = tex2D(_MainTex, i.uv);
                col.xyz *= _Amount;
                return col;
            }
            
            ENDHLSL
        }
    }
}
