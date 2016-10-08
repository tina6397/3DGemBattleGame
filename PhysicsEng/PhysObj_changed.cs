//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Mogre;

//namespace PhysicsEng
//{
//    class PhysObj
//    {
//        public float radius ;
//        public float Radius{
            
//            get { return radius; }
//            set { radius = value; }
//        }

//       // public List<MovableElement,MovableElement> CollisionList = new List<>;
//        public Vector3 position {get;set;}
//        public Vector3 velocity {get;set;}
//        public float radius {get;set;}
//        public float invMass {get;set;}
//        public float restitution {get;set;}
//        public Vector3 resForces {get;set;}
//        public SceneNode sceneNode {get;}

//       private List<Force> forceList{get;};

//        public PhysObj(){}

//        public PhysObj(SceneManager mSceneMgr, float radius, string id, [float invMass = 1.0f], [float restitution = 0.99f], [float frictionCoeff = 0.1f]){
        
//    }
//        //    this.forceList = new List<Force>();        }
            
    
//        public void addForceToList(Force force){
            
//            Force frc = new Force();
//            frc = forceList.Find(f => f.ID == force.ID);
//            if (frc==null)
//                forceList.Add(force);
//        }
//    }
//}
