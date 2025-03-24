Shader "QS/GameLib/Volumn/MeshCloud"
{

    Properties
    {
        // 基本色纹理
        _BaseColor ("Texture", 2D) = "white" {}
        _ColorHint ("Color Hint", Color) = (1, 1, 1, 1)
        _Density ("Density", Range(0, 1)) = 0.5
        _AlphaFactor ("Alpha Factor", Range(0, 3)) = 1
        _AlphaPlus ("Alpha Plus", Range(-1, 1)) = 0
        _NoiseScale ("Noise Scale", Float) = 1.0
        _NoiseStrength ("Noise Strength", Float) = 1.0
        _EdgeSoftness ("Edge Softness", Range(0, 1)) = 0.5
        _Gloss ("Gloss", Range(0, 1)) = 0.5
        _SpecularColor ("Specular Color", Color) = (1, 1, 1, 1)

    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 200

        CGINCLUDE

             #include "UnityCG.cginc"
                #include "Lighting.cginc"

                sampler2D _BaseColor;
                float4 _BaseColor_ST;
                float4 _ColorHint;
                float _Density;
                float _AlphaFactor;
                float _AlphaPlus;
                float _NoiseScale;
                float _NoiseStrength;
                float _EdgeSoftness;
                float _Gloss;
                float4 _SpecularColor;

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                    float3 normal : NORMAL;
                    UNITY_VERTEX_INPUT_INSTANCE_ID
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                    float3 pos : TEXCOORD1;
                    float3 worldPos : TEXCOORD2;
                    fixed3 worldNormal : TEXCOORD3;
                    UNITY_VERTEX_INPUT_INSTANCE_ID
                };


                // 噪声函数, 简单的伪随机数
                float noise(float3 p)
                {
                    return frac(sin(dot(p, float3(12.9898, 78.233, 45.5432))) * 43758.5453);
                }

                            // 伪随机梯度向量生成函数
                float3 random3(float3 p) {
                    return frac(sin(float3(
                        dot(p, float3(127.1, 311.7, 74.7)),
                        dot(p, float3(269.5, 183.3, 246.1)),
                        dot(p, float3(113.5, 271.9, 124.6))
                    )) * 43758.5453);
                }

                // 3D Perlin 噪声
                float perlinNoise3D(float3 p) {
                    float3 i = floor(p);
                    float3 f = frac(p);

                    // 八个角点的梯度向量
                    float3 grad000 = normalize(random3(i + float3(0, 0, 0)) * 2 - 1);
                    float3 grad001 = normalize(random3(i + float3(0, 0, 1)) * 2 - 1);
                    float3 grad010 = normalize(random3(i + float3(0, 1, 0)) * 2 - 1);
                    float3 grad011 = normalize(random3(i + float3(0, 1, 1)) * 2 - 1);
                    float3 grad100 = normalize(random3(i + float3(1, 0, 0)) * 2 - 1);
                    float3 grad101 = normalize(random3(i + float3(1, 0, 1)) * 2 - 1);
                    float3 grad110 = normalize(random3(i + float3(1, 1, 0)) * 2 - 1);
                    float3 grad111 = normalize(random3(i + float3(1, 1, 1)) * 2 - 1);

                    // 八个角点到当前点的向量
                    float3 d000 = f - float3(0, 0, 0);
                    float3 d001 = f - float3(0, 0, 1);
                    float3 d010 = f - float3(0, 1, 0);
                    float3 d011 = f - float3(0, 1, 1);
                    float3 d100 = f - float3(1, 0, 0);
                    float3 d101 = f - float3(1, 0, 1);
                    float3 d110 = f - float3(1, 1, 0);
                    float3 d111 = f - float3(1, 1, 1);

                    // 计算点积
                    float dot000 = dot(grad000, d000);
                    float dot001 = dot(grad001, d001);
                    float dot010 = dot(grad010, d010);
                    float dot011 = dot(grad011, d011);
                    float dot100 = dot(grad100, d100);
                    float dot101 = dot(grad101, d101);
                    float dot110 = dot(grad110, d110);
                    float dot111 = dot(grad111, d111);

                    // 平滑插值
                    float3 u = f * f * (3 - 2 * f);
                    float x00 = lerp(dot000, dot100, u.x);
                    float x01 = lerp(dot001, dot101, u.x);
                    float x10 = lerp(dot010, dot110, u.x);
                    float x11 = lerp(dot011, dot111, u.x);
                    float y0 = lerp(x00, x10, u.y);
                    float y1 = lerp(x01, x11, u.y);
                    return lerp(y0, y1, u.z);
                }
        ENDCG

        Pass
        {
            Tags { "LightMode"="ForwardBase" }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Back

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing 
           

            v2f vert(appdata v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v); // 生成 GPU 中的实例ID 到 UNITY_VERTEX_INPUT_INSTANCE_ID 字段
                UNITY_TRANSFER_INSTANCE_ID(v, o);  // 把实例传递到片元着色器

                // 顶点偏移（模拟体积感）
                float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                // 位置作为噪声参数 来偏移顶点
                float noiseValue = perlinNoise3D(v.vertex * _NoiseScale) * _NoiseStrength;
                v.vertex.xyz += v.normal * noiseValue;

                // 变换到裁剪空间
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _BaseColor);
                o.pos = v.vertex;
                o.worldPos = worldPos;
                o.worldNormal = mul(v.normal, (float3x3)unity_WorldToObject);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(i);

                // 获取基本颜色纹理
                fixed4 texColor = tex2D(_BaseColor, i.uv);

                // 密度也用噪声扰动， 密度体现为透明度。
                float density = perlinNoise3D(i.pos * _NoiseScale) * _NoiseStrength;
                density = saturate(density * _Density);

                // 边缘软化
                float edgeAlphaRamp = smoothstep(0, _EdgeSoftness, density);

                // 混合颜色
                fixed4 cloudColor = _ColorHint * texColor;
                cloudColor.a *= saturate(edgeAlphaRamp * _AlphaFactor + _AlphaPlus);


                // 环境光
                fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;

                // 半Lambert 光照
                fixed3 worldNormal = normalize(i.worldNormal);
                fixed3 worldLightDir = normalize(_WorldSpaceLightPos0.xyz);
                fixed3 halfLambert = 0.5 * dot(worldNormal, worldLightDir) + 0.5;

                //Blinn-Phong 高光

                float3 L = worldLightDir;
                float3 V = normalize(_WorldSpaceCameraPos - i.worldPos);
                float3 H = normalize(L + V);
                float specular = pow(max(0, dot(worldNormal, H)), _Gloss * 128);
                float3 specularColor = _SpecularColor.rgb * specular;


                cloudColor.xyz = _LightColor0.rgb * cloudColor.xyz * halfLambert; // 漫反射光与基本颜色调制
                cloudColor.xyz += ambient; // 环境光与基本颜色调制
                cloudColor.xyz += specularColor; // 环境光与高光调制

                return cloudColor;
            }
            ENDCG
        }





        Pass
        {
            Tags { "LightMode"="ForwardAdd" }
            Blend One One // 叠加
            ZWrite Off
            Cull Back

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing 
           

            v2f vert(appdata v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v); // 生成 GPU 中的实例ID 到 UNITY_VERTEX_INPUT_INSTANCE_ID 字段
                UNITY_TRANSFER_INSTANCE_ID(v, o);  // 把实例传递到片元着色器

                // 顶点偏移（模拟体积感）
                float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                // 位置作为噪声参数 来偏移顶点
                float noiseValue = perlinNoise3D(v.vertex * _NoiseScale) * _NoiseStrength;
                v.vertex.xyz += v.normal * noiseValue;

                // 变换到裁剪空间
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _BaseColor);
                o.pos = v.vertex;
                o.worldPos = worldPos;
                o.worldNormal = mul(v.normal, (float3x3)unity_WorldToObject);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(i);

                 // 点光源能量衰减
                float distance = length(_WorldSpaceLightPos0.xyz - i.worldPos);
                float attenuation = 1.0 / (1.0 + 0.1 * distance * distance);

                // 获取基本颜色纹理
                fixed4 texColor = tex2D(_BaseColor, i.uv);

                // 密度也用噪声扰动， 密度体现为透明度。
                float density = perlinNoise3D(i.pos * _NoiseScale) * _NoiseStrength;
                density = saturate(density * _Density);

                // 边缘软化
                float edgeAlphaRamp = smoothstep(0, _EdgeSoftness, density);

                // 混合颜色
                fixed4 cloudColor = _ColorHint * texColor;
                cloudColor.a *= saturate(edgeAlphaRamp * _AlphaFactor + _AlphaPlus);


                // 环境光 环境光不必加两遍
                // fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;

                // 半Lambert 光照
                fixed3 worldNormal = normalize(i.worldNormal);
                fixed3 worldLightDir = normalize(_WorldSpaceLightPos0.xyz);
                fixed3 halfLambert = 0.5 * dot(worldNormal, worldLightDir) + 0.5;

                //Blinn-Phong 高光

                float3 L = normalize(_WorldSpaceLightPos0.xyz - i.worldPos);
                float3 V = normalize(_WorldSpaceCameraPos - i.worldPos);
                float3 H = normalize(L + V);
                float specular = pow(max(0, dot(worldNormal, H)), _Gloss * 128);
                float3 specularColor = _SpecularColor.rgb * specular;

               
                // cloudColor.xyz += ambient; 环境光与基本颜色调制
                cloudColor.xyz = _LightColor0.rgb * cloudColor.xyz * halfLambert * attenuation; // 漫反射光与基本颜色调制
                cloudColor.xyz += specularColor * attenuation; // 环境光与高光调制

               
                return cloudColor;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}