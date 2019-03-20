Shader "Unlit/Rope"
{
    Properties
    {
		_Color("Color", Color) = (1,1,1,1)
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_OccludedColor("Occluded Color", Color) = (1,1,1,1)
		_Outline("Outline width", Range(0.0, 0.1)) = .005
		_OccludedEmission("Occluded Emission", Float) = 1 }
		SubShader
    {
			   Pass
		{
			Tags { "Queue" = "Geometry+1" "RenderType" = "Transparent" }
			ZTest Greater
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert            
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			#include "UnityCG.cginc"

			half4 _OccludedColor;
			half _OccludedEmission;

			struct v2f {
				float4 pos : SV_POSITION;
				fixed4 color : COLOR;
			};


			v2f vert(appdata_base v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.color.xyz = v.normal * 0.5 + 0.5;
				o.color.w = 1.0;
				return o;
			}

			half4 frag(v2f i) : SV_Target
			{
				return _OccludedColor * _OccludedEmission;
			}

			ENDCG
		}
        Pass
        {
				Tags{ "RenderType" = "Opaque" }
					LOD 100
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			half4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                half4 col = _Color;
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return _Color;
            }
            ENDCG
        }
    }
}
