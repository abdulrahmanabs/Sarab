Shader "Custom/OutlineShader"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (1, 1, 1, 1)
        _OutlineWidth ("Outline Width", Range(0, 0.1)) = 0.005
    }
 
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        Cull Off
        ZWrite Off
        ZTest Always
 
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
 
            struct appdata
            {
                float4 vertex : POSITION;
            };
 
            struct v2f
            {
                float4 vertex : SV_POSITION;
            };
 
            sampler2D _MainTex;
            float4 _OutlineColor;
            float _OutlineWidth;
 
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }
 
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 mainColor = tex2D(_MainTex, i.vertex.xy / i.vertex.w);
                fixed4 outlineColor = _OutlineColor;
                float outlineWidth = _OutlineWidth;
 
                float2 pixelSize = 1.0 / _ScreenParams.xy;
                float2 uvOffset = outlineWidth * pixelSize;
 
                float4 outline = tex2D(_MainTex, i.vertex.xy / i.vertex.w + float2(-uvOffset.x, 0)) +
                                 tex2D(_MainTex, i.vertex.xy / i.vertex.w + float2(uvOffset.x, 0)) +
                                 tex2D(_MainTex, i.vertex.xy / i.vertex.w + float2(0, -uvOffset.y)) +
                                 tex2D(_MainTex, i.vertex.xy / i.vertex.w + float2(0, uvOffset.y));
 
                fixed4 finalColor = outline * outlineColor + mainColor;
                return finalColor;
            }
            ENDCG
        }
    }
}