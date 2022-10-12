Shader "Custom/Shader-Triplanar"
{
    
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _VerticalTex ("Vertical", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _TextureScale("Texture Scale: ", Float) = 1.0
        _MixValue("Mix Value:", Range(1,10)) = 1.0
		_WaterHeight("Altura agua:", Range(0,100)) = 1.0
		_SnowHeight("Altura nieve:", Range(0,200)) = 60.0
		
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows
		
        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex, _VerticalTex;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_VerticalTex;
            float3 worldPos;
            float3 worldNormal;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        half _MixValue, _TextureScale;
		half _WaterHeight, _SnowHeight;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
        // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)
			
		
        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c1 = tex2D (_MainTex, IN.worldPos.xz * _TextureScale);
 			fixed4 c1_2 = tex2D (_VerticalTex, IN.worldPos.xz * _TextureScale);
            fixed4 c2 = tex2D (_VerticalTex, IN.worldPos.xy * _TextureScale);
            fixed4 c3 = tex2D (_VerticalTex, IN.worldPos.yz * _TextureScale);

            fixed3 normal = pow(abs(IN.worldNormal), _MixValue);
            float auxNormal  = normal.x + normal.y + normal.z;
            normal = normal / auxNormal;
			
			if(IN.worldPos.y <= _WaterHeight) 
            {
                c1.r -= 0.1;
                c1.g -= 0.1;
                c1.b += 0.5;
            }
			else if(IN.worldPos.y >= _SnowHeight)
            {
                c1.r = 1.0;
				c1.g = 1.0;
				c1.b = 1.0;
            }

			c1 = c1*step(0,IN.worldNormal.y) + c1_2 * step(IN.worldNormal.y,0);

            o.Albedo = c1.rgb * abs(normal.y) + c2.rgb * abs(normal.z) + c3.rgb * abs(normal.x);

			// Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = 1;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
