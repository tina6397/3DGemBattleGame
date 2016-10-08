using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    class GreenGem : Gem
    {

        ModelElement gem;

        /// <summary>
        /// contructor  that initialize the increase value of the gem
        /// </summary>
        /// <param name="mSceneMgr"></param>
        /// <param name="score"></param>
        public GreenGem(SceneManager mSceneMgr, Stat score)
            : base(mSceneMgr, score)
        {
            this.mSceneMgr = mSceneMgr;

            increase = 1;
            LoadModel();

        }

        /// <summary>
        /// A method to load the model
        /// </summary>
        protected override void LoadModel()
        {
            remove = false;

            gem = new ModelElement(mSceneMgr, "Gem.mesh");
            gem.GameEntity.SetMaterialName("green");
            gem.GameNode.Scale(3, 3, 3);
            gem.GameEntity.GetMesh().BuildEdgeList();
            gameNode = gem.GameNode;
            mSceneMgr.RootSceneNode.AddChild(gem.GameNode);


            physObj = new PhysObj(mSceneMgr, 5, "Gem", 0.01f, 0.5f, 0.1f);
            physObj.SceneNode = gem.GameNode;
            physObj.AddForceToList(new WeightForce(physObj.InvMass));

            Physics.AddPhysObj(physObj);
        }

        
        /// <summary>
        /// A method to dispose the gem and remove the physics
        /// </summary>
        public void Dispose()
        {
            Physics.RemovePhysObj(physObj);
            physObj = null;

            gem.GameNode.DetachAllObjects();
            gem.GameNode.Dispose();
        }

    }
}
