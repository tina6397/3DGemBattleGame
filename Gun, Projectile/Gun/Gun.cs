using System;
using Mogre;
using PhysicsEng;


namespace RaceGame
{
    abstract class Gun : MovableElement
    {
        protected int maxAmmo;

        protected Projectile projectile;
        public Projectile Projectile
        {
            set { projectile = value; }
        }

        protected Stat ammo;
        public Stat Ammo
        {
            get { return ammo; }
        }

        public Vector3 GunPosition()
        {
            SceneNode node = gameNode;
            try
            {
                while (node.ParentSceneNode.ParentSceneNode != null)
                {
                    node = node.ParentSceneNode;
                }
            }
            catch (System.AccessViolationException)
            { }

            return node.Position;
        }

        public Vector3 GunDirection()
        {
            SceneNode node = gameNode;
            try
            {
                while (node.ParentSceneNode.ParentSceneNode != null)
                {
                    node = node.ParentSceneNode;
                }
            }
            catch (System.AccessViolationException)
            { }

            Vector3 direction = node.LocalAxes * gameNode.LocalAxes.GetColumn(2);

            return direction;
        }
        public void update()
        {

            if (IsCollidingWith("Player"))
            {
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


        virtual protected void LoadModel() { }
        virtual public void ReloadAmmo() { }
        virtual public void Fire() { }

    }
}
