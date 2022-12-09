Shader "Custom/HumanShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _TensionExpansion ("Tension Expansion", Range(-0.1,0.1)) = -0.05
        _RelaxProgression ("Relax Progression", Range(0, 1)) = 0.0
        _MaxVertexDistance ("Max Vertex Distance", Float) = 1.0
        _CenterYOffset ("Center Y Offset", Float) = 0.0
        _EdgeSharpness ("Edge Sharpness", Range(1, 200)) = 100
        _RelaxColor ("RelaxColor", Color) = (0.5,0.5,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows addshadow vertex:vert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float vertexDistance;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        half _TensionExpansion;
        half _RelaxProgression;
        half _MaxVertexDistance;
        half _CenterYOffset;
        half _EdgeSharpness;
        fixed4 _RelaxColor;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)
        
        void vert(inout appdata_full v, out Input o) {
            UNITY_INITIALIZE_OUTPUT(Input, o);
            v.vertex.xyz += (v.color.x * _TensionExpansion * (1 - _Color.g)) * v.normal;
            o.vertexDistance = length(v.vertex.xyz - float3(0,_CenterYOffset,0));
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float relaxBorderOffset = (IN.vertexDistance / _MaxVertexDistance) - _RelaxProgression;
            float relaxAmount = exp(-max(relaxBorderOffset, 0) * _EdgeSharpness);
            fixed4 col = lerp(_Color, _RelaxColor, relaxAmount);
            //fixed4 col = (IN.vertexDistance / _MaxVertexDistance < _RelaxProgression ? fixed4(0.5, 0.5, 1, 1) : _Color);

            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * col;
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
