using System;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    class CollectableGun : Collectable
    {
        Gun gun;
        public Gun Gun
        {
            get { return gun; }
        }

        Armoury playerArmoury;
        public Armoury PlayerArmoury
        {
            set { playerArmoury = value; }
            get { return playerArmoury; }
        }

       
        public CollectableGun(SceneManager mSceneMgr, Gun gun, Armoury playerArmoury)
        {
            // Initialize here the mSceneMgr, the gun and the playerArmoury fields to the values passed as parameters
            this.mSceneMgr = mSceneMgr;
            this.gun = gun;
            this.playerArmoury = playerArmoury;

            gameNode = mSceneMgr.CreateSceneNode();
 
            gameNode.Scale(1.5f, 1.5f, 1.5f);
            gameNode.AddChild(gun.GameNode);

            mSceneMgr.RootSceneNode.AddChild(gameNode);
            

            float radius = 10;
            gameNode.Position += radius * Vector3.UNIT_Y;
            gameNode.Position -= radius * Vector3.UNIT_Y;


            physObj = new PhysObj(mSceneMgr, 10, "collectableGun",0.02f, 0.1f, 0.5f);
            physObj.SceneNode = gameNode;
            physObj.AddForceToList(new WeightForce(physObj.InvMass));

            Physics.AddPhysObj(physObj);
        }

        public override void Update(FrameEvent evt)
        {
            Animate(evt);


            remove = (IsCollidingWith("Player"));
            if (remove)
            {

                (gun.GameNode.Parent).RemoveChild(gun.GameNode.Name);
                
                
                playerArmoury.AddGun(gun);

                Dispose();

            }
            base.Update(evt);

        }

        private bool IsCollidingWith(string objName)
        {
            bool isColliding = false;
            foreach (Contacts c in physObj.CollisionList)
            {
                if (c.colliderObj.ID == objName || c.collidingObj.ID == objName)
                {
                    isColliding = true;
                    break;
                }
            }
            return isColliding;
        }

        public override void Animate(FrameEvent evt)
        {
            gameNode.Rotate(new Quaternion(Mogre.Math.AngleUnitsToRadians(evt.timeSinceLastFrame * 10), Vector3.UNIT_Y));
        }
    }
}
