Shader "Unlit/Stripes-2"
{
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		[IntRange] _NumColors ("Number of colors", Range(2, 4)) = 2
		_Color1 ("Color 1", Color) = (0,0,0,1)
		_Color2 ("Color 2", Color) = (1,1,1,1)
		_Color3 ("Color 3", Color) = (1,0,1,1)
		_Color4 ("Color 4", Color) = (0,0,1,1)
		_Tiling ("Tiling", Range(1, 500)) = 10
		_WidthShift ("Width Shift", Range(-1, 1)) = 0
		_Direction ("Direction", Range(0, 1)) = 0
		_WarpScale ("Warp Scale", Range(0, 1)) = 0
		_WarpTiling ("Warp Tiling", Range(1, 10)) = 1
	}

	SubShader
	{

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			int _NumColors;
			fixed4 _Color1;
			fixed4 _Color2;
			fixed4 _Color3;
			fixed4 _Color4;
			int _Tiling;
			float _WidthShift;
			float _Direction;
			float _WarpScale;
			float _WarpTiling;

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
			
			float2 rotatePoint(float2 pt, float2 center, float angle) {
				float sinAngle = sin(angle);
				float cosAngle = cos(angle);
				pt -= center;
				float2 r;
				r.x = pt.x * cosAngle - pt.y * sinAngle;
				r.y = pt.x * sinAngle + pt.y * cosAngle;
				r += center;
				return r;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				const float PI = 3.14159;

				float2 pos = rotatePoint(i.uv.xy, float2(0.5, 0.5), _Direction * 2 * PI);
				
				pos.x += sin(pos.y * _WarpTiling * PI * 2) * _WarpScale;
				pos.x *= _Tiling;

				int value = floor((frac(pos.x)) * _NumColors  + _WidthShift);
				value = clamp(value, 0, _NumColors - 1);
				switch (value) {
					case 3: return _Color4;
					case 2: return _Color3;
					case 1: return _Color2;
					default: return _Color1;
				}
			}
			ENDCG
		}
	}
}
