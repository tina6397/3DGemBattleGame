using System;
using System.Collections.Generic;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    abstract class Projectile : MovableElement
    {
        Timer time;
        protected int maxTime = 500;
        public Vector3 initialVelocity;
        public Vector3 InitialVelocity
        {
            set { initialVelocity = value; }
            get { return initialVelocity; }

        }
    
        protected float speed;
        protected Vector3 initialDirection;
        public Vector3 InitialDirection
        {

            set
            {

                initialDirection = value;

                physObj.Velocity = speed * initialDirection;

            }

            get { return initialDirection; }

        }
        protected float healthDamage;
        public float HealthDamage
        {
            get { return healthDamage; }
        }

        protected float shieldDamage;
        public float ShieldDamage
        {
            get { return shieldDamage; }
        }

        virtual protected void Load() { }

        protected Projectile()
        {
            time = new Timer();
        }

        public override void Dispose()
        {

            base.Dispose();
            this.remove = true;
        }

        virtual public void Update(FrameEvent evt)
        {

           remove = IsCollidingWith("Robot");

           if (remove)
           {
               Dispose();

           }

            if (!remove && time.Milliseconds > maxTime)
            {

                Dispose();
                remove = true;
            }
        }

        private bool IsCollidingWith(string objName)
        {
            bool isColliding = false;
            foreach (Contacts c in physObj.CollisionList)
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
