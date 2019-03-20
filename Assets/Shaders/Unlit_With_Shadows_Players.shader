// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/Unlit_With_Shadows_Players"
{
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_OccludedColor("Occluded Color", Color) = (1,1,1,1)
		_Outline("Outline width", Range(0.0, 0.1)) = .005
		_OccludedEmission("Occluded Emission", Float) = 1
	}
	SubShader{
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
		Tags{ "Queue" = "Geometry" "RenderType" = "Opaque" }
		Pass{
		Tags{ "LightMode" = "ForwardBase" }
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#pragma multi_compile_fwdbase
		#pragma fragmentoption ARB_fog_exp2
		#pragma fragmentoption ARB_precision_hint_fastest

		#include "UnityCG.cginc"
		#include "AutoLight.cginc"

		struct v2f
	{
		float4	pos			: SV_POSITION;
		float2	uv			: TEXCOORD0;
		LIGHTING_COORDS(1,2)
	};
	float4 _MainTex_ST;
	v2f vert(appdata_tan v)
	{
		v2f o;

		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = TRANSFORM_TEX(v.texcoord, _MainTex).xy;
		TRANSFER_VERTEX_TO_FRAGMENT(o);
		return o;
	}
	sampler2D _MainTex;
	fixed4 frag(v2f i) : COLOR
	{
		fixed atten = LIGHT_ATTENUATION(i);	// Light attenuation + shadows.
											//fixed atten = SHADOW_ATTENUATION(i); // Shadows ONLY.
	return tex2D(_MainTex, i.uv) * atten;
	}
		ENDCG
	}
		Pass{
		Tags{ "LightMode" = "ForwardAdd" }
		Blend One One
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile_fwdadd_fullshadows
#pragma fragmentoption ARB_fog_exp2
#pragma fragmentoption ARB_precision_hint_fastest

#include "UnityCG.cginc"
#include "AutoLight.cginc"

		struct v2f
	{
		float4	pos			: SV_POSITION;
		float2	uv			: TEXCOORD0;
		LIGHTING_COORDS(1,2)
	};
	float4 _MainTex_ST;
	v2f vert(appdata_tan v)
	{
		v2f o;

		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = TRANSFORM_TEX(v.texcoord, _MainTex).xy;
		TRANSFER_VERTEX_TO_FRAGMENT(o);
		return o;
	}
	sampler2D _MainTex;
	fixed4 frag(v2f i) : COLOR
	{
		fixed atten = LIGHT_ATTENUATION(i);	// Light attenuation + shadows.
											//fixed atten = SHADOW_ATTENUATION(i); // Shadows ONLY.
	return tex2D(_MainTex, i.uv) * atten;
	}
		ENDCG
	}
	}
		FallBack "VertexLit"
}
