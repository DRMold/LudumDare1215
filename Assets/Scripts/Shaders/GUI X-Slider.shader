Shader "Custom/GUI/X-Slider" {
	Properties {
		_Width ("X Width", Range(0,1)) = 1.0
		_DiffuseColor ("Diffuse Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
	}

    SubShader {
    	Pass {
        	Cull Off // turn off triangle culling, alternatives are:
        	// Cull Back (or nothing): cull only back faces 
         	// Cull Front : cull only front faces
 
         CGPROGRAM 
 
         #pragma vertex vert  
         #pragma fragment frag 
         
         uniform float _Width;
         uniform float _DiffuseColor;
         sampler2D _MainTex;
 
         struct vertexInput {
            float4 vertex : POSITION;
            float4 uv : TEXCOORD0;
         };
         struct vertexOutput {
            float4 pos : SV_POSITION;
            float4 uv : TEXCOORD0;
            float4 posInObjectCoords : TEXCOORD1;
            
         };
 
         vertexOutput vert(vertexInput input) 
         {
            vertexOutput output;
 
            output.pos =  mul(UNITY_MATRIX_MVP, input.vertex);
            output.uv = input.uv;
            output.posInObjectCoords = input.vertex; 
 
            return output;
         }
 
         float4 frag(vertexOutput input) : COLOR 
         {
         	if (input.posInObjectCoords.x >= (_Width * 2 - 1) * 4 ) 
            {
               discard; // drop the fragment if y coordinate > 0
            }
         	fixed4 baseColor = tex2D(_MainTex, input.uv);
         	
         	return float4(baseColor);
            
         }
 
         ENDCG  
      }
   }
}