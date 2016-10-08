using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    class PlayerModel : CharacterModel
    {
        ModelElement model; // Root for the sub-graph
        ModelElement weels_group;
        ModelElement hull_group;
        ModelElement Guns_group;


        ModelElement hull;
        ModelElement power;
        ModelElement sphere;

        /// <summary>
        /// A contructor that takes the scene manager and calls the load model method
        /// </summary>
        /// <param name="mSceneMgr"></param>
        public PlayerModel(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
            LoadModelElements();
            AssembleModel();

        }

        /// <summary>
        /// A method to load the model 
        /// </summary>
        protected override void LoadModelElements()
        {

            //make elements----------------------------------
            hull = new ModelElement(mSceneMgr, "Main.mesh");
            hull.GameEntity.GetMesh().BuildEdgeList();

            sphere = new ModelElement(mSceneMgr, "Sphere.mesh");
            sphere.GameEntity.GetMesh().BuildEdgeList();


            power = new ModelElement(mSceneMgr, "PowerCells.mesh");

            //make group nodes----------------------------------
            hull_group = new ModelElement(mSceneMgr);
            weels_group = new ModelElement(mSceneMgr);
            Guns_group = new ModelElement(mSceneMgr);
            model = new ModelElement(mSceneMgr);



        }

        /// <summary>
        /// A method to group the elements together
        /// </summary>
        protected override void AssembleModel()
        {   
            model.AddChild(hull_group.GameNode);

            //attach power & hull node to HULL GROUP NODE
            hull_group.AddChild(hull.GameNode);
            hull_group.AddChild(power.GameNode);
            hull_group.AddChild(weels_group.GameNode);
            hull_group.AddChild(Guns_group.GameNode);

            //attach sphere to weels group node
            weels_group.AddChild(sphere.GameNode);

            //attach weels group node & guns group node to hull group node
            gameNode = model.GameNode;

            mSceneMgr.RootSceneNode.AddChild(gameNode);


            //phy
            physObj = new PhysObj(mSceneMgr, 10, "Player", 0.02f, 0.8f, 10f);
            physObj.SceneNode = model.GameNode;
            physObj.Position = gameNode.Position;
            physObj.AddForceToList(new WeightForce(physObj.InvMass));
            physObj.AddForceToList(new FrictionForce(physObj));
            Physics.AddPhysObj(physObj);



        }

        /// <summary>
        /// A method to attach a gun to the player model
        /// </summary>
        /// <param name="gun"></param>
        public void AttachGun(Gun gun)
        {
            if (Guns_group.GameNode.NumChildren() != 0)
            {
                Guns_group.GameNode.RemoveAllChildren();
            }

            Guns_group.AddChild(gun.GameNode);
        }

        /// <summary>
        /// A method to dispose the player model
        /// </summary>
        public override void DisposeModel()
        {

            Physics.RemovePhysObj(physObj);
            physObj = null;



            if (sphere != null)                     // Start removing from the leaves of the sub-graph
            {
                if (sphere.GameNode.Parent != null)
                    sphere.GameNode.Parent.RemoveChild(sphere.GameNode);
                sphere.Dispose();
            }

            if (hull_group != null)
            {
                if (hull_group.GameNode.Parent != null)
                    hull_group.GameNode.Parent.RemoveChild(hull_group.GameNode);
                hull_group.Dispose();
            }

            if (weels_group != null)
            {
                if (weels_group.GameNode.Parent != null)
                    weels_group.GameNode.Parent.RemoveChild(weels_group.GameNode);
                weels_group.Dispose();
            }

            if (model != null)                      // Stop removing with the sub-graph root
            {
                if (model.GameNode.Parent != null)
                    model.GameNode.Parent.RemoveChild(model.GameNode);
                model.Dispose();
            }

        }


    }
}
