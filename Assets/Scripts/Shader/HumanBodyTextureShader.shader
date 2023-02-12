Shader "Custom/HumanBodyTextureShader"
{
    Properties
    {
        _SkinTex ("Skin Texture (Texture)", 2D) = "white" {}
        _OverlapTex1 ("Overlap Texture - Cloth/Face (Texture)", 2D) = "white" {}
        _OverlapTex2 ("Overlap Texture - Pattern (Texture)", 2D) = "white" {}
        _Overlap1Color ("Cloth Color", Color) = (1,1,1,1)
        _Overlap2Color ("Pattern Color", Color) = (1,1,1,1)
        _Overlap1Blend ("Cloth/Face Visiblity", Range(0, 1)) = 0
        _Overlap2Blend ("Pattern Visiblity", Range(0, 1)) = 0
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 200
        ZWrite Off

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows alpha

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _SkinTex;
        sampler2D _OverlapTex1;
        sampler2D _OverlapTex2;

        float _Overlap1Blend;
        float _Overlap2Blend;

        struct Input
        {
            float2 uv_SkinTex;
            float2 uv_OverlapTex1;
            float2 uv_OverlapTex2;
        };

        fixed4 _Overlap1Color;
        fixed4 _Overlap2Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 skinTex = tex2D (_SkinTex, IN.uv_SkinTex);
            fixed4 overlap1Tex = tex2D (_OverlapTex1, IN.uv_OverlapTex1);
            fixed4 overlap2Tex = tex2D (_OverlapTex2, IN.uv_OverlapTex2); 

            float alpha = (overlap1Tex.a > 0.3)? (overlap1Tex.a * _Overlap1Blend) : 0;

            fixed4 mainTex = skinTex * (1 - alpha);
            fixed4 combineTex = overlap1Tex * alpha * _Overlap1Color;

            fixed4 c = mainTex + combineTex;

            float alpha1 = (overlap2Tex.a > 0.3)? (overlap2Tex.a * _Overlap2Blend) : 0;

            fixed4 mainTex1 = c * (1 - alpha1);
            fixed4 combineTex1 = overlap2Tex * alpha1 * _Overlap2Color;
            fixed4 output = mainTex1 + combineTex1;
            
            o.Albedo = output.rgb;
            o.Alpha = 1;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
