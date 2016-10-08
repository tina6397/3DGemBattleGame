using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    /// <summary>
    /// This class is the robot class
    /// </summary>
    class Robot : Character

    {
        RobotModel robotModel;
        public RobotModel RobotModel
        {
            get { return robotModel; }
            set { robotModel = value; }
        }

        Player player;
        Boolean isClever;

        /// <summary>
        /// Contructor that takes in the player object and a boolean which stated if the robot have AI
        /// </summary>
        /// <param name="mSceneMgr"></param>
        /// <param name="player"></param>
        /// <param name="isClever"></param>
        public Robot(SceneManager mSceneMgr, Player player, Boolean isClever)
        {
            robotModel = new RobotModel(mSceneMgr, player, isClever);
            model = robotModel;
            this.isClever = isClever;
            this.player = player;
        }

        /// <summary>
        /// Update method that check if it is being shooted, and animate the robot
        /// </summary>
        /// <param name="evt"></param>
        public override void Update(FrameEvent evt)
        {


            if (robotModel != null)
            {

                robotModel.AnimateRobot(evt);

                robotModel.RemoveMe = IsCollidingWith("Bomb");
                if (robotModel.RemoveMe)
                {
                    ((PlayerStats)player.Stats).enemyKilled ++;
                }
            }


        }

        /// <summary>
        /// This method check if this object is colliding the object input
        /// </summary>
        /// <param name="objName"></param>
        /// <returns></returns>
        private bool IsCollidingWith(string objName)
        {
            bool isColliding = false;
            foreach (Contacts c in robotModel.PhysObj.CollisionList)
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
