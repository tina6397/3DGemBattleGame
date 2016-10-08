using System;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    /// <summary>
    /// This class implements the ground of the environment
    /// </summary>
    class Water
    {
        SceneManager mSceneMgr;

        Entity groundEntity;
        SceneNode groundNode;

        int x;
        int z;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mSceneMgr">A reference of the scene manager</param>
        public Water(SceneManager mSceneMgr, int x, int z )
        {
            this.mSceneMgr = mSceneMgr;
            this.x = x;
            this.z = z;
            CreateGround();

        }

        /// <summary>
        /// This method initializes the ground mesh and node
        /// </summary>
        private void CreateGround()
        {
            GroundPlane();
            groundNode = mSceneMgr.CreateSceneNode();
            groundNode.AttachObject(groundEntity);
            groundNode.SetPosition(x, 0, z);
            mSceneMgr.RootSceneNode.AddChild(groundNode);
        
        }

        /// <summary>
        /// This method generate a plane in an Entity which will be used as ground plane
        /// </summary>
        private void GroundPlane()
        {
            Plane plane;

            //--- Set Vertex Shader Parameters 
            string materialName = "VFShaderExampleW"; 
            float maxHeight = 10.0f; 
            MaterialPtr mat = MaterialManager.Singleton.GetByName(materialName); 
            Pass pass = mat.GetTechnique(0).GetPass(0); 
            GpuProgramParametersSharedPtr vertShaderParam = pass.GetVertexProgramParameters(); 
            vertShaderParam.SetNamedConstant("maxHeight", maxHeight);

            plane = new Plane(Vector3.UNIT_Y, 0); 
            MeshManager.Singleton.CreatePlane("ground",
            ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME, plane,
            1000, 1000, 20, 20, true, 1, 5, 5, Vector3.UNIT_Z);

            groundEntity = mSceneMgr.CreateEntity("ground");
            groundEntity.SetMaterialName("VFShaderExampleW");
            Physics.AddBoundary(plane);

            
        }

        /// <summary>
        /// This method disposes of the scene node and enitity
        /// </summary>
        public void Dispose()
        {
            groundNode.DetachAllObjects();
            groundNode.Parent.RemoveChild(groundNode);
            groundNode.Dispose();
            groundEntity.Dispose();
        }

    }
}
