sampler heightMap:register(s0);

struct VS_OUTPUT {					//Programmer defined data structure			
   float4 pos: POSITION;			//position value is lost in the rasteriser
   float2 texCoord0: TEXCOORD0;		//Goes through to the fragment shader
   float maxHeight : TEXCOORD1;		//Goes through to the fragment shader
};

float4x4 worldViewProj;				//Transformation matrix
float maxHeight;					// Maximum peaks height
float t;							// time

//This function gets a color and transform it in a gray scale value
float gray(float4 color)
{
	float4 factors = float4(0.2989, 0.5870, 0.1140, 0.0);
	return dot(factors, color);
}

VS_OUTPUT main(float4 Pos: POSITION, float4 texCoord0: TEXCOORD0)
{ 
	VS_OUTPUT Out = (VS_OUTPUT)0;

	float k = abs(sin(0.25*t));												//Animation of the terrain

	float heightDispl = maxHeight * gray(tex2Dlod(heightMap, texCoord0));	//Reads the eight map, transform it in a grayscale value and scales it by maxHeight
	float4 p = float4(Pos.x, Pos.y + heightDispl, Pos.z, Pos.w);			//Displaces the vertex along the y axis
	Out.pos = mul(worldViewProj, p);										//Transform the vertex from local to clip space

	Out.texCoord0 = texCoord0;												//Passes the values through to the fragment shader
	Out.maxHeight = maxHeight;

	return Out;																//This goes thorugh the rasteriser where the values are interpolated and attached to each fragment
}