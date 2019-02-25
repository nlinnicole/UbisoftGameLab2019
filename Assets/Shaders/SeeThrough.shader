Shader "Custom/SeeThrough"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
		_OccludedColor("Occluded Color", Color) = (1,1,1,1)
    }
    SubShader
    {
	   Pass
		{
			Tags { "Queue" = "Geometry+1" }
			ZTest Greater
			ZWrite Off

			CGPROGRAM
			#pragma vertex vert            
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest

			half4 _OccludedColor;

			float4 vert(float4 pos : POSITION) : SV_POSITION
			{
				float4 viewPos = UnityObjectToClipPos(pos);
				return viewPos;
			}

				half4 frag(float4 pos : SV_POSITION) : COLOR
			{
				return _OccludedColor;
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
