using System;
using Mogre;
using PhysicsEng;
namespace RaceGame
{
    class Player : Character
    {
        private int restCounter = 0;

        protected Armoury playerArmoury;
        public Armoury PlayerArmoury
        {
            get { return playerArmoury; }
        }

        public Boolean isDead;
        public Boolean IsDead
        {
            get { return isDead; }
        }


        /// <summary>
        /// This constructor takes in a scene manager 
        /// and initialize the model, controller, stat, and armoury object for the player
        /// </summary>
        /// <param name="mSceneMgr"></param>
        public Player(SceneManager mSceneMgr)
        {

            model = new PlayerModel(mSceneMgr);

            controller = new PlayerController(this);

            stats = new PlayerStats();
            
            playerArmoury = new Armoury();
        }

        /// <summary>
        /// This method update the level, controller, armoury and stat of the player
        /// </summary>
        /// <param name="evt"></param>
        public override void Update(FrameEvent evt)
        {

            //Update Level-------------------------------------------
              if (((PlayerStats)Stats).Score.Value < 10)
            {
                stats.CurrentLevel = 1;
            }

            if (((PlayerStats)Stats).Score.Value >= 10 && ((PlayerStats)Stats).Score.Value < 80)
            {
                stats.CurrentLevel = 2;
            }

            if (((PlayerStats)Stats).Score.Value >= 80)
            {
                stats.CurrentLevel = 3;
            }

            //Update Controller -------------------------------------------
            controller.Update(evt);

            //Update Armoury----------------------------------
            if (playerArmoury.gunChanged )
            {
                ((PlayerModel)model).AttachGun(playerArmoury.activeGun);

                playerArmoury.gunChanged = false;


            }

            //Update Collision Detection With Robot & stat----------------------------------
            if ((IsCollidingWith("Robot")))
            {
                restCounter++;

                //this counter slow down the speed of decreasing health when colliding with robot
                if (restCounter == 150)
                {
                    restCounter = 0;
                    if (stats.Shield.Value != 0)
                    {
                        stats.Shield.Decrease(1);
                    }
                    else
                    {
                        if (stats.Health.Value != 0)
                        {
                            stats.Health.Decrease(1);
                        }
                        else
                        {
                            if (stats.Lives.Value != 0)
                            {
                                stats.Lives.Decrease(1);


                            }
                            else
                            {
                                isDead = true;
                            }

                        }
                    }
                }

            }



        }

        /// <summary>
        /// This method fire the gun with shoot is pressed
        /// </summary>
        public override void Shoot()
        {
            if (PlayerArmoury.activeGun != null)
            {
                PlayerArmoury.activeGun.Fire();

            }
        }



        /// <summary>
        /// This method checkes for collision detection
        /// </summary>
        /// <param name="objName"></param>
        /// <returns></returns>
        private bool IsCollidingWith(string objName)
        {
            bool isColliding = false;
            foreach (Contacts c in model.PhysObj.CollisionList)
            {
                if (c.colliderObj.ID == objName || c.collidingObj.ID == objName)
                {
                    isColliding = true;
                    break;
                }
            }
            return isColliding;
        }


    }
}
