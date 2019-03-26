Shader "Custom/NewAcid"
{
	Properties
	{
		_Color("Acid Bottom", Color) = (1,1,1,1)
		_Color2("Acid Top", Color) = (1,1,1,1)

		_ColorRes("Color res", float) = 5
		_Noise("Noise", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_WaveDensityX("Wave density x", float) = 1
		_WaveDensityZ("Wave density z", float) = 1
		_WaveSpeed("Wave speed", float) = 1
		_WaveHeight("Wave height", float) = 1
		_WaveOffset("Wave offset", float) = 0.5


		_EdgeColor("Edge Color", Color) = (1, 1, 1, 1)
		_DepthFactor("Depth Factor", float) = 1.0
		_WaveSpeed("Wave Speed", float) = 1.0
		_WaveAmp("Wave Amp", float) = 0.2
		_NoiseTex("Noise Texture", 2D) = "white" {}



	}

		SubShader
	{


		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#include "UnityCG.cginc"

			#pragma vertex vert
			#pragma fragment frag

		// Properties
		float4 _Color;
		float4 _Color2;
		float4 _EdgeColor;
		float  _DepthFactor;
		float  _WaveSpeed;
		float  _WaveAmp;
		float _ExtraHeight;
		sampler2D _CameraDepthTexture;
		sampler2D _DepthRampTex;
		sampler2D _NoiseTex;
		float _WaveHeight;
		float _WaveDensityX;
		float _WaveDensityZ;
		float _WaveOffset;


		float _ColorRes;


		struct vertexInput
		{
			float4 vertex : POSITION;
			float4 texCoord : TEXCOORD1;
		};

		struct vertexOutput
		{
			float4 pos : SV_POSITION;
			float4 texCoord : TEXCOORD0;
			float4 screenPos : TEXCOORD1;
		};

		vertexOutput vert(vertexInput input)
		{
			vertexOutput output;

			// convert to world space
			output.pos = UnityObjectToClipPos(input.vertex);

			// apply wave animation
			float noiseSample = tex2Dlod(_NoiseTex, float4(input.texCoord.xy, 0, 0));
			output.pos.y += sin(output.pos.x * _WaveDensityX + (_Time * _WaveSpeed)) * _WaveHeight + sin(output.pos.z * _WaveDensityZ + (_Time * _WaveSpeed * _WaveOffset)) * _WaveHeight;

			// compute depth
			output.screenPos = ComputeScreenPos(output.pos);

			// texture coordinates 
			output.texCoord = input.texCoord;

			return output;
		}

		float4 frag(vertexOutput input) : COLOR
		{
			// apply depth texture
			float4 depthSample = SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, input.screenPos);
			float depth = LinearEyeDepth(depthSample).r;

			// create foamline
			float foamLine = 1 - saturate(_DepthFactor * (depth - input.screenPos.w));

			float heightGradient = ((input.pos.y / _WaveHeight + 1)*0.5);
			heightGradient = floor(heightGradient * _ColorRes);

			//for extrusion based color
			//fixed4 bubbles = -step(tex2D(_BubbleTex, uv), lerp(0,1,sin(_Time.x * _BubbleDissolveSpeed) +1.5)) + _BubbleColor;
			fixed4 c = /*bubbles +*/ lerp(_Color, _Color2, heightGradient);


			float4 col = c /*+ foamLine * _EdgeColor*/;
			return col;
		}

		ENDCG
	}
	}
}