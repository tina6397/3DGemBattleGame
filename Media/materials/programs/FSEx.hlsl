sampler heightMap: register(s1);		//Sampler containing the height map	used to compute the normals
//Textures applied to the terrain
sampler grassTexture: register(s2);		
sampler rockTexture: register(s3);
sampler dirtTexture: register(s4);

float4 l;			//Light direction (not necessarily normalized)
float4 invTexSizes; //one over the width and height of the height map texture (1/widht, 1/height, 1 ,0)

// Funciton pre declaration (C style function definition)
float gray(float4 color);
float4 normal(float2 texCoord0, float maxHeight);
float4 colorBySlope(float2 texCoord0, float maxHeight);
float indicator(float min, float max, float t);
float4 phongShading(float4 normal, float4 colour);


float4 main(float2 texCoord0: TEXCOORD0, float maxHeight:TEXCOORD1) : COLOR0
{
	return colorBySlope(texCoord0, maxHeight);
}


// Gets a colour and return a gray scale value
float gray(float4 color)
{
	float4 factors = float4(0.2989, 0.5870, 0.1140, 0.0);
		return dot(factors, color);
}

//Takes the texture coordinate and the max height and ueses them to compute a normal to the terrain
float4 normal(float2 texCoord0, float maxHeight)
{
	//Computes the coordinates of the neighbours of texCoord0
	float2 tc1 = texCoord0 + float2(invTexSizes.x, 0); //(x+e,y)
	float2 tc2 = texCoord0 + float2(0, invTexSizes.y); //(x,y+e)
	float2 tc3 = texCoord0 - float2(invTexSizes.x, 0); //(x-e,y)
	float2 tc4 = texCoord0 - float2(0, invTexSizes.y); //(x,y-e)

	//Samples the height map getting the neighbouring texel of the texel pointed by texCoord0
	float4 col1 = maxHeight * tex2D(heightMap, tc1); 
	float4 col2 = maxHeight * tex2D(heightMap, tc2);
	float4 col3 = maxHeight * tex2D(heightMap, tc3);
	float4 col4 = maxHeight * tex2D(heightMap, tc4);

	//Reurn the normal in world space (the main colour is green rather than the usual blue), normalized to have it of lenght one
	return normalize(float4(gray(0.5*(col1 - col3)), 1.0, gray(0.5*(col2 - col4)), 0.0));
}

//This function interpolate the between three values, 
// it goes from 0 to 1 if t is between min and mid 
// and from 1 to 0 if t is between mid and max, 
// it returns 0 in all other cases.
float interp(float min, float mid,float max, float t)
{
	if (t >= min && t <= mid)
		return (t - min) / (mid - min);
	if (t > mid && t <= max)
		return (max - t) / (max - mid);
	return 0;
}

//Computes the colour of the fragment based on the slope of the terrain
float4 colorBySlope(float2 texCoord0, float maxHeight)
{
	float2 tiledTC = 20.0*texCoord0;
	float4 n = normal(texCoord0, maxHeight);

	float4 col = saturate(			//min, mid, max, angle between up and n (dot(n,y) returns the y component of n as both n and y have lenght 1)
							  interp(-1.0, 0.3125, 0.625, n.y) * tex2D(rockTexture, tiledTC) +	// Rock if the normal is almost orthogonal to the up direction y
							  interp(0.3125, 0.625, 1.0, n.y) * tex2D(dirtTexture, tiledTC) +	// Dirt when the normal starts to move toward the up direction y
							  interp(0.625, 1.0, 1.0, n.y) * tex2D(grassTexture, tiledTC)		// Grass when the normal is almost parallel to the up direction y
						  );

	return phongShading(n, col); //Applies the phong shading algorithm
}
// This function computes the shading of the terrain using Phong shading
float4 phongShading(float4 normal, float4 colour)
{
	// The saturate() function takes a value (scalar or vector) and clamps it to have components between 0 and 1
	float4 diffuse = saturate(colour * dot(normal, normalize(l))); // diffuse component = color * dot(normal, light direction)
	float4 ambient = float4(0.15, 0.15, 0.15, 1.0) * colour;	   // ambient component = dim factor * color;
	return saturate(ambient + diffuse);							   // Phong shading is the sum of all the illumination components ambient diffuse (and specular which we are not using here)
}