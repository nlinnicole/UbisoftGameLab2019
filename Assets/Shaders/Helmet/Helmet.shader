Shader "Custom/Helmet"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,5)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _InnerFog("InnerFog", Range(0.5, 2.0)) = 0.3
        _Bottom("Bottom", float) = 0
        _Top("Top", float) = 1
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




        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 200

        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows alpha

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float3 viewDir;
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float _InnerFog;
        float _Bottom;
        float _Top;


        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;

            half rimTerm = saturate(dot(normalize(IN.viewDir), o.Normal));
            o.Alpha = smoothstep(_Bottom, _Top, rimTerm * _InnerFog);
            //o.Emission = _EmissionColor * smoothstep(_Bottom, _Top, rimTerm * _EmissionEdge) * _EmissionAmount;
        }
        ENDCG
    } 
	FallBack "Diffuse"

}
