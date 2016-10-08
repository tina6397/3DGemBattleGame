using Mogre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhysicsEng
{
    public class WeightForce : Force
    {
        //*************** DO NOT TOUCH THIS SECTION, THE METHOD TO IMPLEMENT IS AT THE BOTTOM *********************
        // --------------------- Fields ---------------------
        private const float g = 9.8f;
        float mass;

        // --------------------- Attributes ---------------------
        public float Gravity
        {
            get { return g; }
        }

        // -------------------------------- Constructor ----------------------------------

        public WeightForce(float invMass, string id = "WEIGHT")
        {
            type = id;
            this.mass = 1 / invMass;
        }

        //**********************************************************************************************************

        // ------------------------------- Compute Method ---------------------------------
        public override void compute(Vector3 Position = new Vector3())
        {
            // Your Implementation here, force is inherited from the Force class as protected field
            force = Vector3.UNIT_Y * mass * g * -1;
        }
    }
}
