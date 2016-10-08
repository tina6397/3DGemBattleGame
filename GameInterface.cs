using System;
using System.Collections.Generic;
using Mogre;

namespace RaceGame
{
    /// <summary>
    /// This class implements an example of interface
    /// </summary>
    class GameInterface : HMD     // Game interface inherits form the Head Mounted Dispaly (HMD) class
    {
        private PanelOverlayElement panel;

        private OverlayElement scoreText;
        private OverlayElement timeText;


        private OverlayElement gameOver;
        private OverlayElement guns;
        private OverlayElement level;


        private OverlayElement healthBar;
        private OverlayElement shieldBar;

        private Overlay overlay3D;
        private Entity lifeEntity;
        private List<SceneNode> lives;
        private Timer timer;
        private Timer timer2;
        ulong timer_countdown;

        int timeLimit = 40000;

        private Boolean isGameEnded = false;

        private string gameOverText = "Game over";

        public Timer Time
        {
            set { timer = value; }
        }

        private float hRatio;
        private float sRatio;
        private string score = "Score: ";
        private string timeDisplay = "Time left: ";
        private Armoury playerArmoury;
        private CharacterStats playerStats
        {
            get;
            set;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mSceneMgr">A reference of a scene manager</param>
        /// <param name="playerStats">A reference to a character stats</param>
        public GameInterface(SceneManager mSceneMgr,
            RenderWindow mWindow, CharacterStats playerStats, Armoury playerArmoury)
            : base(mSceneMgr, mWindow, playerStats)  // this calls the constructor of the parent class
        {
            this.playerArmoury = playerArmoury;
            this.playerStats = playerStats;
            Load("GameInterface");
            
        }

        /// <summary>
        /// This method initializes the element of the interface
        /// </summary>
        /// <param name="name"> A name to pass to generate the overaly </param>
        protected override void Load(string name)
        {
            base.Load(name);

            lives = new List<SceneNode>();
            timer = new Timer();

            gameOver = OverlayManager.Singleton.GetOverlayElement("gameOver");
            gameOver.Left = mWindow.Width * 0.5f;

            level = OverlayManager.Singleton.GetOverlayElement("level");
            level.Left = mWindow.Width * 0.5f;



            healthBar = OverlayManager.Singleton.GetOverlayElement("HealthBar");
            hRatio = healthBar.Width / (float)characterStats.Health.Max;

            shieldBar = OverlayManager.Singleton.GetOverlayElement("ShieldBar");
            sRatio = shieldBar.Width / (float)characterStats.Shield.Max;

            guns = OverlayManager.Singleton.GetOverlayElement("guns");
            guns.Caption = "no guns loaded";
            guns.Left = mWindow.Width * 0.2f;


            timeText = OverlayManager.Singleton.GetOverlayElement("timeText");
            //timeText.Caption = timeDisplay;
            //timeText.Left = mWindow.Width * 0.5f;

            scoreText = OverlayManager.Singleton.GetOverlayElement("ScoreText");
            scoreText.Caption = score;
            scoreText.Left = mWindow.Width * 0.3f;


            panel =
           (PanelOverlayElement)OverlayManager.Singleton.GetOverlayElement("GreenBackground");
            
            LoadOverlay3D();
        }

        /// <summary>
        /// This method initalize a 3D overlay
        /// </summary>
        private void LoadOverlay3D()
        {
            overlay3D = OverlayManager.Singleton.Create("3DOverlay");
            overlay3D.ZOrder = 10000;

            CreateHearts();

            overlay3D.Show();
        }

        /// <summary>
        /// This method generate as many hearts as the number of lives left
        /// </summary>
        private void CreateHearts()
        {
            for (int i = 0; i < characterStats.Lives.Value; i++)
                AddHeart(i);


        }

        /// <summary>
        /// This method add an heart to the 3D overlay
        /// </summary>
        /// <param name="n"> A numeric tag</param>
        private void AddHeart(int n)
        {
            SceneNode livesNode = CreateHeart(n);
            lives.Add(livesNode);
            overlay3D.Add3D(livesNode);
        }

        /// <summary>
        /// This method remove from the 3D overlay and destries the passed scene node
        /// </summary>
        /// <param name="life"></param>
        private void RemoveAndDestroyLife(SceneNode life)
        {
            overlay3D.Remove3D(life);
            lives.Remove(life);
            MovableObject heart = life.GetAttachedObject(0);
            life.DetachAllObjects();
            life.Dispose();
            heart.Dispose();
        }

        /// <summary>
        /// This method initializes the heart node and entity
        /// </summary>
        /// <param name="n"> A numeric tag used to determine the heart postion on sceen </param>
        /// <returns></returns>
        private SceneNode CreateHeart(int n)
        {
            lifeEntity = mSceneMgr.CreateEntity("Heart.mesh");
            lifeEntity.SetMaterialName("HeartHMD");
            SceneNode livesNode;
            livesNode = new SceneNode(mSceneMgr);
            livesNode.AttachObject(lifeEntity);
            livesNode.Scale(new Vector3(0.2f, 0.2f, 0.2f));
            livesNode.Position = new Vector3(5f, 5f, -8) - n * 0.7f * Vector3.UNIT_X; ;
            livesNode.SetVisible(true);
            return livesNode;
        }

        /// <summary>
        /// This method converts milliseconds in to minutes and second format mm:ss
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private string convertTime(float time)
        {
            string convTime;
            float secs = time / 1000f;
            int min = (int)(secs / 60);
            secs = (int)secs % 60f;
            if (secs < 10)
                convTime = min + ":0" + secs;
            else
                convTime = min + ":" + secs;
            return convTime;
        }

        /// <summary>
        /// This method updates the interface
        /// </summary>
        /// <param name="evt"></param>
        public override void Update(FrameEvent evt)
        {
            base.Update(evt);

            Animate(evt);


            //if (((PlayerStats)characterStats).CurrentLevel == 1)
            //{
            //    if (((PlayerStats)characterStats).Score.Value >= 2)
            //    {
            //        gameOver.Caption = "You entered Level 2";
            //        ((PlayerStats)characterStats).CurrentLevel = 2;
            //        //if (convertTime(100 - timer2.Milliseconds) == "0:00" ){
            //        //    gameOver.Caption = "";

            //        //}
            //    }
            //    else
            //    {
            //        gameOver.Caption = "";
            //    }
            //}

            //if (((PlayerStats)characterStats).CurrentLevel == 2)
            //{
            //    if (((PlayerStats)characterStats).Score.Value >= 4)
            //    {
            //        gameOver.Caption = "You entered Level 3";
            //        ((PlayerStats)characterStats).CurrentLevel = 3;
            //        //if (convertTime(100 - timer2.Milliseconds) == "0:00" ){
            //        //    gameOver.Caption = "";

            //        //}
            //    }
            //    else
            //    {
            //        gameOver.Caption = "";
            //    }
            //}


            if (((PlayerStats)characterStats).enemyKilled == 10)
            {
                isGameEnded = true;
                gameOverText = "Congratulations, YOU Won ! Woooooo";
                panel.Width = mWindow.Width;
                panel.Height = mWindow.Height;

            }

            if (((PlayerStats)characterStats).CurrentLevel == 1)
            {
                 level.Caption = "LV 1";


                 

            }

            if (((PlayerStats)characterStats).CurrentLevel == 2)
            {
                level.Caption = "LV 2";

            }


            if (((PlayerStats)characterStats).CurrentLevel == 3)
            {
                level.Caption = "LV 3";

            }

            if (playerArmoury.activeGun != null){
                guns.Caption = "ammo: " + playerArmoury.activeGun.Ammo.Value;
            }

            if (lives.Count > characterStats.Lives.Value && characterStats.Lives.Value >= 0)
            {
                SceneNode life = lives[lives.Count - 1];
                RemoveAndDestroyLife(life);

            }
            if (lives.Count < characterStats.Lives.Value)
            {
                AddHeart(characterStats.Lives.Value);
            }

            if (convertTime(timeLimit - timer.Milliseconds) == "0:00")
            {
                isGameEnded = true;
                gameOver.Caption = gameOverText;
                panel.Width = mWindow.Width;
                panel.Height = mWindow.Height;

            }

            if (isGameEnded)
            {

                    
                    timeText.Caption =   "" ;
                    level.Caption = "";
                    guns.Caption = "";
                    scoreText.Caption = "";
                    score = "";
                    playerStats.Lives.Value = 0;
                    playerStats.Health.Value = 0;
                    playerStats.Shield.Value = 0;
                    
               
                    this.Dispose();

                    
            }
            else
            {
                 timeText.Caption = "Time Left:" + convertTime(timeLimit - timer.Milliseconds);
                 scoreText.Caption = score + ((PlayerStats)characterStats).Score.Value;

            }

            if (((PlayerStats)characterStats).Lives.Value == 0)
            {
                isGameEnded = true;
                gameOver.Caption = gameOverText;
                panel.Width = mWindow.Width;
                panel.Height = mWindow.Height;

            }
            healthBar.Width = hRatio * characterStats.Health.Value;
            shieldBar.Width = sRatio * characterStats.Shield.Value;


        }

        /// <summary>
        /// This method animates the heart rotation
        /// </summary>
        /// <param name="evt"></param>
        protected override void Animate(FrameEvent evt)
        {
            foreach (SceneNode sn in lives)
                sn.Yaw(evt.timeSinceLastFrame);
        }

        /// <summary>
        /// This method disposes of the elements generated in the interface
        /// </summary>
        public override void Dispose()
        {
            List<SceneNode> toRemove = new List<SceneNode>();
            foreach (SceneNode life in lives)
            {
                toRemove.Add(life);
            }
            foreach (SceneNode life in toRemove)
            {
                RemoveAndDestroyLife(life);
            }
            lifeEntity.Dispose();
            toRemove.Clear();
            shieldBar.Dispose();
            healthBar.Dispose();
            scoreText.Dispose();

            panel.Dispose();
            overlay3D.Dispose();
            base.Dispose();
        }
    }
}
