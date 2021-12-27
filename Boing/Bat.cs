using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boing;

internal class Bat : GameEntity
{
    public int Timer = 0;
    private int playerNo;
    private int score = 0;
    Func<int> moveFunc;
    public int Score = 0;

    public Bat(int playerNo, Func<int>? moveFunc)
    {
        X = playerNo == 0 ? 40 : 760;
        Y = GameConstants.HalfHeight;

        this.playerNo = playerNo;

        this.moveFunc = moveFunc ?? AiMoveFunc;
    }

    public override void Update()
    {
        Timer--;
        var yMovement = moveFunc();

        Y = Math.Min(400, Math.Max(80, Y + yMovement));

        var frame = 0;

        if (Timer > 0)
        {
            if (Game.Ball.Out())
            {
                frame = 2;
            } else
            {
                frame = 1;
            }
        }

        Image = $"bat{playerNo}{frame}";
    }

    public int AiMoveFunc()
    {
        var xDistance = Math.Abs(Game.Ball.X - X);
        var targetY1 = GameConstants.HalfHeight;
        var targetY2 = Game.Ball.Y + Game.AiOffSet;
        var weight1 = Math.Min(1, xDistance / GameConstants.HalfWidth);
        var weight2 = 1 - weight1;
        var targetY = (weight1 * targetY1) + (weight2 * targetY2);
        return Math.Min(GameConstants.AiSpeed, Math.Max(GameConstants.AiSpeed, targetY - Y));
    }
}

