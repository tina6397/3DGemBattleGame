using System;
using Mogre;

namespace RaceGame
{
    /// <summary>
    /// This class inherits from the MovableElement class and is to determine how the character model is described
    /// </summary>
    abstract class CharacterModel:MovableElement
    { 
        Vector3 left;               // This field contains the left direction for the character model
        /// <summary>
        /// Read only. This property returns the left direction of the character model
        /// </summary>
        public Vector3 Left
        {
            get
            {
                left = gameNode.LocalAxes.GetColumn(0).NormalisedCopy;  // This allows to get the updated direction when this property is called
                return left;
            }
        }

        Vector3 up;                 // This field contains the up direction for the character model
        /// <summary>
        /// Read only. This property returns the up direction of the character model
        /// </summary>
        public Vector3 Up
        {
            get
            {
                up = gameNode.LocalAxes.GetColumn(1).NormalisedCopy;
                return up;
            }
        }

        Vector3 forward;            // This field contains the forward direction for the character model
        /// <summary>
        /// Read only. This property returns the forward direction of the character model
        /// </summary>
        public Vector3 Forward
        {
            get
            {
                forward = gameNode.LocalAxes.GetColumn(2).NormalisedCopy;
                return forward;
            }
        }

        /// <summary>
        /// This virtual method is to initialize the model elements
        /// </summary>
        virtual protected void LoadModelElements() { }

        /// <summary>
        /// This virtual method is to define how the model is assembled
        /// </summary>
        virtual protected void AssembleModel() { }

        /// <summary>
        /// This virtual method is for dispose all the components of the character model
        /// </summary>
        virtual public void DisposeModel() { }
        
        /// <summary>
        /// This method dispose of the character model
        /// </summary>
        public override void Dispose()
        {
            DisposeModel();
            base.Dispose();
        }

    }
}
