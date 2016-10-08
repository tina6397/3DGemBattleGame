using System;
using Mogre;

namespace RaceGame
{
    /// <summary>
    /// This abstract class allows to implement a character controller. 
    /// This allows to controll the camera either by keyboard and mouse or by AI
    /// </summary>
    abstract class CharacterController
    {
        protected Character character;      // A reference to the character to controll

        protected Vector3 angles;           // Rotation angles
        /// <summary>
        /// Write only. This property allows to set the rotation angle
        /// </summary>
        public Vector3 Angles
        {
            set { angles = value; }
        }

        protected bool shoot;               // This method determines when the charatcer is to shoot
        /// <summary>
        /// Write only. This property allows to set whether the character is to shoot
        /// </summary>
        public bool Shoot
        {
            set { shoot = value; }
            get { return shoot; }
        }


        protected bool forward;             // This method determines when the charatcer is to move forward
        /// <summary>
        /// Write only. This property allows to set whether the character is to move forward
        /// </summary>
        public bool Forward
        {
            set { forward = value; }
        }

        protected bool backward;           // This method determines when the charatcer is to move backward
        /// <summary>
        /// Write only. This property allows to set whether the character is to move backward
        /// </summary>
        public bool Backward
        {
            set { backward = value; }
        }

        protected bool left;                // This method determines when the charatcer is to move left
        /// <summary>
        /// Write only. This property allows to set whether the character is to move left
        /// </summary>
        public bool Left
        {
            set { left = value; }
        }

        protected bool right;               // This method determines when the charatcer is to move right
        /// <summary>
        /// Write only. This property allows to set whether the character is to move right
        /// </summary>
        public bool Right
        {
            set { right = value; }
        }

        protected bool up;                  // This method determines when the charatcer is to move up
        /// <summary>
        /// Write only. This property allows to set whether the character is to move up
        /// </summary>
        public bool Up
        {
            set { up = value; }
        }

        protected bool down;                // This method determines when the charatcer is to move down
        /// <summary>
        /// Write only. This property allows to set whether the character is to move down
        /// </summary>
        public bool Down
        {
            set { down = value; }
        }

        protected bool accellerate;         // This method determines when the charatcer is to move accellerate
        /// <summary>
        /// Write only. This property allows to set whether the character is to accellerate
        /// </summary>
        public bool Accellerate
        {
            set { accellerate = value; }
        }

        protected float speed;              // This method determines the speed of the character movement
        /// <summary>
        /// Write only. This property allows to set the speed of the character movement
        /// </summary>
        public float Speed
        {
            set { speed = value; }
        }

        /// <summary>
        /// This virtual method updates the state of the character
        /// </summary>
        /// <param name="evt">A frame event that can be used for the update of the character</param>
        virtual public void Update(FrameEvent evt)
        {
        }
    }
}
