// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/SeeThrough"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_OccludedColor("Occluded Color", Color) = (1,1,1,1)
		_Outline("Outline width", Range(0.0, 0.1)) = .005
		_OccludedEmission("Occluded Emission", Float) = 1
    }
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


		//non-see through 
		Tags{ "RenderType" = "Opaque" "Queue" = "Geometry+1" }
		LOD 200
		ZTest Less
		Blend SrcAlpha OneMinusSrcAlpha

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows


		#pragma target 3.0


		sampler2D _MainTex;

		struct Input
		{
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		UNITY_INSTANCING_BUFFER_START(Props)
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
    }
    FallBack "Diffuse"
}
