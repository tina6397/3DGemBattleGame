using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    class CannonBall : Projectile
    {
        ModelElement cannonBall;

        public CannonBall(SceneManager mSceneMgr)
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
            cannonBall = new ModelElement(mSceneMgr, "Bomb.mesh");
            cannonBall.GameEntity.SetMaterialName("11-Default");
            cannonBall.GameNode.Scale(2, 2, 2);
            gameEntity = cannonBall.GameEntity;
            gameNode = cannonBall.GameNode;
            mSceneMgr.RootSceneNode.AddChild(gameNode);


            physObj = new PhysObj(mSceneMgr, 10, "Bomb", 0.03f,0.1f, 0.5f);
            physObj.SceneNode = cannonBall.GameNode;
            physObj.AddForceToList(new WeightForce(physObj.InvMass));

            Physics.AddPhysObj(physObj);


        }


    }
}
