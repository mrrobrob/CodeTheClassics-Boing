using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boing;

internal class Bat : GameEntity
{
    public int Timer = 0;

    public Bat(int playerNo, Action moveFunc)
    {
        X = playerNo == 0 ? 40 : 760;
        Y = GameConstants.HalfHeight;
    }
}

