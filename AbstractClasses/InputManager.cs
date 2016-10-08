using System;
using Mogre;
using System.Collections;

namespace RaceGame
{
    /// <summary>
    /// This class manages the inputs from keyboard and mouse
    /// </summary>
    class InputsManager
    {
        // Keyboard, mouse and inputs managers
        MOIS.Keyboard mKeyboard;
        MOIS.Mouse mMouse;
        MOIS.InputManager mInputMgr;

        PlayerController playerController;

        public PlayerController PlayerController
        {

            set { playerController = value; }

        }

        /// <summary>
        /// Private constructor (for singleton pattern)
        /// </summary>
        private InputsManager() { }

        private static InputsManager instance; // Private instance of the class
        /// <summary>
        /// Gives back a new istance of the class if the instance field is null
        /// otherwise it gives back the istance already created
        /// </summary>
        public static InputsManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InputsManager();
                }
                return instance;
            }
        }

        /// <summary>
        /// This method set the reaction to each key stroke
        /// </summary>
        /// <param name="evt">Can be used to tune the reaction timings</param>
        /// <returns></returns>
        public bool ProcessInput(FrameEvent evt)
        {
            Vector3 displacements = Vector3.ZERO;
            Vector3 angles = Vector3.ZERO;
            mKeyboard.Capture();
            mMouse.Capture();

            //set A = left key
            if (mKeyboard.IsKeyDown(MOIS.KeyCode.KC_A))
            {
                playerController.Left = true;
            }
            else
            {
                playerController.Left = false;
            }


            //set D = right key
            if (mKeyboard.IsKeyDown(MOIS.KeyCode.KC_D))
            {
                playerController.Right = true;
            }
            else
            {
                playerController.Right = false;
            }


            //set W = up key

            if (mKeyboard.IsKeyDown(MOIS.KeyCode.KC_W))
            {
                playerController.Forward = true;
            }
            else
            {

                playerController.Forward = false;
            }

            //set S = down key

            if (mKeyboard.IsKeyDown(MOIS.KeyCode.KC_S))
            {
                playerController.Backward = true;
            }
            else
            {
                playerController.Backward = false;
            }


            //set rotation
            if (mMouse.MouseState.ButtonDown(MOIS.MouseButtonID.MB_Left))
            {
                //angles.z = -mMouse.MouseState.X.rel;
                 angles.y = -mMouse.MouseState.X.rel;
            }
            if (mMouse.MouseState.ButtonDown(MOIS.MouseButtonID.MB_Right))
            {
                angles.x = -mMouse.MouseState.Y.rel;
            }
            playerController.Angles = angles;

            return true;
        }

        /// <summary>
        /// Initializes the keyboad, mouse and the input manager systems
        /// </summary>
        /// <param name="windowHandle">An handle to the game windonw</param>
        public void InitInput(ref int windowHandle)
        {
            mInputMgr = MOIS.InputManager.CreateInputSystem((uint)windowHandle);
            mKeyboard = (MOIS.Keyboard)mInputMgr.CreateInputObject(MOIS.Type.OISKeyboard, true);
            mMouse = (MOIS.Mouse)mInputMgr.CreateInputObject(MOIS.Type.OISMouse, false);

            mKeyboard.KeyPressed += new MOIS.KeyListener.KeyPressedHandler(OnKeyPressed);
        }

        /// <summary>
        /// Buffered key listener
        /// </summary>
        /// <param name="arg">A keyboard event</param>
        /// <returns></returns>
        private bool OnKeyPressed(MOIS.KeyEvent arg)
        {
            switch (arg.key)
            {
                case MOIS.KeyCode.KC_SPACE:
                    {
                        
                        playerController.Shoot = true;
                       // Game.shoot = true;
                        break;
                    }
                case MOIS.KeyCode.KC_E:
                    {
                        
                        playerController.ChangeGun = true;
                        break;
                    }
                case MOIS.KeyCode.KC_ESCAPE:
                    {
                        return false;
                    }
            }
            return true;
        }
    }
}
