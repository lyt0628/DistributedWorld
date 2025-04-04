// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "QS/GameLib/DiffuseLambertByVertex"
{
    Properties
    {
        _Diffuse ("Diffuse", Color ) = (1.0, 1.0, 1.0, 1.0) 
        _Color ("Color Hint", Color ) = (1.0, 1.0, 1.0, 1.0) 
    }
    SubShader
    {
        Pass
        {
            Tags {"LightMode" = "ForwardBase"}

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            fixed4 _Diffuse;
            fixed4 _Color;

            struct a2v {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct v2f {
                float4 pos : SV_POSITION;
                fixed3 color : COLOR0;
            };

            v2f vert(a2v v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;
                
                fixed3 worldNormal = normalize(mul(v.normal, (float3x3)unity_WorldToObject));
                fixed3 worldLight = normalize(_WorldSpaceLightPos0.xyz);

                fixed3 diffuse = _LightColor0.rgb * _Diffuse.rgb * saturate(dot(worldNormal, worldLight));


                o.color = ambient + diffuse;
                return o;
            }

            fixed4 frag(v2f i): SV_TARGET
            {
                fixed3 c = i.color;
                c *= _Color.rgb;
                 return fixed4(c, 1.0);
            }
            ENDCG   
        }
    }
    FallBack "Diffuse"
}
