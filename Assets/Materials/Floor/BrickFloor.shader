Shader "Custom/BrickFloor"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BrickSize ("Brick Size", Vector) = (0.2, 0.1, 1.0, 1.0)
        _MortarSize ("Mortar Size", Range(0.01, 0.1)) = 0.05
        _Color ("Color", Color) = (0.2, 0.2, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

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
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float4 _BrickSize;
            float _MortarSize;
            float4 _Color;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;

            fixed4 frag(v2f i) : SV_Target
            {
                float2 brickUV = float2(floor(i.uv.x / _BrickSize.x), floor(i.uv.y / _BrickSize.y));
                float2 brickOffset = brickUV * _BrickSize.xy;
                float2 mortarOffset = float2(_MortarSize, _MortarSize) * 0.5;

                float2 brickUVOffset = i.uv - brickOffset;
                float2 mortarUV = abs(frac(i.uv / _BrickSize.xy - 0.5)) * _BrickSize.xy - mortarOffset;

                float4 brickColor = tex2D(_MainTex, brickUVOffset);
                float4 mortarColor = _Color;

                float brickMask = step(0.0, brickUVOffset.x) * step(brickUVOffset.x, _BrickSize.x) * step(0.0, brickUVOffset.y) * step(brickUVOffset.y, _BrickSize.y);
                float mortarMask = step(0.0, mortarUV.x) * step(mortarUV.x, _MortarSize) * step(0.0, mortarUV.y) * step(mortarUV.y, _MortarSize);

                fixed4 finalColor = lerp(brickColor, mortarColor, mortarMask);
                finalColor *= brickMask;

                return finalColor;
            }
            ENDCG
        }
    }
}
