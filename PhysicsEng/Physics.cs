using Mogre;
using System;
using System.Collections.Generic;

namespace PhysicsEng
{
    public class Physics
    {
        #region ************************************ DO NOT TOUCH THIS SECTION **************************************************************
        // ************************** THE METHODS YOU MUST IMPLEMENT ARE IN THE NEXT SECTION (Line 40) *******************************************
        // --------------------- Fields ---------------------
        private const float MINSPEED = 0.00000001f;

        private Timer simTime;
        private float dt;

        static private List<PhysObj> physObjsList;
        static private List<PhysObj> toRemove;
        static private List<Plane> boundaries;

        // --------------------- Properties ---------------------
        public float Milliseconds
        {
            get { return simTime.Milliseconds; }
        }
        #endregion *********************************************************************************************************

        #region ********************************** METHODS TO IMPLEMENT **************************************

        private Vector3 VelVerletSolver(PhysObj obj, float Dt)
        {
            Vector3 newPos = new Vector3();
            Vector3 finalVel = new Vector3();
            Vector3 oldResForces = new Vector3();

            oldResForces = obj.ResForces; 	//Froces at this frame

            // ***---> YOUR POSITION UPDATE IMPLEMENTATION HERE! <---***
            newPos = obj.Position + (obj.Velocity * Dt) + ((Dt * Dt * 0.5f * obj.InvMass) * oldResForces);

            obj.UpdateResFor(newPos);//Forces at next frame (access thorugh obj.ResForces)

            // ***---> YOUR VELOCITY UPDATE IMPLEMENTATION HERE! <---***
            finalVel = obj.Velocity + ((Dt * 0.5f * obj.InvMass)) * (obj.ResForces + oldResForces);

            return finalVel;
        }

        private Vector3 Impulse(PhysObj obj, PhysObj obj1)
        {
            Vector3 impulse;
            // ***---> YOUR IMPULSE IMPLEMENTATION HERE! <---***
            impulse = (-(obj.Restitution + 1) / (obj.InvMass + obj1.InvMass)) * (obj.Velocity - obj1.Velocity);

            return impulse;
        }
        #endregion *********************************************************************************************************

        #region *********************************** DO NOT TOUCH THIS SECTION *******************************************
        #region Engine
        //---------------------------------------- Constructor -----------------------------------------
        public Physics()
        {
            physObjsList = new List<PhysObj>();
            toRemove = new List<PhysObj>();
            boundaries = new List<Plane>();
        }

        //---------------------------------------- Update Method -----------------------------------------
        public void UpdatePhysics(float frequency)
        {
            foreach (PhysObj po in toRemove)
            {
                physObjsList.Remove(po);
            }
            toRemove.Clear();

            dt = frequency;

            if (Milliseconds > frequency)
            {
                UpdateObjsCollisionList(physObjsList);                                                                   //Generate Collision lists

                foreach (PhysObj obj in physObjsList)
                {
                    List<PhysObj> objCollisionList;

                    GetCollidingObjects(obj, physObjsList, out objCollisionList);

                    if (objCollisionList.Count != 0)
                    {
                        CollisionReactions(obj, objCollisionList);                                                      //Compute collisions reactions between objects    
                    }
                }

                foreach (PhysObj obj in physObjsList)                                                                    //For each physics object in the simulation
                {
                    obj.Velocity = VelVerletSolver(obj, frequency);                                                     //Update object velocity
                    foreach (Plane plane in boundaries)
                    {
                        PlaneCollision(obj, plane);
                    }
                    obj.UpdatePostion(frequency);                                                                       //Update object position
                }

                ResetSimTimer();                                                                                        //Reset the simulation timer
            }
        }

        //---------------------------------------- Auxiliary Methods -----------------------------------------

        static public void AddPhysObj(PhysObj physObj)
        {
            physObjsList.Add(physObj);
        }

        static public void AddBoundary(Plane boundary)
        {
            boundaries.Add(boundary);
        }

        static public void RemovePhysObj(PhysObj physObj)
        {
            toRemove.Add(physObj);
        }

        private void PlaneCollision(PhysObj obj, Plane plane)
        {
            float normalSpeed = obj.Velocity.DotProduct(plane.normal);
            float potential = obj.Position.DotProduct(plane.normal);
            if (normalSpeed < 0)
            {
                float t = (obj.Radius - potential - plane.d) / normalSpeed;
                if (t <= dt)
                {
                    obj.Velocity -= (1 + obj.Restitution) * normalSpeed * plane.normal;
                }
            }
            Vector3 pos = obj.Position - obj.Radius * plane.normal;
            if (pos.DotProduct(plane.normal) + plane.d < 0)
            {
                obj.Position = obj.Position - (pos.DotProduct(plane.normal) + plane.d) * plane.normal;
            }
        }

        private void UpdateObjsCollisionList(List<PhysObj> physObjList)
        {
            for (int i = 0; i < physObjList.Count - 1; i++)
                physObjList[i].CollisionList.Clear();

            for (int i = 0; i < physObjList.Count - 1; i++)
            {
                for (int j = i + 1; j < physObjList.Count; j++)
                {
                    float[] times = new float[2];
                    if (SphereCollision(physObjList[i], physObjList[j], ref times))
                    {
                        Vector3 contactNormal = Vector3.ZERO;
                        if (times[1] >= 0 && times[0] <= dt)
                            contactNormal = (physObjList[j].Position - physObjList[i].Position) + times[0] * (physObjList[j].Velocity - physObjList[i].Velocity);

                        Contacts contact = new Contacts();
                        contact.colliderObj = physObjList[i];
                        contact.collidingObj = physObjList[j];
                        contact.penetrationTimes = times;
                        contact.contactNormal = contactNormal;
                        physObjList[i].CollisionList.Add(contact);
                        physObjList[j].CollisionList.Add(contact);
                    }
                }
            }
        }

        private bool SphereCollision(PhysObj obj, PhysObj potCollObj, ref float[] times)
        {
            times[0] = dt + 1;
            times[1] = -1;

            bool collision = false;

            Vector3 c = potCollObj.Position - obj.Position;
            float r = obj.Radius + potCollObj.Radius;

            Vector3 v = potCollObj.Velocity - obj.Velocity;

            if (v != Vector3.ZERO)
            {
                float cDotV = c.DotProduct(v);

                float sqrNormV = v.SquaredLength;

                float numTerm = c.SquaredLength - r * r;

                float delta = cDotV * cDotV - sqrNormV * numTerm;

                if (delta >= 0)
                {
                    float sqrtDelta = Mogre.Math.Sqrt(delta);
                    float time1 = (-cDotV + sqrtDelta) / sqrNormV;
                    float time2 = -(cDotV + sqrtDelta) / sqrNormV;

                    if (time2 < time1)
                    {
                        times[0] = time2;
                        times[1] = time1;
                    }
                    else
                    {
                        times[0] = time1;
                        times[1] = time2;
                    }
                }

                if (times[1] >= 0 && times[0] <= dt)
                {
                    collision = true;
                }
            }
            else
            {
                if (c.Length < r)
                    collision = true;
            }

            return collision;
        }

        private void GetCollidingObjects(PhysObj obj, List<PhysObj> physObjList, out List<PhysObj> collisionList)
        {
            collisionList = new List<PhysObj>();
            foreach (Contacts contObj in obj.CollisionList)
            {
                PhysObj collObj = physObjList.Find(el => el.ID == contObj.collidingObj.ID);

                if (collObj != null)
                    collisionList.Add(collObj);
            }
        }

        private void BoundingSphereSeparation(PhysObj obj)
        {
            foreach (Contacts c in obj.CollisionList)
            {
                Vector3 direction = obj.Position - c.collidingObj.Position;
                float distance = direction.Normalise();
                float radiiSum = obj.Radius + c.collidingObj.Radius;
                if (distance < radiiSum)
                {
                    Vector3 separation = 0.5f * (radiiSum - distance) * direction;
                    obj.Position += separation;
                    c.collidingObj.Position -= separation;
                }
            }
        }

        private void CollisionReactions(PhysObj obj, List<PhysObj> collisionList)
        {
            BoundingSphereSeparation(obj);
            for (int i = 0; i < collisionList.Count; i++)
            {
                Vector3 imp = Impulse(obj, collisionList[i]);

                obj.Velocity += imp * obj.InvMass;

                collisionList[i].Velocity -= imp * collisionList[i].InvMass;
            }
        }

        public void StartSimTimer()
        {
            simTime = new Timer();
        }

        public void StopSimTimer()
        {
            simTime.Dispose();
        }

        public void ResetSimTimer()
        {
            simTime.Reset();
        }

        public void Dispose()
        {
            physObjsList.Clear();
            boundaries.Clear();
        }

        //*********************************************************************************************************
        #endregion
        #endregion
    }
}