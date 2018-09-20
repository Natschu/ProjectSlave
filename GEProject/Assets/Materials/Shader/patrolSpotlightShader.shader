Shader "Custom/Patrol Spotlight on Ground"
{
    Properties
    {
        _Color ("Color", Color) = (1, 1, 1, 1)
        _MainTex ("Albedo (RGB)", 2D) = "white" { }
        [HDR] _Color2 ("Secondary Color", Color) = (1, 1, 1, 1)
        _SecondTex ("Secondary (RGB)", 2D) = "white" { }
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
        _Radius ("Radius", Range(0, 10)) = 0
		_Position("[Use Script] Position", Vector) = (0,0,0,0)     
    }
    
    SubShader
    {
        Tags { "RenderType" = "Opaque"}
        LOD 200
        
        CGPROGRAM
        
        #pragma surface surf Standard fullforwardshadows
        
        #pragma target 3.0
        
        //From Script
        float3 _Position;
		
        //From Properties
        sampler2D _MainTex, _SecondTex;
        float4 _Color, _Color2;
        half _Glossiness;
        half _Metallic;
        float _Radius;
        
        struct Input
        {
            float2 uv_MainTex: TEXCOORD0;
            float3 worldPos;				//built-in value -> world space position
            float3 worldNormal;				//built-in value -> world normal
        };
        
        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            //multiply Texture2D times Color
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            fixed4 c2 = tex2D(_SecondTex, IN.uv_MainTex) * _Color2;
            
            float3 dis = distance(_Position, IN.worldPos);
            float3 sphere = 1 - saturate(dis / _Radius);

            //draw textures
            float3 primaryTex = step(sphere, 0.1) * c.rgb;
            float3 secondaryTex = step(0.1, sphere) * c2.rgb;
            float3 resultTex = primaryTex + secondaryTex;
            o.Albedo = resultTex;
            
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
        
    }
    FallBack "Diffuse"
}