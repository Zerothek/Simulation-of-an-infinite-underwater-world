Shader "Custom/TerrainShader"
{
        Properties{

            _MainTex("Texture", 2D) = "white" {}
            _TexScale("Texture Scale", float) = 1
            _NormalMap("Normal Map", 2D) = "bump" {}

        }

            Subshader{

                Cull Off
                Tags { "RenderType" = "Opaque" }
                LOD 200

                CGPROGRAM
                #pragma surface surf Standard fullforwardshadows
                #pragma target 3.0

                sampler2D _MainTex, _NormalMap;
                float _TexScale;

                struct Input {
                    
                    float3 worldPos;
                    float3 worldNormal; INTERNAL_DATA
                    float2 uv_BumpMap;
                    float2 uv_MainTex;
                };

                void surf(Input IN, inout SurfaceOutputStandard o) {

                    float3 scaledWorldPos = IN.worldPos / _TexScale;
                    float3 pWeight = abs(IN.worldNormal);
                    pWeight /= pWeight.x + pWeight.y + pWeight.z;

                    float3 xP = tex2D(_MainTex, scaledWorldPos.yz) * pWeight.x;
                    float3 yP = tex2D(_MainTex, scaledWorldPos.xz)* pWeight.y;
                    float3 zP = tex2D(_MainTex, scaledWorldPos.xy)* pWeight.z;

                    o.Albedo = xP + yP + zP;
                    //o.Normal = UnpackNormal(tex2D(_NormalMap, IN.uv_MainTex));

                }

                ENDCG
        }

    Fallback "Diffuse"

}
