Shader "Custom/Hologram2" {
	Properties{
		_BumpMap("Normal Map", 2D) = "bump"{}
		_HoloColor("Hologram Color", Color) = (1,1,1,1)
		_HoloPower("Hologram Power", Range(0, 10)) = 3
		_LineCount("Hologram Line Count", Range(0, 10)) = 3
		_LineArea("Hologram Line Area", Range(0, 50)) = 30
	}
	SubShader{
		Tags{ "RenderType" = "Transparent" "Queue" = "Transparent" }

		CGPROGRAM

		#pragma surface surf nolight noambinet alpha:fade

		sampler2D _BumpMap;
		float4 _HoloColor;
		float _HoloPower;
		float _LineCount;
		float _LineArea;

		struct Input {
			float2 uv_BumpMap;
			float3 viewDir;
			float3 worldPos;
		};

		void surf(Input IN, inout SurfaceOutput o) {
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
			o.Emission = _HoloColor;
			float rim = saturate(dot(o.Normal, IN.viewDir));
			rim = saturate(pow(1 - rim, _HoloPower)) + pow(frac(IN.worldPos.g * _LineCount - _Time.y), _LineArea) * 0.5;
			o.Alpha = rim;
		}

		float4 Lightingnolight(SurfaceOutput s, float3 lightDir, float atten) {
			return float4(0, 0, 0, s.Alpha);
		}
		ENDCG
	}
		FallBack "Transparent/Diffuse"
}
