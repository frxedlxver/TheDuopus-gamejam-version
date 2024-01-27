Shader "Unlit/WaterShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BaseColor ("Base Color", Color) = (1, 1, 1, 0.2)
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
        float _RippleAmount;
//        float _Frequency = 9.0;
//        float _Speed = 1.0;

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
                float rippleStrength = 15;
                float rippleSpeed = 0.2;
                float frequencyScaleX = 5.0;
                float frequencyScaleY = 7.0;

                float timeFactor = _WaterTime * rippleSpeed;
                output.uv = input.uv + cos(input.uv * frequencyScaleX + timeFactor) * rippleStrength;
                output.position = TransformObjectToHClip(input.position.xyz);
                output.rippleOffset = output.uv.y; 
                return output;
            }

            half4 frag(VertexOutput output) : COLOR
            {
                float wave = sin(output.rippleOffset * 1.2) * _RippleAmount;
                float4 baseTex = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, output.uv * 7 + wave);

                return baseTex * _BaseColor;
            }
                
            ENDHLSL
        }
    }
}
