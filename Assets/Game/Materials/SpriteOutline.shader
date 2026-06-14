Shader "Custom/SpriteOutline"
{
    Properties
    {
        _MainTex ("Sprite", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)

        _OutlineColor ("Outline Color", Color) = (0,1,0,1)
        _OutlineSize ("Outline Size", Float) = 1
        _OutlineEnabled ("Outline Enabled", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        Lighting Off
        ZWrite Off

        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                fixed4 color : COLOR;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                fixed4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;

            fixed4 _Color;

            fixed4 _OutlineColor;
            float _OutlineSize;
            float _OutlineEnabled;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.color = v.color;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 sprite = tex2D(_MainTex, i.uv) * i.color * _Color;

                if (_OutlineEnabled < 0.5)
                    return sprite;

                float2 offset = _MainTex_TexelSize.xy * _OutlineSize;

                float alphaLeft  = tex2D(_MainTex, i.uv + float2(-offset.x, 0)).a;
                float alphaRight = tex2D(_MainTex, i.uv + float2(offset.x, 0)).a;
                float alphaUp    = tex2D(_MainTex, i.uv + float2(0, offset.y)).a;
                float alphaDown  = tex2D(_MainTex, i.uv + float2(0, -offset.y)).a;

                float outline =
                    max(alphaLeft,
                    max(alphaRight,
                    max(alphaUp,
                        alphaDown)));

                if (sprite.a <= 0.01 && outline > 0.01)
                    return _OutlineColor;

                return sprite;
            }

            ENDCG
        }
    }
}
