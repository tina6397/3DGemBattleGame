using Mogre;
using Mogre.TutorialFramework;
using System;
using PhysicsEng;
using System.Collections.Generic;


namespace RaceGame
{
    class Game : BaseApplication
    {
        Player player;
        List<Robot> enemies;

        Environment environment;
        Light spotLight;

        int sceneLevel;

        Gun bombDropper;
        Gun cannon;
        CollectableGun collectableBombDropper;
        CollectableGun collectableCannon;
        static public List<CollectableGun> collectableGuns;


        //red gem
        List<RedGem> R_gems;
        List<RedGem> R_gemsToRemove;


        //green gem
        List<GreenGem> G_gems;
        List<GreenGem> G_gemsToRemove;


        //health up
        List<HealthUp> health_ups;
        List<HealthUp> health_up_to_remove;


        List<ShieldUp> shield_ups;
        List<ShieldUp> shield_up_to_remove;



        static List<Projectile> BombsFired;
        static public List<Projectile> GetBombsFired
        {
            get { return BombsFired; }
        }


        SceneNode cameraNode;
        InputsManager inputsManager = InputsManager.Instance;
        GameInterface gameHMD;

        ////static int x = 0;
        //static int health = 0;

        Physics physics;

        /// <summary>
        /// This method starts the rendering loop
        /// </summary>
        public static void Main()
        {
            new Game().Go();           
        }


        /// <summary>
        /// This method create a new camera
        /// </summary>
        protected override void CreateCamera()
        {

            mCamera = mSceneMgr.CreateCamera("PlayerCam");
            mCamera.Position = new Vector3(0, 100, 500);
            mCamera.Position = new Vector3(0, 50, -120);
            mCamera.LookAt(new Vector3(0, 0, 0));
            // nearest distance to POV
            mCamera.NearClipDistance = 5;
            mCamera.FarClipDistance = 1000;
            mCamera.FOVy = new Degree(80);

            mCameraMan = new CameraMan(mCamera);
            mCameraMan.Freeze = true;


        }


        /// <summary>
        /// This method create the initial scene
        /// </summary>
        protected override void CreateScene()
        {

            sceneLevel = 1;


            physics = new Physics();

            player = new Player(mSceneMgr);

            enemies = new List<Robot>();

            environment = new Environment(mSceneMgr, mWindow);

            cameraNode = mSceneMgr.CreateSceneNode();
            cameraNode.AttachObject(mCamera);

            //makes the camera follows player when player moves
            player.Model.GameNode.AddChild(cameraNode);


            //create oevrlay 
            gameHMD = new GameInterface(mSceneMgr, mWindow, player.Stats, player.PlayerArmoury);

            inputsManager.PlayerController = (PlayerController)player.Controller;
            physics.StartSimTimer();


            //gem
            R_gems = new List<RedGem>();
            R_gemsToRemove = new List<RedGem>();


            G_gems = new List<GreenGem>();
            G_gemsToRemove = new List<GreenGem>();


            //health up

            health_ups = new List<HealthUp>();
            health_up_to_remove = new List<HealthUp>();


            //shield up

            shield_ups = new List<ShieldUp>();
            shield_up_to_remove = new List<ShieldUp>();

            collectableGuns = new List<CollectableGun>();
            BombsFired = new List<Projectile>();

            //make spot light that attach to the camera node
            spotLight = mSceneMgr.CreateLight("spotLight");
            spotLight.Type = Light.LightTypes.LT_SPOTLIGHT;
            spotLight.DiffuseColour = ColourValue.White;

            spotLight.SpecularColour = ColourValue.White;
            spotLight.Direction = new Vector3(-1, -1, 0);
            spotLight.Position = new Vector3(500, 500, 0);
            spotLight.SetSpotlightRange(new Degree(50), new Degree(80));

            cameraNode.AttachObject(spotLight);




            CreateLevel1();






        }




        /// <summary>
        /// This method destrois the scene
        /// </summary>
        protected override void DestroyScene()
        {
            base.DestroyScene();
            cameraNode.DetachAllObjects();
            cameraNode.Dispose();
            gameHMD.Dispose();
            environment.Dispose();
            player.Model.Dispose();

            foreach (Robot r in enemies)
            {
                if (r.RobotModel != null)
                {
                    r.RobotModel.Dispose();

                }
            }

            foreach (CollectableGun g in collectableGuns)
            {
                g.Dispose();
            }



            //gem
            foreach (RedGem gem in R_gems)
            {
                gem.Dispose();
            }

            foreach (GreenGem gem in G_gems)
            {
                gem.Dispose();
            }

            //health up
            foreach (HealthUp h in health_ups)
            {
                h.Dispose();
            }
            physics.Dispose();

            foreach (ShieldUp h in shield_ups)
            {
                h.Dispose();
            }


        }


        /// <summary>
        /// This method create a new viewport
        /// </summary>
        protected override void CreateViewports()
        {
            //  base.CreateViewports();
            Viewport viewport = mWindow.AddViewport(mCamera);
            viewport.BackgroundColour = ColourValue.Black;
            mCamera.AspectRatio = viewport.ActualWidth / viewport.ActualHeight;
        }

        /// <summary>
        /// This method create the scene in LV 1
        /// </summary>
        protected void CreateLevel1()
        {
            //gem
            for (int x = 0; x < 5; x++)
            {
                AddGem();
            }
        }



        /// <summary>
        /// This method create the scene in LV 2
        /// </summary>
        protected void CreateLevel2()
        {
            //reset the lightings
            spotLight.SetSpotlightRange(new Degree(20), new Degree(40));
            spotLight.Position = new Vector3(350, 350, 0);


            //player.Model.SetPosition(new Vector3(0, 0, 0));
            for (int x = 0; x < 3; x++)
            {

                AddHealth();
                AddShield();

            }

            //create a new gun
            bombDropper = new BombDropper(mSceneMgr);

            collectableBombDropper = new CollectableGun(mSceneMgr, bombDropper, player.PlayerArmoury);
            collectableBombDropper.SetPosition(new Vector3(Mogre.Math.RangeRandom(-500, 500), 0, Mogre.Math.RangeRandom(-500, 500)));

            collectableGuns.Add(collectableBombDropper);

            cannon = new Cannon(mSceneMgr);
            collectableCannon = new CollectableGun(mSceneMgr, cannon, player.PlayerArmoury);
            collectableCannon.SetPosition(new Vector3(0, 0, 0));

            collectableGuns.Add(collectableCannon);

            //create 5 enemies in level 3 scen
            for (int x = 0; x < 5; x++)
            {
                AddDumbEnemies();

            }


            sceneLevel = 2;



        }



        /// <summary>
        /// This method create the scene in LV 3
        /// </summary>
        protected void CreateLevel3()
        {
            //reset the lightings for level 3
            spotLight.SetSpotlightRange(new Degree(5), new Degree(20));
            spotLight.Position = new Vector3(200, 200, 0);

            for (int x = 0; x < 5; x++)
            {
                AddCleverEnemies();

            }
  


            sceneLevel = 3;
        }

        /// <summary>
        /// This method update the scene after a frame has finished rendering
        /// </summary>
        /// <param name="evt"></param>
        protected override void UpdateScene(FrameEvent evt)
        {


            //Check if player is alive, if not, Dispose player model ------------------------------------------------
            if (player.Stats.Lives.Value == 0)
            {
                ((PlayerModel)player.Model).DisposeModel();

            }
            else
            {
                base.UpdateScene(evt);
                physics.UpdatePhysics(0.01f);
                player.Update(evt);
                gameHMD.Update(evt);
                physics.UpdatePhysics(0.01f);




                // Update level system when score is reached ------------------------------------------------
                if (player.Stats.currentLevel == 2 && sceneLevel == 1)
                {
                    CreateLevel2();
                }

                if (player.Stats.currentLevel == 3 && sceneLevel == 2)
                {
                    CreateLevel3();
                }


                //Update when level is 2 or above-------------------------------------------------------------
                if (sceneLevel >1 )
                {
                    //check if need to shoot
                    if (player.Controller.Shoot)
                    {


                        if (player.PlayerArmoury.activeGun != null)
                        {
                            player.PlayerArmoury.activeGun.Fire();

                        }
                    }

                    player.Controller.Shoot = false;

                    //Check if need to change gun------------------------------
                    if (((PlayerController)player.Controller).ChangeGun == true)
                    {
                        if (player.PlayerArmoury.activeGun == bombDropper)
                        {
                            player.PlayerArmoury.ChangeGun(cannon);
                        }
                        else
                        {
                            player.PlayerArmoury.ChangeGun(bombDropper);


                        }
                    }
                    ((PlayerController)player.Controller).ChangeGun = false;
                    

                    foreach (CollectableGun g in collectableGuns)
                    {
                        g.Update(evt);
                    }

                    //Update enemies ------------------------------------------------
                    foreach (Robot robot in enemies)
                    {
                        //check if robot model exists
                        if (robot.RobotModel != null)
                        {

                            robot.Update(evt);

                            //if the bomb hits the robot, add points to the player, and dispose the model and physics object

                            if (robot.RobotModel.RemoveMe)
                            {
                                ((PlayerStats)player.Stats).Score.Increase(20);
                                robot.RobotModel.Dispose();
                                robot.RobotModel = null;

                            }
                        }
                    }
                }



                //update gems, bullets, power ups with collision detection ------------------------------------------------------
                foreach (RedGem gem in R_gems)
                {
                    gem.Update(evt);
                    if (gem.RemoveMe)
                    {
                        gem.Update(evt);
                        if (gem.RemoveMe)
                            R_gemsToRemove.Add(gem);

                    }

                }

                foreach (RedGem gem in R_gemsToRemove)
                {
                    R_gems.Remove(gem);
                    gem.Dispose();
                }
                R_gemsToRemove.Clear();

                //green
                foreach (GreenGem gem in G_gems)
                {
                    gem.Update(evt);
                    if (gem.RemoveMe)
                    {
                        gem.Update(evt);
                        if (gem.RemoveMe)
                            G_gemsToRemove.Add(gem);

                    }

                }

                foreach (GreenGem gem in G_gemsToRemove)
                {
                    G_gems.Remove(gem);
                    gem.Dispose();
                }
                R_gemsToRemove.Clear();

                //stat 
                foreach (HealthUp h in health_ups)
                {
                    h.Update(evt);
                    if (h.RemoveMe)
                        health_up_to_remove.Add(h);
                }

                foreach (HealthUp h in health_up_to_remove)
                {
                    health_ups.Remove(h);
                    h.Dispose();
                }
                health_up_to_remove.Clear();

                foreach (ShieldUp h in shield_ups)
                {
                    h.Update(evt);
                    if (h.RemoveMe)
                        shield_up_to_remove.Add(h);
                }

                foreach (ShieldUp h in shield_up_to_remove)
                {
                    shield_ups.Remove(h);
                    h.Dispose();
                }
                shield_up_to_remove.Clear();

                foreach (Projectile p in BombsFired)
                {
                    p.Update(evt);
                }


            }

           

        }

        /// <summary>
        /// This method add the gems into the scene
        /// </summary>
        private void AddGem()
        {

            RedGem gem = new RedGem(mSceneMgr, ((PlayerStats)player.Stats).Score);
            gem.SetPosition(new Vector3(Mogre.Math.RangeRandom(-500, 500), 0, Mogre.Math.RangeRandom(-500, 500)));
            R_gems.Add(gem);

            GreenGem gem2 = new GreenGem(mSceneMgr, ((PlayerStats)player.Stats).Score);
            gem2.SetPosition(new Vector3(Mogre.Math.RangeRandom(-500, 500), 0, Mogre.Math.RangeRandom(-500, 500)));
            G_gems.Add(gem2);
        }


        /// <summary>
        /// This method add the dumb enemies into the scene
        /// </summary>
        private void AddDumbEnemies()
        {
            Robot robot = new Robot(mSceneMgr, player,false);
            robot.Model.SetPosition(new Vector3(Mogre.Math.RangeRandom(-300, 300), 0, Mogre.Math.RangeRandom(-300, 300)));
            enemies.Add(robot);

        }


        /// <summary>
        /// This method add the clever enemies into the scene
        /// </summary>
        private void AddCleverEnemies()
        {
            Robot robot = new Robot(mSceneMgr, player, true);
            robot.Model.SetPosition(new Vector3(Mogre.Math.RangeRandom(-300, 300), 0, Mogre.Math.RangeRandom(-300, 300)));
            enemies.Add(robot);

        }


        /// <summary>
        /// This method add the health power-ups into the scene
        /// </summary>
        private void AddHealth()
        {

            HealthUp h = new HealthUp(mSceneMgr, ((PlayerStats)player.Stats).Health);
            Vector3 position = new Vector3(Mogre.Math.RangeRandom(-500, 500), 0, Mogre.Math.RangeRandom(-500, 500));
            h.SetPosition(position);
            health_ups.Add(h);

        }


        /// <summary>
        /// This method add the shielf power-ups into the scene
        /// </summary>
        private void AddShield()
        {

            ShieldUp h = new ShieldUp(mSceneMgr, ((PlayerStats)player.Stats).Shield);
            Vector3 position = new Vector3(Mogre.Math.RangeRandom(-500, 500), 0, Mogre.Math.RangeRandom(-500, 500));
            h.SetPosition(position);
            shield_ups.Add(h);

        }

        /// <summary>
        /// This method set create a frame listener to handle events before, during or after the frame rendering
        /// </summary>
        protected override void CreateFrameListeners()
        {
            base.CreateFrameListeners();
            mRoot.FrameRenderingQueued +=
                new FrameListener.FrameRenderingQueuedHandler(inputsManager.ProcessInput);

        }

        /// <summary>
        /// This method initilize the inputs reading from keyboard adn mouse
        /// </summary>
        protected override void InitializeInput()
        {
            base.InitializeInput();

            int windowHandle;
            mWindow.GetCustomAttribute("WINDOW", out windowHandle);
            inputsManager.InitInput(ref windowHandle);
        }
    }
}