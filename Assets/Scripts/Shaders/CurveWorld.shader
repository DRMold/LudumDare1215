Shader "34/CurvedWorld" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_DiffuseColor ("Diffuse Color", Color) = (1,1,1,1)
		_Intensity ("Intensity", Float) = 0.001
	}
	
	SubShader {
        Tags { "RenderType" = "Opaque" }
        LOD 200
 
        CGPROGRAM
        
        #pragma surface surf Lambert
        #pragma vertex vert addshadow
        
        uniform float _Intensity;
        uniform float4 _DiffuseColor;
 
 
 		
        // UVs
        struct Input {
            float2 uv_MainTex;
        };
 
        // This is where the curvature is applied
        void vert(inout appdata_full input)
        {
            // Transform into world space
            float4 offsetVector = mul( _Object2World, input.vertex );
 
            // Adjust to camera
            offsetVector.xyz -= _WorldSpaceCameraPos.xyz;
 
            // Reduce y by square of z-distance from camera adjusted by intensity
            offsetVector = float4( 0.0f, (offsetVector.z * offsetVector.z) * - _Intensity, 0.0f, 0.0f );
 
            // Reapply to object coordinates
            input.vertex += mul(_World2Object, offsetVector);    
        }
        
        sampler2D _MainTex;
 
        // This is just a default surface shader
        void surf (Input input, inout SurfaceOutput output) {
            output.Albedo = tex2D (_MainTex, input.uv_MainTex).rgb;
            output.Alpha = _DiffuseColor.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}