using Mogre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhysicsEng
{
    public class FrictionForce : Force
    {
        //*************** DO NOT TOUCH THIS SECTION, THE METHOD TO IMPLEMENT IS AT THE BOTTOM *********************
        // --------------------- Fields ---------------------
        private Vector3 normal;
        private Vector3 velocityDir;
        PhysObj obj;

        // -------------------------------- Constructor ----------------------------------
        public FrictionForce(PhysObj obj, string id = "FRICTION")
        {
            type = id;
            this.obj = obj;

        }
        //**********************************************************************************************************

        // ------------------------------- Compute Method ---------------------------------
        public override void compute(Vector3 Position = new Vector3())
        {
            velocityDir = obj.Velocity;
            normal = velocityDir.NormalisedCopy;
            // Your Implementation here, force is inherited from the Force class as protected field
            force = obj.FrictionCoeff * -1 * velocityDir;
        }
    }
}
