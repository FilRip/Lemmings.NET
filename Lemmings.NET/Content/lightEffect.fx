// Lighting parameters
float4 LightColor;
float3 LightPosition;
float LightRadius;
float LightIntensity;

// Calculates diffuse light with attenuation and normal dot light
float4 CalculateLight(float3 pos, float3 normal)
 {
	float3 lightDir = LightPosition - pos;

	float attenuation = saturate(1.0f - length(lightDir) / LightRadius);
 	lightDir = normalize(lightDir); 

	float NdL = max(0, dot(normal, lightDir));
	float4 diffuseLight = NdL * LightColor * LightIntensity * attenuation;
		
	return float4(diffuseLight.rgb, 1.0f);
}

// Screen size and the inverted view matrix
float2 screenSize;
float4x4 InverseVP;

// Input texture sampler
sampler s0;

// Basic XNA Vertex shader
float4x4 MatrixTransform;
void SpriteVertexShader(inout float4 color    : COLOR0,
	inout float2 texCoord : TEXCOORD0,
	inout float4 position : SV_Position)
{
	position = mul(position, MatrixTransform);
}

float4 PixelShaderFunction(float2 position : SV_POSITION, 
						   float4 color : COLOR0,
						   float2 TexCoordsUV : TEXCOORD0) : COLOR0
{
	// Obtain texture coordinates corresponding to the current pixel on screen
	float2 TexCoords = position.xy / screenSize;
	TexCoords += 0.5f / screenSize;

	// Sample the input texture
	float4 normal = 2.0f * tex2D(s0, TexCoords) - 1.0f;

	// Transform input position to view space
	float3 newPos = float3(position.xy, 0.0f);
	float4 pos = mul(newPos, InverseVP);
	// Calculate the lighting with given normal ans position
	float4 lighting = CalculateLight(pos.xyz, normal.xyz);
	return lighting;
}

technique Technique1
{
	pass Pass1
	{
		VertexShader = compile vs_3_0 SpriteVertexShader();
		PixelShader = compile ps_3_0 PixelShaderFunction();
	}
}