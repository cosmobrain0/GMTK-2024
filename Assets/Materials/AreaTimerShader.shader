Shader "Custom/AreaTimerShader"
{
    Properties
    {
        _NormalColour ("Normal Colour", Color) = (1,1,1,0.5)
        _TriggeredColour ("Triggered Colour", Color) = (1,1,0,0.5)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _Progress ("Progress", Range(0,1)) = 0.0
        _TriggerTime ("Trigger Time", Float) = 0.0
        _TriggerDuration ("Trigger Duration", Float) = 0.0
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows alpha

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        float _Progress;
        float4 _NormalColour, _TriggeredColour;
        float _TriggerTime, _TriggerDuration;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        float ease(float t)
        {
            t = 1 - t;
            t = 1 - t*t*t*t;
            t = sin(3.1415926 * t);
            return t;
        }

        float smoothstep(float t)
        {
            return t*t*(3-2*t);
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float state = smoothstep(saturate((_Time.y-_TriggerTime)/(_TriggerDuration/10))) * (1.0 - _Progress);
            state = lerp(state, 0, float(_Progress < 0));

            float radius = sqrt(2)/2 * state;
            float distance = length(IN.uv_MainTex-0.5);
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * lerp(_NormalColour, _TriggeredColour, float(distance < radius));
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a * float(distance < 0.5);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
