Shader "Unlit/WaterShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BaseColor ("Base Color", Color) = (1, 1, 1, 0.1)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 100

        HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

        CBUFFER_START(UnityPerMaterial)
            float4 _BaseColor;
        CBUFFER_END

        TEXTURE2D(_MainTex);
        SAMPLER(sampler_MainTex);

        float _WaterTime;

        struct VertexInput
        {
            float4 position : POSITION;
            float2 uv : TEXCOORD0;
        };

        struct VertexOutput
        {
           float4 position : POSITION;
            float2 uv : TEXCOORD0;
            float rippleOffset : TEXCOORD1; 
        };
        
        ENDHLSL
        
        Pass
        {
            HLSLPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag

            VertexOutput vert(VertexInput input)
            {
                VertexOutput output;
                
                // Add time-based ripple effect to the UV coordinates
                float rippleStrength = 0.1;
                float rippleSpeed = 1.0;

                float timeFactor = _WaterTime * rippleSpeed;
                output.uv = input.uv + float2(sin(timeFactor), cos(timeFactor)) * rippleStrength;
                output.position = TransformObjectToHClip(input.position.xyz);
                output.rippleOffset = output.uv.xy; 
                return output;
            }

            half4 frag(VertexOutput output) : COLOR
            {
                float rippleAmount = 0.2;
                // idk if I like this
//                float wave = sin(output.rippleOffset * 10.0) * rippleAmount;
//                float4 baseTex = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, output.uv + float2(0, wave));
                float4 baseTex = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, output.uv);

                return baseTex * _BaseColor;
            }
                
            ENDHLSL
        }
    }
}
