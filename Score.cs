using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mogre;

namespace RaceGame
{
    class Score:Stat
    {

       // public int value;
        public override void Increase(int val)
        {
            value +=  val;
        }
    }
}
