
Shader "XYZ/ToonShader_Texture"
{
    Properties
    {
        _MainTex ("Mian Texture", 2D) = "white" {}
        _Color("Main Color", Color) = (1,1,1,1)
        _ShadowTex ("Shadow Texture", 2D) = "white" {}
         _ShadowColor("Shadow Color", Color) = (1,1,1,1)
        _ShadowSoftness("Shadow Softness", Range(0, 1)) = 0.5
        _FogColor("Fog Color", Color) = (0.5,0.5,0.5,0.5)
        _FogDistance("Fog Distance", float) = 100
        
   
         
    }
    SubShader
    {
        Tags 
        { 
            "RenderType"="Opaque" 
            "LightMode" = "ForwardBase"
            "PassFlags" = "OnlyDirectional"
        }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog
            #pragma multi_compile_fwdbase
            #include "Lighting.cginc"
            #include "AutoLight.cginc"

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(5)
                float4 pos : SV_POSITION;
                float3 worldNormal : NORMAL;
                SHADOW_COORDS(2)
                float3 worldPos : TEXCOORD3;
              
            };
            
            float _ShadowSoftness;
            float4 _FogColor;
            float _FogDistance;
            sampler2D _MainTex;
            sampler2D _ShadowTex;
            float4 _Color;
            float4  _ShadowColor;
            
   

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
       
                o.uv = v.uv;
                TRANSFER_SHADOW(o)
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
            
                float3 normal = normalize(i.worldNormal);
                float NdotL = dot(_WorldSpaceLightPos0, normal);
                float shadow = SHADOW_ATTENUATION(i);
                float lightIntensity = smoothstep(0, _ShadowSoftness, NdotL * shadow);
                // sample the texture
                // apply fog

                float fog = (-i.worldPos.y / _FogDistance);

                fixed4 colMain = tex2D(_MainTex, i.uv);
                fixed4 colShadow = tex2D(_ShadowTex,i.uv);
                
                float4 finalColor = lerp(colShadow*_ShadowColor, colMain*_Color, lightIntensity);
                finalColor.a = colMain.a;

                UNITY_APPLY_FOG(i.fogCoord, finalColor);
                
                return lerp(finalColor, _FogColor, clamp(fog,0,1));
            }
           
            ENDCG
        }
        UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
    }
}
