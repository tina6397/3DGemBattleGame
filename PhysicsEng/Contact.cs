using System;
using Mogre;

namespace PhysicsEng
{
    public class Contacts
    {
        public PhysObj colliderObj;
        public PhysObj collidingObj;
        public float[] penetrationTimes;

        public Vector3 contactNormal;

        public Contacts()
        {
            contactNormal = new Vector3();
            penetrationTimes = new float[2];
        }
    }
}
