Shader "Custom/ClothShader"
{
    Properties
    {
        _SkinTex ("Skin Texture (Texture)", 2D) = "white" {}
        _ClothTex ("Cloth Texture (Texture)", 2D) = "white" {}
        _ClothTypeTex ("Cloth Type Texture (Texture)", 2D) = "white" {}
        _ClothPatternTex ("Cloth Pattern Texture (Texture)", 2D) = "white" {}
        
        _ClothColor ("Cloth Color", Color) = (1,1,1,1)
        _ClothTypeColor ("Cloth Type Color", Color) = (1, 1, 1, 1)

        _ClothType ("Is Cloth Type Texture Applied", Int) = 0
        _ClothPattern ("Is Cloth Pattern Texture Applied", Int) = 0
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 200
        ZWrite Off

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows alpha
        #pragma target 3.5

        sampler2D _SkinTex;
        sampler2D _ClothTex;
        sampler2D _ClothTypeTex;
        sampler2D _ClothPatternTex;

        struct Input
        {
            float2 uvTex;
            float2 uv_ClothTex;
            float2 uv_ClothTypeTex;
            float2 uv_ClothPatternTex;
        };

        fixed4 _ClothColor;
        fixed4 _ClothTypeColor;

        int _ClothType;
        int _ClothPattern;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        fixed4 overlapTex(fixed4 previousTex, fixed4 newTex, fixed4 newTexColor)
        {
            float alpha = (newTex.a > 0.3)? (newTex.a * 1) : 0;

            fixed4 mainTex = previousTex * (1 - alpha);
            fixed4 overlapTex = newTex * alpha * newTexColor;

            return mainTex + overlapTex;
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 skinTex = tex2D (_SkinTex, IN.uvTex);
            fixed4 clothTex = tex2D (_ClothTex, IN.uv_ClothTex);
            fixed4 clothTypeTex = tex2D (_ClothTypeTex, IN.uv_ClothTypeTex); 
            fixed4 clothPatternTex = tex2D (_ClothPatternTex, IN.uv_ClothPatternTex);

            fixed4 firstOverlap = overlapTex(skinTex, clothTex, _ClothColor);
            fixed4 secondOverlap = firstOverlap;
            fixed4 finalOverlap = finalOverlap;

            if(_ClothType == 1) secondOverlap = overlapTex(firstOverlap, clothTypeTex, _ClothTypeColor);
            if(_ClothPattern == 1) finalOverlap = overlapTex(secondOverlap, clothPatternTex, (1, 1, 1, 1));

            o.Albedo = finalOverlap.rgb;
            o.Alpha = 1;
        }
        ENDCG
    }
    FallBack "Diffuse"
}