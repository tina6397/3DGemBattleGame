using System;
using System.Collections.Generic;
using Mogre;

namespace RaceGame
{
    abstract class Collectable : MovableElement
    {
        virtual public void Update(FrameEvent evt) { }
    }
}
