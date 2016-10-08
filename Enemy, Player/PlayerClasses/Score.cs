using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mogre;

namespace RaceGame
{
    class Score:Stat
    {

       /// <summary>
       /// A method to increase the value in score
       /// </summary>
       /// <param name="val"></param>
        public override void Increase(int val)
        {
            value +=  val;
        }
    }
}
