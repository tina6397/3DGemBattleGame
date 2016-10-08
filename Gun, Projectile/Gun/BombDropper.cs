using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    class BombDropper : Gun

    {

        public BombDropper(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
            maxAmmo = 10;
            ammo = new Stat() ;
            ammo.InitValue(maxAmmo);
            LoadModel();
        }

        protected override void LoadModel(){


            base.LoadModel();
            gameNode = mSceneMgr.CreateSceneNode();
            gameEntity = mSceneMgr.CreateEntity("BombDropperGun.mesh");
            gameEntity.SetMaterialName("BombDropper");
            gameNode.AttachObject(gameEntity);


        }

        /// <summary>
        /// A method to fire the bullet
        /// </summary>
        public override void Fire()
        {
            if (ammo.Value != 0)
            {
                projectile = new Bombs(mSceneMgr);
                projectile.SetPosition(GunPosition() + 50 * GunDirection());
                projectile.InitialDirection = GunDirection();
                projectile.initialVelocity = projectile.InitialDirection * 70;
                Game.GetBombsFired.Add(projectile);
                projectile.Move(projectile.initialVelocity);
                ammo.Decrease(1);

            }
        }

        public override void ReloadAmmo()
        {
            if (ammo.Value < maxAmmo)
            {
                ammo.Increase(10);
                if (ammo.Value > maxAmmo)
                {
                    ammo.Reset();
                }
            }
        }

    }
}
