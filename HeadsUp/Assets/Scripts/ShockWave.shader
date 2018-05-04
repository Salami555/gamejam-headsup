Shader "ShockWave/WaveDistort"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
            
            sampler2D _MainTex;
            
            uniform float4 _HitPoints[10];

            fixed4 frag (v2f i) : SV_Target
            {
                
                float2 aspect = _ScreenParams.xy / _ScreenParams.y;
                float totalStrength = 0;
                float2 displacement = float2(0, 0);
                for (int h = 0; h < 10; h++) {
                    float2 origin = _HitPoints[h].xy;
                    float strength = _HitPoints[h].w;
                    float age = _HitPoints[h].z / strength;
                
                    float2 direction = i.uv - origin;
                    
                    float distance = length(direction * aspect) / strength;
                    
                    float waveWidth = 0.001 + _HitPoints[h].z * 0.6;
                    
                    float intensity = max(0, 0.15 - _HitPoints[h].z * 0.1) * strength * 1.5;
                    
                    float x = 1 - (abs(distance - _HitPoints[h].z) / waveWidth);
                    x = max(0, x);
                    x = smoothstep(0, 1, x);
                    
                    x *= intensity * intensity;
                    totalStrength += x;
                    displacement -= normalize(direction) * x;
                }
                totalStrength *= 4;
                    
                float red = tex2D(_MainTex, i.uv + displacement * 1.3).r;
                float green = tex2D(_MainTex, i.uv + displacement * 1).g;
                float blue = tex2D(_MainTex, i.uv + displacement * 0.7).b;
                return float4(red + totalStrength, green + totalStrength, blue + totalStrength, 1);
            }
            ENDCG
        }
    }
}
