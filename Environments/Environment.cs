using System;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    /// <summary>
    /// This class implements the game environment
    /// </summary>
    class Environment
    {
        SceneManager mSceneMgr;             // This field will contain a reference of the scene manages
        RenderWindow mWindow;               // This field will contain a reference to the rendering window

        Ground ground;                      // This field will contain an istance of the ground object
        Water w1;
        Water w2;
        Water w3;
        Water w4;
        Water w5;
        Water w6;
        Water w7;


        Light light;                        // This field will contain a reference of a light
        PhysObj physObj;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mSceneMgr">A reference to the scene manager</param>
        public Environment(SceneManager mSceneMgr, RenderWindow mWindow)
        {
            this.mSceneMgr = mSceneMgr;
            this.mWindow = mWindow;

            Load();                                 // This method loads  the environment
        }

        /// <summary>
        /// load the environment and boundaries
        /// </summary>
        private void Load()
        {
            SetSky();
            SetFog();
            SetLights();
            SetShadows();

            w1 = new Water(mSceneMgr, 1000,1000);
            w2 = new Water(mSceneMgr, -1000, -1000);

            w3 = new Water(mSceneMgr, 1000, 0);
            w4 = new Water(mSceneMgr, -1000, 0);

            w5 = new Water(mSceneMgr, 0, 1000);
            w6 = new Water(mSceneMgr, 0, -1000);

            w7 = new Water(mSceneMgr, 1000,-1000);
            w7 = new Water(mSceneMgr, -1000, 1000);


            ground = new Ground(mSceneMgr, 0, 0);


            //make boundary

            Plane plane1 = new Plane(Vector3.NEGATIVE_UNIT_Z, -500);
            Physics.AddBoundary(plane1);
            Plane plane2 = new Plane(Vector3.UNIT_Z, -500);
            Physics.AddBoundary(plane2);
            Plane plane3 = new Plane(Vector3.NEGATIVE_UNIT_X, -500);
            Physics.AddBoundary(plane3);
            Plane plane4 = new Plane(Vector3.UNIT_X, -500);
            Physics.AddBoundary(plane4);
        }

        /// <summary>
        /// This method dispose of any object instanciated in this class
        /// </summary>
        public void Dispose()
        {
            ground.Dispose();
        }

        /// <summary>
        /// A method to set the sky
        /// </summary>
        private void SetSky()
        {
            //1.--sky dome
            //mSceneMgr.SetSkyDome(true, "Sky", 1f, 10, 500, true);

            //2---sky plane
            //Plane sky = new Plane(Vector3.NEGATIVE_UNIT_Y, -100);

            //mSceneMgr.SetSkyPlane(true, sky, "Sky", 10, 5, true, 0.5f, 100, 100,
            //    ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME);

            //3---sky box
            mSceneMgr.SetSkyBox(true, "SkyBox", 10, true);
        }

        /// <summary>
        /// A method to set the fog
        /// </summary>
        private void SetFog()
        {

            ColourValue fadeColour = new ColourValue(0.1f, 0.5f, 0.1f);
            //1--- linear fog
            //mSceneMgr.SetFog(FogMode.FOG_LINEAR, fadeColour, 0.1f, 100, 1000);
            //2---exponential fog
            //mSceneMgr.SetFog(FogMode.FOG_EXP, fadeColour, 0.01f);

            //3--- exponential square
            mSceneMgr.SetFog(FogMode.FOG_EXP2, fadeColour, 0.001f);

            //set fade color as background
            mWindow.GetViewport(0).BackgroundColour = fadeColour;
        }

        /// <summary>
        /// A method to set the light
        /// </summary>
        private void SetLights()
        {
            mSceneMgr.AmbientLight = new ColourValue(0.1f, 0.1f, 0.3f);                 // Set the ambient light in the scene

            light = mSceneMgr.CreateLight();                                            // Set an instance of a light;

            light.DiffuseColour = ColourValue.Blue;                                      // Sets the color of the light
            light.Position = new Vector3(0, 100, 0);                                    // Sets the position of the light
            
            //1--all scene affected
            //light.Type = Light.LightTypes.LT_DIRECTIONAL;                               // Sets the light to be a directional Light

            //2--spot light-- limit light in specific area
            //light.Type = Light.LightTypes.LT_SPOTLIGHT;                                 // Sets the light to be a spot light
            //light.SetSpotlightRange(Mogre.Math.PI / 1, Mogre.Math.PI / 3, 0.001f);      // Sets the spot light parametes

            //1--all scene affected

            //light.Direction = Vector3.NEGATIVE_UNIT_Y;                                  // Sets the light direction

            //3--light is stronger in origin, fade when further away
            light.Type = Light.LightTypes.LT_POINT;                                     // Sets the light to be a point light

            //smaller the value--> bigger the effect!
            float range = 1000;                                                         // Sets the light range
            float constantAttenuation = 0;                                              // Sets the constant attenuation of the light [0, 1]
            float linearAttenuation = 0;                                                // Sets the linear attenuation of the light [0, 1]
            float quadraticAttenuation = 0.0001f;                                       // Sets the quadratic  attenuation of the light [0, 1]

            light.SetAttenuation(range, constantAttenuation,
                      linearAttenuation, quadraticAttenuation); // Not applicable to directional ligths
        }

        /// <summary>
        /// A method to set the shadow
        /// </summary>
        private void SetShadows()
        {
            mSceneMgr.ShadowTechnique = ShadowTechnique.SHADOWTYPE_STENCIL_ADDITIVE;
        }

    }
}
