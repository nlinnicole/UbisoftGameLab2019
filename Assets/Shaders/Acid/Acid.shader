Shader "Custom/Acid"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_Color2("Color2", Color) = (1,1,1,1)
		_ColorRes("Color res", float) = 5
		_BubbleColor("BubbleColor", Color) = (1,1,1,1)
		_BubbleTex("Bubbles", 2D) = "white" {}
		_Noise("Noise", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_WaveDensityX("Wave density x", float) = 1
		_WaveDensityZ("Wave density z", float) = 1
		_WaveSpeed("Wave speed", float) = 1
		_WaveHeight("Wave height", float) = 1
		_WaveOffset("Wave offset", float) = 0.5
		_BubbleSpeed("Bubble Speed", float) = 1
		_BubbleDissolveSpeed("Bubble dissolve speed", float) = 1
	}
		SubShader
		{
			Tags { "RenderType" = "Opaque" }
			LOD 200

			CGPROGRAM
			// Physically based Standard lighting model, and enable shadows on all light types
			#pragma surface surf Standard vertex:vert

			// Use shader model 3.0 target, to get nicer looking lighting
			#pragma target 3.0

			sampler2D _BubbleTex;
			sampler2D _Noise;

			struct Input
			{
				float2 uv_BubbleTex;
				float2 uv_Noise;

				float4 pos;
			};

			half _Glossiness;
			half _Metallic;
			fixed4 _Color;
			fixed4 _Color2;
			fixed4 _Color3;
			fixed4 _BubbleColor;
			float _ColorRes;
			float _WaveDensityX;
			float _WaveDensityZ;
			float _WaveSpeed;
			float _WaveHeight;
			float _WaveOffset;
			float _BubbleSpeed;
			float _BubbleDissolveSpeed;

			void vert(inout appdata_full v, out Input o) {
				UNITY_INITIALIZE_OUTPUT(Input, o);
				v.vertex.y = sin(v.vertex.x * _WaveDensityX + (_Time * _WaveSpeed)) * _WaveHeight + sin(v.vertex.z * _WaveDensityZ + (_Time * _WaveSpeed * _WaveOffset)) * _WaveHeight;
				o.pos = v.vertex;
				o.uv_BubbleTex.xy += _Time * 10;
			}


			float2 FlowUV(float2 uv, float time) {
				return uv + time * _BubbleSpeed;
			}

			// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
			// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
			// #pragma instancing_options assumeuniformscaling
			UNITY_INSTANCING_BUFFER_START(Props)
				// put more per-instance properties here
			UNITY_INSTANCING_BUFFER_END(Props)

			void surf(Input IN, inout SurfaceOutputStandard o)
			{
				IN.pos.y += tex2D(_BubbleTex, _Time)/100;

				float2 uv = FlowUV(IN.uv_BubbleTex, -_Time.y);
				float2 noiseUV = FlowUV(IN.uv_Noise, -_Time.x);

				float heightGradient = ((IN.pos.y/_WaveHeight + 1)*0.5);
				heightGradient = floor(heightGradient * _ColorRes);

				//for extrusion based color
				//fixed4 bubbles = -step(tex2D(_BubbleTex, uv), lerp(0,1,sin(_Time.x * _BubbleDissolveSpeed) +1.5)) + _BubbleColor;
				fixed4 c = /*bubbles +*/ lerp(_Color, _Color2, heightGradient);


				o.Albedo = c.rgb;
				// Metallic and smoothness come from slider variables
				o.Metallic = _Metallic;
				o.Smoothness = _Glossiness;
				o.Alpha = c.a;
			}
			ENDCG
		}
		FallBack "Diffuse"

}