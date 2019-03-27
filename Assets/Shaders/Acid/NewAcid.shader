Shader "Custom/NewAcid"
{
	Properties
	{
		_Color("Acid Bottom", Color) = (1,1,1,1)
		_Color2("Acid Top", Color) = (1,1,1,1)

		_ColorRes("Color res", float) = 5
		_WaveDensityX("Wave density x", float) = 1
		_WaveDensityZ("Wave density z", float) = 1
		_WaveSpeed("Wave speed", float) = 1
		_WaveHeight("Wave height", float) = 1
		_WaveOffset("Wave offset", float) = 0.5


		_EdgeColor("Edge Color", Color) = (1, 1, 1, 1)
		_DepthFactor("Depth Factor", float) = 1.0
		_NoiseTex("Noise Texture", 2D) = "white" {}



	}

		SubShader
	{


		Pass
		{

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

		float _HeightMin;
		float _HeightMax;


		float _ColorRes;


		struct vertexInput
		{
			float4 vertex : POSITION;
			float4 texCoord : TEXCOORD1;
		};

		struct vertexOutput
		{
			float4 pos : SV_POSITION;
			float height : TEXCOORD0;
			float4 screenPos : TEXCOORD1;
		};

		vertexOutput vert(vertexInput input)
		{
			vertexOutput output;

			// convert to world space
			output.pos = UnityObjectToClipPos(input.vertex);

			// apply wave animation
			float noise = tex2Dlod(_NoiseTex, float4(input.texCoord.xy, 0, 0));
			float height = sin(input.vertex.x * _WaveDensityX + noise * 3 + (_Time * _WaveSpeed)) * _WaveHeight + sin(input.vertex.z * _WaveDensityZ + (_Time * _WaveSpeed * _WaveOffset)) * _WaveHeight;

			
			output.height = height;
			output.pos.y += height;

			// compute depth
			output.screenPos = ComputeScreenPos(output.pos);


			return output;
		}

		float4 frag(vertexOutput input) : COLOR
		{
			// apply depth texture
			float4 depthSample = SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, input.screenPos);
			float depth = LinearEyeDepth(depthSample).r;

			// create foamline
			float foamLine = 1 - saturate(_DepthFactor * (depth - input.screenPos.w));

			float h = (_WaveHeight - input.height) / (_WaveHeight - (-_WaveHeight));
			h = floor(h * _ColorRes) / _ColorRes;
			fixed4 hCol = lerp(_Color.rgba, _Color2.rgba, h);

			float4 col = hCol + foamLine * _EdgeColor;

			return col;
		}

		ENDCG
	}
	}
}