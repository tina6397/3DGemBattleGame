using System;
using Mogre;

namespace RaceGame
{
    /// <summary>
    /// This abstrac class allows the creation of Head Mounted Displays (HMD)
    /// </summary>
    abstract class HMD
    {
        protected RenderWindow mWindow;
        protected SceneManager mSceneMgr;

        protected Overlay overlay;
        protected CharacterStats characterStats;

        /// <summary>
        /// Protected constructor
        /// </summary>
        /// <param name="mSceneMgr">A reference to the scene manager</param>
        /// <param name="characterStat">A reference to a character stat</param>
        protected HMD(SceneManager mSceneMgr, RenderWindow mWindow, CharacterStats characterStat)
        {
            this.mWindow = mWindow;
            this.mSceneMgr = mSceneMgr;
            this.characterStats = characterStat;
        }

        /// <summary>
        /// This method initializes the overaly for the HMD
        /// </summary>
        /// <param name="name">The name of the script which contain the overlay description</param>
        virtual protected void Load(string name)
        {
            this.overlay = OverlayManager.Singleton.GetByName(name);
            overlay.Show();
        }

        /// <summary>
        /// This method animates overlay elements
        /// </summary>
        /// <param name="evt">A frame event which can be used to tune the animation timing</param>
        virtual protected void Animate(FrameEvent evt) { }

        /// <summary>
        /// This method updates the overlay
        /// </summary>
        /// <param name="evt">A frame event</param>
        virtual public void Update(FrameEvent evt)
        {
            Animate(evt);
        }

        /// <summary>
        /// This method dispose of the HMD
        /// </summary>
        virtual public void Dispose()
        {
            if (overlay != null)
                overlay.Dispose();
        }
    }
}
