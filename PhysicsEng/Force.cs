using Mogre;
using System;

namespace PhysicsEng
{
    public class Force
    {
        //*************** DO NOT TOUCH THIS CLASS *********************
        // --------------------- Fields ---------------------
        protected Vector3 force;
        protected string type;

        // --------------------- Attributes ---------------------
        public Vector3 ForceVect
        {
            get { return force; }
            set { force = value; }
        }

        public string ID
        {
            get { return type; }
        }

        // -------------------------------- Constructor ----------------------------------
        public Force(string id = "GENERIC")
        {
            type = id;
        }

        // ------------------------------- Compute Method ---------------------------------
        public virtual void compute(Vector3 Position)
        {
            // Virtual method NO IMPLEMENTATION HERE
        }
    }
}
