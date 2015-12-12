// Upgrade NOTE: replaced 'glstate.matrix.mvp' with 'UNITY_MATRIX_MVP'

Shader "Custom/CurveWorld" {
	Properties {
//		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Main Texture", 2D) = "blue" {}
		_Offset("Offset (X, Y, Z)", Vector) = (0,0,0,1)
		_Strength("Strength", Float) = 10.0
//		_Glossiness ("Smoothness", Range(0,1)) = 0.5
//		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags {"RenderType" = "Opaque" "Queue" = "Geometry"}
		LOD 100
		
		Pass {
			Name "Base"
			Tags {"LightMode" = "Always"}
			
			CGPROGRAM
			#include "UnityCG.cginc"
			#pragma vertex vert
			#pragma fragment frag
			
			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _Offset;
			float _Strength;
			
			struct a2v {
				float4 vertex : POSITION;
				float4 texcoord : TEXCOORD0;
			};
			
			struct v2f {
				float4 hPos : SV_POSITION;
				float2 coord0 : TEXCOORD0;
			};
			
			v2f vert (a2v v) {
				v2f o;
				float4 vPos = mul(UNITY_MATRIX_MV, v.vertex);
				float d = vPos.z / _Strength;
				vPos.xyz += _Offset.xyz * (d * d);
				o.hPos = mul(UNITY_MATRIX_P, vPos);
				o.coord0 = TRANSFORM_TEX(v.texcoord, _MainTex);
				return o;
			}
			
			fixed4 frag(v2f i) : COLOR {
				fixed3 result = tex2D(_MainTex, i.coord0).rgb;
				return fixed4(result, 1.0);
			}
			ENDCG
		}
	
	//Wave
//		Pass{
//			CULL Off
//			
//			CGPROGRAM
//			#pragma vertex vert
//			#pragma fragment frag
//			#include "UnityCG.cginc"
//			#include "AutoLight.cginc"
//			
//			float4 _Color;
//			sampler2D _MainTex;
//			
//			//float4 _Time
//			
//			//vertx input: position, normal
//			struct appdata {
//				float4 vertex : POSITION;
//				float4 texcoord : TEXCOORD0;
//			};
//			
//			struct v2f {
//				float4 pos : POSITION;
//				float2 uv : TEXCOORD0;
//			};
//			
//			v2f vert (appdata v) {
//				v2f o;
//				
//				float angle= _Time * 50;
//				
//				v.vertex.y = v.texcoord.x * sin(v.vertex.x + angle);
//				v.vertex.y += sin(v.vertex.z / 2 + angle);
//				v.vertex.y *= v.vertex.x * 0.1f;
//				
//				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
//				o.uv = v.texcoord;
//				return o;
//			}
//			
//			float4 frag (v2f i) : COLOR {
//				half4 color = tex2D(_MainTex, i.uv);
//				return color;
//			}
//			
//			ENDCG
//			
//			//SetTexture [_MainTex] {combine texture}
//		}
		
		
		//Def
//		Tags { "RenderType"="Opaque" }
//		LOD 200
//		
//		CGPROGRAM
//		// Physically based Standard lighting model, and enable shadows on all light types
//		#pragma surface surf Standard fullforwardshadows
//
//		// Use shader model 3.0 target, to get nicer looking lighting
//		#pragma target 3.0
//
//		sampler2D _MainTex;
//
//		struct Input {
//			float2 uv_MainTex;
//		};
//
//		half _Glossiness;
//		half _Metallic;
//		fixed4 _Color;
//
//		void surf (Input IN, inout SurfaceOutputStandard o) {
//			// Albedo comes from a texture tinted by color
//			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
//			o.Albedo = c.rgb;
//			// Metallic and smoothness come from slider variables
//			o.Metallic = _Metallic;
//			o.Smoothness = _Glossiness;
//			o.Alpha = c.a;
//		}
//		ENDCG
	} 
	FallBack Off
}
