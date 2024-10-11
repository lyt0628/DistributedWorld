Shader "GameLib/SimpleShader"
{
    Properties
    {
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag
            
            void vert(in float4 vertex : POSITION,
                        out float4 position : SV_POSITION)
            {
                            position = UnityObjectToClipPos(vertex);
            }

            fixed4 frag(in float4 vertex : SV_POSITION): SV_TARGET
            {
                 return fixed4(1, 0, 0, 1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
