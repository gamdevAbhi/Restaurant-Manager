Shader "Custom/HandLegShader"
{
    Properties
    {
        _SkinTex ("Skin Texture (Texture)", 2D) = "white" {}
        _HandLegTex ("Hand Leg Texture (Texture)", 2D) = "white" {}
        _HandLegPatternTex ("Hand Leg Pattern Texture (Texture)", 2D) = "white" {}
        
        _HandLegColor ("Cloth Color", Color) = (1,1,1,1)
        _HandLegPatternColor ("Hand Leg Pattern Color", Color) = (1, 1, 1, 1)

        _HandLegPattern ("Is Hand Leg Pattern Texture Applied", Int) = 0
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
        sampler2D _HandLegTex;
        sampler2D _HandLegPatternTex;

        struct Input
        {
            float2 uvTex;
            float2 uv_HandLegTex;
            float2 uv_HandLegPatternTex;
        };

        fixed4 _HandLegColor;
        fixed4 _HandLegPatternColor;

        int _HandLegPattern;

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
            fixed4 handLegTex = tex2D (_HandLegTex, IN.uv_HandLegTex);
            fixed4 handLegPatternTex = tex2D (_HandLegPatternTex, IN.uv_HandLegPatternTex); 

            fixed4 firstOverlap = overlapTex(skinTex, handLegTex, _HandLegColor);
            fixed4 finalOverlap = firstOverlap;
            
            if(_HandLegPattern == 1) finalOverlap = overlapTex(firstOverlap, handLegPatternTex, _HandLegPatternColor);
            
            o.Albedo = finalOverlap.rgb;
            o.Alpha = 1;
        }
        ENDCG
    }
    FallBack "Diffuse"
}