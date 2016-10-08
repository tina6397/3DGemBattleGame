using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    /// <summary>
    /// This class contains the robot model
    /// </summary>
    class RobotModel : CharacterModel
    {
        ModelElement robot;
        Player player;

        Radian angle;           // Angle for the mesh rotation
        Vector3 direction;      // Direction of motion of the mesh for a single frame
        float radius;           // Radius of the circular trajectory of the mesh

        const float maxTime = 2000f;        // Time when the animation have to be changed
        Timer time;                         // Timer for animation changes
        AnimationState animationState;      // Animation state, retrieves and store an animation from an Entity

        string animationName;               // Name of the animation to use

        Vector3 playerPosition;
        Vector3 playerDirection;

        Boolean isClever;


        /// <summary>
        /// Contructor that takes in the player object and a boolean which stated if the robot have AI
        /// </summary>
        /// <param name="mSceneMgr"></param>
        /// <param name="player"></param>
        /// <param name="isClever"></param>
        public RobotModel(SceneManager mSceneMgr, Player player, Boolean isClever )
        {
            this.mSceneMgr = mSceneMgr;
            LoadModelElements();
            this.player = player;
            this.isClever = isClever;
            AnimationSetup();

        }
        /// <summary>
        /// This method create a new model element for the robot and apply physics to it
        /// </summary>
        public void LoadModelElements()
        {
            robot = new ModelElement(mSceneMgr, "robot.mesh");
            gameEntity = robot.GameEntity;
            gameNode = robot.GameNode;
            mSceneMgr.RootSceneNode.AddChild(gameNode);


            //phy
            physObj = new PhysObj(mSceneMgr, 15, "Robot", 0.02f, 0.8f, 10f);
            physObj.SceneNode = gameNode;
            physObj.AddForceToList(new WeightForce(physObj.InvMass));
            physObj.AddForceToList(new FrictionForce(physObj));
            
            Physics.AddPhysObj(physObj);

        }

        /// <summary>
        /// This method is a public method that calls the private method to animated the robot 
        /// </summary>
        /// <param name="evt"></param>
        public void AnimateRobot(FrameEvent evt)
        {
            AnimateMesh(evt);
            
        }


        /// <summary>
        /// This set up the initial animation atate of the robot
        /// </summary>
        private void AnimationSetup()
        {
            radius = 0.01f;
            direction = Vector3.ZERO;
            angle = 0f;

            animationName = "Shoot";
            LoadAnimation();
        }

        /// <summary>
        /// This method let the robot to move in circle
        /// </summary>
        /// <param name="evt"></param>
        private void CircularMotion(FrameEvent evt)
        {
            angle += (Radian)evt.timeSinceLastFrame;
            direction = radius * new Vector3(Mogre.Math.Cos(angle), 0, Mogre.Math.Sin(angle));
            gameNode.Translate(direction);
            gameNode.Yaw(-evt.timeSinceLastFrame);
        }


        /// <summary>
        /// This method loads the animation from the animation name
        /// </summary>
        private void LoadAnimation()
        {
            animationState = gameEntity.GetAnimationState(animationName);
            animationState.Loop = true;
            animationState.Enabled = true;
        }

        /// <summary>
        /// This method puts the mesh in motion
        /// </summary>
        /// <param name="evt">A frame event which can be used to tune the animation timings</param>
        private void AnimateMesh(FrameEvent evt)
        {

            playerPosition = player.Position;
            playerDirection = playerPosition - gameNode.Position;
            animationState.AddTime(evt.timeSinceLastFrame);

            //check if player is close and if robot have AI----------------------------------

            //playerDirection = playerDirection.NormalisedCopy;
            if (isClever && playerDirection < (new Vector3(150, 150, 150)))
            {
                physObj.Velocity = 80 * evt.timeSinceLastFrame * playerDirection;

            }
            else
            {
                //if not an AI robot, spin around 
                CircularMotion(evt);
            }





        }

        /// <summary>
        /// This method dispose the robot model
        /// </summary>
        public override void DisposeModel()
        {
            base.DisposeModel();
        }

        /// <summary>
        /// This method dispose the robot and the physics object
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();

            robot.Dispose();
            gameEntity.Dispose();
            gameNode.Dispose();
            Physics.RemovePhysObj(physObj);
            physObj = null;


        }

    }

}
