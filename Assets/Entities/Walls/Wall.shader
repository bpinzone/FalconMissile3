Shader "Unlit/Wall" {
    Properties {
        [NoScaleOffset] _MainTex ("Texture", 2D) = "white" {}
        _GridScale ("Grid Scale", float) = 0.25
        _TileOffsetX ("Tile Offset X", float) = 0
        _TileOffsetY ("Tile Offset Y", float) = 0.5
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct VertexInput {
                float4 vertex_model_position : POSITION;
                // float2 uv : TEXCOORD0;
            };

            struct VertexOutput {
                float4 vertex_clip_position : SV_POSITION;
                float3 vertex_world_position : POSITION1;
                // float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float _GridScale;
            float _TileOffsetX;
            float _TileOffsetY;
            float4 _MainTex_ST;

            VertexOutput vert (VertexInput vi) {
                VertexOutput vo;
                vo.vertex_clip_position = UnityObjectToClipPos(vi.vertex_model_position);
                vo.vertex_world_position = mul(unity_ObjectToWorld, vi.vertex_model_position);
                return vo;
            }

            fixed4 frag (VertexOutput vo) : SV_Target {

                fixed4 color = tex2D(_MainTex, (vo.vertex_world_position.xy + float2(_TileOffsetX, _TileOffsetY)) * _GridScale);
                return color;
            }
            ENDCG
        }
    }
}
