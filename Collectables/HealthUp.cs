using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    class HealthUp:PowerUp
    {
        
        ModelElement heart;

        /// <summary>
        /// A contructor to initialise the increase value
        /// </summary>
        /// <param name="mSceneMgr"></param>
        /// <param name="health"></param>
        public HealthUp(SceneManager mSceneMgr, Stat health)
            : base(mSceneMgr, health)
        {
            this.mSceneMgr = mSceneMgr;
            increase = 1 ;
            LoadModel() ;
            

        }


        /// <summary>
        /// A method to load the model
        /// </summary>
        protected override void LoadModel()
        {
           // base.LoadModel();
            remove = false;

            heart = new ModelElement(mSceneMgr, "heart.mesh");
            heart.GameEntity.SetMaterialName("HeartHMD");
            
            heart.GameNode.Scale(4, 4, 4);
          // gem.GameEntity.GetMesh().BuildEdgeList();
           gameNode = heart.GameNode;
           mSceneMgr.RootSceneNode.AddChild(heart.GameNode);


           physObj = new PhysObj(mSceneMgr, 5, "healthUp", 0.01f, 0.5f, 0.1f);
           physObj.SceneNode = heart.GameNode ;
           physObj.AddForceToList(new WeightForce(physObj.InvMass));
           //physObj.AddForceToList(new FrictionForce(physObj));

           Physics.AddPhysObj(physObj);
        }


       


        /// <summary>
        /// A method to dispose the gem and remove the physics
        /// </summary>
        public void Dispose()
        {
            Physics.RemovePhysObj(physObj);
            physObj = null;

            heart.GameNode.DetachAllObjects();
            heart.GameNode.Dispose();
        }

    }
}
