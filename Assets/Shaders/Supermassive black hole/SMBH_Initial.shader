Shader "KelvinvanHoorn/SMBH"
{
    Properties
    {
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" "RenderPipeline" = "UniversalRenderPipeline" "Queue" = "Transparent" }
        Cull Front

        Pass
        {
            HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareOpaqueTexture.hlsl"                   

            static const float maxFloat = 3.402823466e+38;

			struct Attributes
			{
				float4 posOS	: POSITION;
			};

			struct v2f
			{
				float4 posCS		: SV_POSITION;
				float3 posWS		: TEXCOORD0;

				float3 centre		: TEXCOORD1;
				float3 objectScale	: TEXCOORD2;
			};

            v2f vert(Attributes IN)
			{
				v2f OUT = (v2f)0;

				VertexPositionInputs vertexInput = GetVertexPositionInputs(IN.posOS.xyz);

				OUT.posCS = vertexInput.positionCS;
				OUT.posWS = vertexInput.positionWS;

				// Object information, based upon Unity's shadergraph library functions
				OUT.centre = UNITY_MATRIX_M._m03_m13_m23;
				OUT.objectScale = float3(length(float3(UNITY_MATRIX_M[0].x, UNITY_MATRIX_M[1].x, UNITY_MATRIX_M[2].x)),
                             length(float3(UNITY_MATRIX_M[0].y, UNITY_MATRIX_M[1].y, UNITY_MATRIX_M[2].y)),
                             length(float3(UNITY_MATRIX_M[0].z, UNITY_MATRIX_M[1].z, UNITY_MATRIX_M[2].z)));

				return OUT;
			}
			float2 intersectSphere(float3 rayOrigin, float3 rayDir, float3 centre, float radius) {
    
    float3 offset = rayOrigin - centre;
    const float a = 1;
    float b = 2 * dot(offset, rayDir);
    float c = dot(offset, offset) - radius * radius;
    
    float discriminant = b * b - 4 * a*c;
    // No intersections: discriminant < 0
    // 1 intersection: discriminant == 0
    // 2 intersections: discriminant > 0
    if (discriminant > 0) {
        float s = sqrt(discriminant);
        float dstToSphereNear = max(0, (-b - s) / (2 * a));
        float dstToSphereFar = (-b + s) / (2 * a);
    
        if (dstToSphereFar >= 0) {
            return float2(dstToSphereNear, dstToSphereFar - dstToSphereNear);
        }
    }
    // Ray did not intersect sphere
    return float2(maxFloat, 0);
}

            float4 frag (v2f IN) : SV_Target
{
    // Initial ray information
    float3 rayOrigin = _WorldSpaceCameraPos;
    float3 rayDir = normalize(IN.posWS - _WorldSpaceCameraPos);
        
    return float4(1,0,0,1);
}
            ENDHLSL
        }
    }
}