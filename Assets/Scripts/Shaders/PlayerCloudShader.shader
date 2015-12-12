Shader "34/Player Cloud Shader" {
	Properties {
		_MainTex ("Texture Image", 2D) = "white" {}
			//main texture, white by default
	}
	SubShader {
		Pass {
			
			CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			
			uniform sampler2D _MainTex;
			
			ENDCG
			
		}
	}

}