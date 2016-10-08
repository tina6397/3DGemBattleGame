using System;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    abstract class Gem : Collectable
    {
        protected Stat score;
        protected int increase;

        /// <summary>
        /// A contructor that takes the scene manager and the score 
        /// </summary>
        /// <param name="mSceneMgr"></param>
        /// <param name="score"></param>
        protected Gem(SceneManager mSceneMgr, Stat score)
        {
            this.mSceneMgr = mSceneMgr;
            this.score = score;
        }

        /// <summary>
        /// A virtual method for loading the model
        /// </summary>
        protected virtual void LoadModel()
        {}

        /// <summary>
        /// A update method that check the collision detection with the player
        /// </summary>
        /// <param name="evt"></param>
        public override void Update(FrameEvent evt)
        {
            if (IsCollidingWith("Player")){
                remove = true;
                score.Increase(increase);
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
        /// This method animate the gem
        /// </summary>
        /// <param name="evt"></param>
        public override void Animate(FrameEvent evt)
        {
            gameNode.Yaw(Mogre.Math.AngleUnitsToRadians(20) * evt.timeSinceLastFrame);
        }
    }
}
