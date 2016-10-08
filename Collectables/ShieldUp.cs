using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    class ShieldUp:PowerUp
    {
        
        ModelElement heart;

        /// <summary>
        /// A contructor to initialise the increase value
        /// </summary>
        /// <param name="mSceneMgr"></param>
        /// <param name="sheild"></param>
        public ShieldUp(SceneManager mSceneMgr, Stat shield)
            : base(mSceneMgr, shield)
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
           heart.GameEntity.SetMaterialName("HeartBlueHMD");
            
           heart.GameNode.Scale(4, 4, 4);
           gameNode = heart.GameNode;
           mSceneMgr.RootSceneNode.AddChild(heart.GameNode);


           physObj = new PhysObj(mSceneMgr, 5, "sheildUp", 0.01f, 0.5f, 0.1f);
           physObj.SceneNode = heart.GameNode ;
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

            heart.GameNode.DetachAllObjects();
            heart.GameNode.Dispose();
        }

    }
}
