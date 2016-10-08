using System;
using Mogre;

namespace RaceGame
{
    /// <summary>
    /// This abstract class describes the logic and the components of a character in the game
    /// </summary>
    abstract class Character
    {
        protected CharacterController controller;       // This field is to contains an instace of the CharacterController
        /// <summary>
        /// Read/Write. This property allows to set and read the character controller.
        /// </summary>
        public CharacterController Controller
        {
            get { return controller; }
            set { controller = value; }
        }

        protected CharacterStats stats;                 // This field is to contain an instance of the CharacterStats
        /// <summary>
        /// Read/Write. This property allows to set and read the character statistics.
        /// </summary>
        public CharacterStats Stats
        {
            set { stats = value; }
            get { return stats; }
        }

        protected CharacterModel model;                 // This field is to contain an instance of the CharacterModel
        /// <summary>
        /// Read/Write. This property allows to set and read the character model.
        /// </summary>
        public CharacterModel Model
        {
            set { model = value; }
            get { return model; }
        }

        protected bool isDead = false;                  // This field is to determine wheter the caracter is dead
        /// <summary>
        /// Read/Write. This property allows to set and read whether the character is dead.
        /// </summary>
        public bool IsDead
        {
            get { return isDead; }
            set { isDead = value; }
        }

        /// <summary>
        /// Read/Write. This property allows to set and read the position of the character in the scene.
        /// </summary>
        public Vector3 Position 
        { 
            set { model.SetPosition(value); }
            get { return model.GameNode.Position; }
        }

        /// <summary>
        /// This virtual method allows to move the character in the scene.
        /// </summary>
        /// <param name="direction">The direction along which move the character</param>
        virtual public void Move(Vector3 direction) 
        { 
            model.Move(direction); 
        }

        /// <summary>
        /// This virtual method allows to rotate the character in the specified transform space.
        /// </summary>
        /// <param name="quaternion">The quaternion which describes the rotation axis and angle</param>
        /// <param name="transformSpace">The transformation space in which the rotation is to be performed, local space by default</param>
        virtual public void Rotate(Quaternion quaternion, 
                    Node.TransformSpace transformSpace = Node.TransformSpace.TS_LOCAL) 
        {
            model.Rotate(quaternion, transformSpace); 
        }

        /// <summary>
        /// This virtual method is to implement the logic for shooting of the character
        /// </summary>
        virtual public void Shoot() { }
        
        /// <summary>
        /// This method is to update the character state
        /// </summary>
        /// <param name="evt">A frame event which can be used to tune the character update</param>
        virtual public void Update(FrameEvent evt) { }
        
        /// <summary>
        /// This method disposes od the character when it dies
        /// </summary>
        virtual protected void Die()
        {
            model.Dispose();
        }

    }
}
