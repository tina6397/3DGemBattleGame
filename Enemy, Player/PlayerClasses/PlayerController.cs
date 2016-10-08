using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;

namespace RaceGame
{
    class PlayerController:CharacterController
    {

        /// <summary>
        /// A contructor that takes a player object and initiate the speed 
        /// </summary>
        /// <param name="player"></param>
        public PlayerController(Character player)
        {
            character = player;
            speed = 200.0f;
        }

        
        /// <summary>
        /// A update method that update the movement, mouse and shooting control
        /// </summary>
        /// <param name="evt"></param>
        public override void Update(FrameEvent evt)
        {
           MovementsControl(evt);
            
            MouseControls();

            ShootingControls();
        }

        /// <summary>
        /// A method to read the movement control from the input manager
        /// </summary>
        /// <param name="evt"></param>
        private void MovementsControl(FrameEvent evt)
        {
            Vector3 move = Vector3.ZERO;

            if (forward)
                move += character.Model.Forward;

            if (backward)
                move -= character.Model.Forward;


            if (left)
                move += character.Model.Left;


            if (right)
                move -= character.Model.Left;

            move = evt.timeSinceLastFrame * move.NormalisedCopy * speed;

            if (accellerate)
                move = 2.0f * move;
            character.Move(move);
        }

        /// <summary>
        /// A method for the mouse control
        /// </summary>
        private void MouseControls()
        {
            character.Model.GameNode.Yaw(Mogre.Math.AngleUnitsToRadians(angles.y));
        }

        /// <summary>
        /// A methdo for the shooting control
        /// </summary>
        private void ShootingControls()
        {
            if (shoot)
            {
                character.Shoot();

            }
            shoot = false;
        }
        
        protected bool changeGun;               
        /// <summary>
        /// Write only. This property allows to set whether the character is to shoot
        /// </summary>
        public bool ChangeGun
        {
            set { changeGun = value; }
            get { return changeGun; }
        }
       

    }
}
