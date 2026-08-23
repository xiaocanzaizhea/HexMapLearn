Shader "Custom/GaussianBlur"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlurSize ("Blur Size", Range(0.5, 5.0)) = 1.0
    }

    SubShader
    {
        CGINCLUDE
        
        #include "UnityCG.cginc"

        sampler2D _MainTex;
        float4 _MainTex_TexelSize;
        float _BlurSize;

        struct appdata
        {
            float4 vertex : POSITION;
            float2 uv : TEXCOORD0;
        };

        struct v2f
        {
            float4 vertex : SV_POSITION;
            float2 uv[5] : TEXCOORD0;  // 5个采样点
        };

        // 5x5 高斯核（σ=1.0）
        static const float _Weight[5] = { 0.0545, 0.2442, 0.4026, 0.2442, 0.0545 };

        // ============ 水平模糊 ============
        v2f vert_horizontal(appdata v)
        {
            v2f o;
            o.vertex = UnityObjectToClipPos(v.vertex);
            float2 texel = _MainTex_TexelSize.xy * _BlurSize;

            o.uv[0] = v.uv;
            o.uv[1] = v.uv + float2(texel.x * 1, 0);
            o.uv[2] = v.uv - float2(texel.x * 1, 0);
            o.uv[3] = v.uv + float2(texel.x * 2, 0);
            o.uv[4] = v.uv - float2(texel.x * 2, 0);

            return o;
        }

        fixed4 frag_horizontal(v2f i) : SV_Target
        {
            fixed4 color = fixed4(0, 0, 0, 0);

            color += tex2D(_MainTex, i.uv[0]) * _Weight[0];
            color += tex2D(_MainTex, i.uv[1]) * _Weight[1];
            color += tex2D(_MainTex, i.uv[2]) * _Weight[2];
            color += tex2D(_MainTex, i.uv[3]) * _Weight[3];
            color += tex2D(_MainTex, i.uv[4]) * _Weight[4];

            return color;
        }

        // ============ 垂直模糊 ============
        v2f vert_vertical(appdata v)
        {
            v2f o;
            o.vertex = UnityObjectToClipPos(v.vertex);
            float2 texel = _MainTex_TexelSize.xy * _BlurSize;

            o.uv[0] = v.uv;
            o.uv[1] = v.uv + float2(0, texel.y * 1);
            o.uv[2] = v.uv - float2(0, texel.y * 1);
            o.uv[3] = v.uv + float2(0, texel.y * 2);
            o.uv[4] = v.uv - float2(0, texel.y * 2);

            return o;
        }

        fixed4 frag_vertical(v2f i) : SV_Target
        {
            fixed4 color = fixed4(0, 0, 0, 0);

            color += tex2D(_MainTex, i.uv[0]) * _Weight[0];
            color += tex2D(_MainTex, i.uv[1]) * _Weight[1];
            color += tex2D(_MainTex, i.uv[2]) * _Weight[2];
            color += tex2D(_MainTex, i.uv[3]) * _Weight[3];
            color += tex2D(_MainTex, i.uv[4]) * _Weight[4];

            return color;
        }

        ENDCG

        // ============ Pass 1: 水平 ============
        Pass
        {
            ZTest Always
            Cull Off
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert_horizontal
            #pragma fragment frag_horizontal
            ENDCG
        }

        // ============ Pass 2: 垂直 ============
        Pass
        {
            ZTest Always
            Cull Off
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert_vertical
            #pragma fragment frag_vertical
            ENDCG
        }
    }
}