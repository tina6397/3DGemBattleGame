using System;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    abstract class PowerUp : Collectable
    {
        protected Stat health;
        protected int increase;


        /// <summary>
        /// A constructor to initialize the health stat
        /// </summary>
        /// <param name="mSceneMgr"></param>
        /// <param name="health"></param>
        protected PowerUp(SceneManager mSceneMgr, Stat health)
        {
            this.mSceneMgr = mSceneMgr;
            this.health = health;
            
        }

        /// <summary>
        /// A virtual method to load the model
        /// </summary>
        protected virtual void LoadModel()
        {

        }

        /// <summary>
        /// A update method that update the collision detection with the player
        /// </summary>
        /// <param name="evt"></param>
        public override void Update(FrameEvent evt)
        {

            if (IsCollidingWith("Player"))
            {
                remove = true;
                health.Increase(increase);

            }
        }


        /// <summary>
        /// This method determine wheter the bomb is colliding with a named object  type
        /// </summary>
        /// <param name="objName">The name of the potential colliding object</param>
        /// <returns>True if a collision with the named object type happens, false otherwhise</returns>
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

        /// <summary>
        /// A method that animated the power up
        /// </summary>
        /// <param name="evt"></param>
        public override void Animate(FrameEvent evt)
        {
            gameNode.Yaw(Mogre.Math.AngleUnitsToRadians(20) * evt.timeSinceLastFrame);
        }
    }
}
