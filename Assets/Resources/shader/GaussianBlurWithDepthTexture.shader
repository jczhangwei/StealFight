Shader "Custom/GaussianBlurWithDepthTexture" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
	}
	SubShader {
		CGINCLUDE
		
		#include "UnityCG.cginc"

		sampler2D _MainTex;
		half4 _MainTex_TexelSize;
		
		sampler2D _CameraDepthTexture;

		float _BlurSize;
		float _focus;
		float _depthOfField;
		float _transition;

		struct v2f {
			float4 pos : SV_POSITION;
			half2 depth: TEXCOORD0;
			half2 uv_o: TEXCOORD1;
			half2 uv_depth: TEXCOORD2;
			half2 uv[5]: TEXCOORD3;
		};

		float getBlurDegOfUV(half2 uv) {
			float linearDepth = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, uv));
			float far = 1 / (_ZBufferParams.z + _ZBufferParams.w);
			float near = 1 / _ZBufferParams.w;
			
			linearDepth = abs(linearDepth - _focus);
			linearDepth = saturate((linearDepth - _depthOfField)/(_transition - _depthOfField));
			return linearDepth;
		}
		  
		v2f vertBlurVertical(appdata_img v) {
			v2f o;
			o.pos = UnityObjectToClipPos(v.vertex);
			o.uv_depth = v.texcoord;

			#if UNITY_UV_STARTS_AT_TOP
			if (_MainTex_TexelSize.y < 0)
				o.uv_depth.y = 1 - o.uv_depth.y;
			#endif
			
			half2 uv = v.texcoord;
			
			o.uv_o = v.texcoord;
			o.uv[0] = float2(0.0, 0.0);
			o.uv[1] = float2(0.0, _MainTex_TexelSize.y * 1.0) * _BlurSize;
			o.uv[2] = -float2(0.0, _MainTex_TexelSize.y * 1.0) * _BlurSize;
			o.uv[3] = float2(0.0, _MainTex_TexelSize.y * 2.0) * _BlurSize;
			o.uv[4] = -float2(0.0, _MainTex_TexelSize.y * 2.0) * _BlurSize;
					 
			return o;
		}
		
		v2f vertBlurHorizontal(appdata_img v) {
			v2f o;
			o.pos = UnityObjectToClipPos(v.vertex);
			o.uv_depth = v.texcoord;
			
			#if UNITY_UV_STARTS_AT_TOP
			if (_MainTex_TexelSize.y < 0)
				o.uv_depth.y = 1 - o.uv_depth.y;
			#endif
			
			half2 uv = v.texcoord;
			
			o.uv_o = v.texcoord;			
			o.uv[0] = float2(0.0, 0.0);
			o.uv[1] = float2(_MainTex_TexelSize.x * 1.0, 0.0) * _BlurSize;
			o.uv[2] = -float2(_MainTex_TexelSize.x * 1.0, 0.0) * _BlurSize;
			o.uv[3] = float2(_MainTex_TexelSize.x * 2.0, 0.0) * _BlurSize;
			o.uv[4] = -float2(_MainTex_TexelSize.x * 2.0, 0.0) * _BlurSize;
					 
			return o;
		}
		
		fixed4 fragBlur(v2f i) : SV_Target {
			float linearDepth = getBlurDegOfUV(i.uv_depth);
			float weight[3] = {0.4026, 0.2442, 0.0545};
			
			fixed3 sum = tex2D(_MainTex, i.uv_o).rgb * weight[0];
			
			for (int it = 1; it < 3; it++) {
				half2 simple_uv1 = i.uv_o + i.uv[it*2-1] * -linearDepth;
				half2 simple_uv2 = i.uv_o + i.uv[it*2] * -linearDepth;
				sum += tex2D(_MainTex, simple_uv1 ).rgb * weight[it];
				sum += tex2D(_MainTex, simple_uv2 ).rgb * weight[it];
			}
			
			return fixed4(sum, 1.0);
			// return fixed4(_ZBufferParams.z, _ZBufferParams.z, _ZBufferParams.z, 1.0);
		}
		    
		ENDCG
		
		ZTest Always Cull Off ZWrite Off
		
		Pass {
			NAME "GAUSSIAN_BLUR_VERTICAL"
			
			CGPROGRAM
			  
			#pragma vertex vertBlurVertical  
			#pragma fragment fragBlur
			  
			ENDCG  
		}
		
		Pass {  
			NAME "GAUSSIAN_BLUR_HORIZONTAL"
			
			CGPROGRAM  
			
			#pragma vertex vertBlurHorizontal  
			#pragma fragment fragBlur
			
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
