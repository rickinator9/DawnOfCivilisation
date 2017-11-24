Shader "Custom/HexShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Terrain Texture Array", 2DArray) = "white" {}
		_StripeTex ("Occupation Stripe Texture", 2D) = "black" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows vertex:vert
		// Set target to 3.5 for texture arrays
		#pragma target 3.5

		UNITY_DECLARE_TEX2DARRAY(_MainTex);
		sampler2D _StripeTex;

		struct Input {
			float4 color : COLOR;
			float3 worldPos;
			float4 stripeColor;
			float3 terrain;
		};

		void vert(inout appdata_full v, out Input data) {
			UNITY_INITIALIZE_OUTPUT(Input, data);
			float a = saturate((v.texcoord1.x + v.texcoord1.y + v.texcoord1.z)*100);
			data.stripeColor = float4(v.texcoord1.xyz, a);
			data.terrain = v.texcoord2.xyz;
		}

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_CBUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_CBUFFER_END

		float4 GetTerrainColor(Input IN, int index) {
			float3 uvw = float3(IN.worldPos.xz * 0.02, IN.terrain[index]);
			float4 c = UNITY_SAMPLE_TEX2DARRAY(_MainTex, uvw);
			return c;
		}

		float4 GetStripeColor(float2 position, float4 color, float4 stripeColor) {
			const float pi = 3.14159f; // pi radians is 180 degrees.
			float radians = pi*2; // 45 degrees.
			float sinX = sin(radians);
			float cosX = cos(radians);
			float sinY = sin(radians);
			float2x2 rotationMatrix = float2x2(cosX, -sinX, sinY, cosX);
			float2 pos = mul(position, rotationMatrix);

			float4 vSample = tex2D(_StripeTex, position / 8.0f);
			float4 stripe = stripeColor * vSample.a;
			return lerp(color, stripe, stripe.a);
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed4 c = (GetTerrainColor(IN, 0)+
					   GetTerrainColor(IN, 1)+
					   GetTerrainColor(IN, 2))/3;
			//fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			c = fixed4(lerp(c.rgb, IN.color, IN.color.a), c.a);
			c = GetStripeColor(IN.worldPos.xz, c, IN.stripeColor);
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
