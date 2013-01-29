Shader "Custom/Decal" {
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Texture to blend", 2D) = "black" {}
	}
	SubShader {
		Tags { "Queue" = "Transparent-1" }
        Pass {
        	Material {
                Diffuse [_Color]
                Ambient [_Color]
            }
            Lighting On
        	ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
            SetTexture [_MainTex] {  Combine texture * primary DOUBLE, texture * primary }
        }
	} 
	FallBack "Diffuse"
}
