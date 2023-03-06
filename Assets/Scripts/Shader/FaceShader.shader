Shader "Custom/FaceShader"
{
    Properties
    {
        _SkinTex ("Skin Texture (Texture)", 2D) = "white" {}
        _EyebrowTex ("Eyebrow Texture (Texture)", 2D) = "white" {}
        _EyeTex ("Eye Texture (Texture)", 2D) = "white" {}
        _NoseTex ("Nose Texture (Texture)", 2D) = "white" {}
        _MouthTex ("Mouth Texture (Texture)", 2D) = "white" {}
        _EarTex ("Ear Texture (Texture)", 2D) = "white" {}
        
        _MouthColor ("Mouth Color", Color) = (1,1,1,1)
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
        sampler2D _EyebrowTex;
        sampler2D _EyeTex;
        sampler2D _NoseTex;
        sampler2D _MouthTex;
        sampler2D _EarTex;

        struct Input
        {
            float2 uvTex;
            float2 uv_EyebrowTex;
            float2 uv_EyeTex;
            float2 uv_NoseTex;
            float2 uv_MouthTex;
            float2 uv_EarTex;
        };

        fixed4 _MouthColor;

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
            fixed4 eyebrowTex = tex2D (_EyebrowTex, IN.uv_EyebrowTex);
            fixed4 eyeTex = tex2D (_EyeTex, IN.uv_EyeTex); 
            fixed4 noseTex = tex2D (_NoseTex, IN.uv_NoseTex);
            fixed4 mouthTex = tex2D (_MouthTex, IN.uv_MouthTex);
            fixed4 earTex = tex2D (_EarTex, IN.uv_EarTex);

            fixed4 firstOverlap = overlapTex(skinTex, eyebrowTex, (1, 1, 1, 1));
            fixed4 secondOverlap = overlapTex(firstOverlap, eyeTex, (1, 1, 1, 1));
            fixed4 thirdOverlap = overlapTex(secondOverlap, noseTex, (1, 1, 1, 1));
            fixed4 fourthOverlap = overlapTex(thirdOverlap, mouthTex, _MouthColor);
            fixed4 finalOverlap = overlapTex(fourthOverlap, earTex, (1, 1, 1, 1));
            
            o.Albedo = finalOverlap.rgb;
            o.Alpha = 1;
        }
        ENDCG
    }
    FallBack "Diffuse"
}