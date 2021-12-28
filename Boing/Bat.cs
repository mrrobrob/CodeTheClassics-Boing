using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boing;

internal class Bat : GameEntity
{
    public int Timer = 0;
    private readonly GameStuff game;
    public int playerNo;
    private int score = 0;
    Func<int> moveFunc;
    public int Score = 0;
    int frame = 0;

    public Bat(GameStuff game, int playerNo, Func<int>? moveFunc)
    {
        Position = new Vector2(playerNo == 0 ? 40 : 760, GameConstants.HalfHeight);
        this.game = game;
        this.playerNo = playerNo;

        this.moveFunc = moveFunc ?? AiMoveFunc;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        var texture = game.ContentManager.Load<Texture2D>($"bat{playerNo}{frame}");
        spriteBatch.Draw(texture, Position - new Vector2(80,80), Color.White);
    }

    public override void Update()
    {
        Timer--;
        var yMovement = moveFunc();

        Position = new Vector2(Position.X, Math.Min(400, Math.Max(80, Position.Y + yMovement)));

        frame = 0;

        if (Timer > 0)
        {
            if (game.Ball.Out())
            {
                frame = 2;
            }
            else
            {
                frame = 1;
            }
        }

        Image = $"bat{playerNo}{frame}";
    }

    public int AiMoveFunc()
    {
        var xDistance = Math.Abs(game.Ball.Position.X - Position.X);
        var targetY1 = GameConstants.HalfHeight;
        var targetY2 = game.Ball.Position.Y + game.AiOffSet;
        var weight1 = Math.Min(1, xDistance / GameConstants.HalfWidth);
        var weight2 = 1 - weight1;
        var targetY = (weight1 * targetY1) + (weight2 * targetY2);
        return (int)Math.Min(GameConstants.AiSpeed, Math.Max(-GameConstants.AiSpeed, targetY - Position.Y));
    }
}

