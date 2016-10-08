using System;

using Mogre;

namespace RaceGame
{
    /// <summary>
    /// This abstract method set some basic statistics for a character in the game
    /// </summary>
    abstract class CharacterStats
    {
        public CharacterStats()
        {
            InitStats();
        }
        public int currentLevel;
        public int CurrentLevel
        {
            get { return currentLevel; }
            set { currentLevel = value; }
        }
        //public void CurrentLevel(int v)
        //{
        //   currentLevel = v;

        //}
        protected Stat health;      // This field represents the health of the character
        /// <summary>
        /// Read only. This property gives back the health stat of the character.
        /// </summary>
        public Stat Health
        {
            get { return health; }
        }

        protected Stat shield;      // This field represents the shield of the character
        /// <summary>
        /// Read only. This property gives back the shield stat of the character.
        /// </summary>
        public Stat Shield
        {
            get { return shield; }
        }

        protected Stat lives;      // This field represents the lives stat of the character
        /// <summary>
        /// Read only. This property gives back the lives stat of the character.
        /// </summary>
        public Stat Lives
        {
            get { return lives; }
        }

        /// <summary>
        /// This method initializes the stats objects, initial values are to be set in derived classes
        /// </summary>
        virtual protected void InitStats() 
        {
            lives = new Stat();
            health = new Stat();
            shield = new Stat();
        }
    }
}
