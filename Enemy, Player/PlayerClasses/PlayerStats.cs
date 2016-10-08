using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mogre;

namespace RaceGame
{
    class PlayerStats:CharacterStats
    {


        protected Stat score;      
        
        public Stat Score 
        {
            get { return score; }
        }

        /// <summary>
        /// A integer variable to store the enemy killed by the player
        /// </summary>
        public int enemyKilled;
        public int EnemyKilled
        {
            get { return enemyKilled; }
            set { enemyKilled = value; }
        }

        /// <summary>
        /// A methdo to initialize the player stat data to a value
        /// </summary>
        protected override void InitStats()
        {
            base.InitStats();
            score = new Score();

            score.InitValue(0);
            health.InitValue(5);
            shield.InitValue(5);
            lives.InitValue(1);
            currentLevel = 1;
            

            
        }

    }
}
