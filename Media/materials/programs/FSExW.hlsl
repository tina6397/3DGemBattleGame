sampler colourMap: register(s0);

float4 main(float2 texCoord0: TEXCOORD0) : COLOR0
{
	return tex2D(colourMap, texCoord0);
}