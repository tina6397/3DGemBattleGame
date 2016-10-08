struct VS_OUTPUT {					
   float4 pos: POSITION;			
   float2 texCoord0: TEXCOORD0;	
};

float4x4 worldViewProj; 
float maxHeight;		
float t;

VS_OUTPUT main(float4 Pos: POSITION, float4 texCoord0: TEXCOORD0)
{ 
	VS_OUTPUT Out = (VS_OUTPUT)0;
	float w0 = (cos(0.08*Pos.x + t) + sin(0.08*(Pos.x - Pos.z) + t));

	float k = 0.4* maxHeight * w0;
	float4 p = float4(Pos.x, Pos.y + k, Pos.z, Pos.w);
	Out.pos =  mul(worldViewProj, p);

	Out.texCoord0 = texCoord0;	

	return Out;
}