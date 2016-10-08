using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    class Bombs : Projectile
    {
        ModelElement bomb;



        public Bombs(SceneManager mSceneMgr)
        {

            this.mSceneMgr = mSceneMgr;
            healthDamage = 5;
            shieldDamage = 5;
            speed = 5;
            initialVelocity = speed * initialDirection;

            Load();
        }

        protected override void Load()
        {
            base.Load();
            bomb = new ModelElement(mSceneMgr, "Bomb.mesh");
            bomb.GameEntity.SetMaterialName("11-Default");
            gameEntity = bomb.GameEntity;
            gameNode = bomb.GameNode;

            mSceneMgr.RootSceneNode.AddChild(gameNode);


            physObj = new PhysObj(mSceneMgr, 10, "Bomb", 0.03f,0.1f, 0.5f);
            physObj.SceneNode = bomb.GameNode;
            physObj.AddForceToList(new WeightForce(physObj.InvMass));

            Physics.AddPhysObj(physObj);


        }


    }
}
