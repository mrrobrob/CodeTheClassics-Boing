using Boing.Controls;
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
    private readonly IControl controls;
    private int score = 0;
    public int Score = 0;
    int frame = 0;

    public Bat(GameStuff game, int playerNo, IControl controls)
    {
        Position = new Vector2(playerNo == 0 ? 40 : 760, GameConstants.HalfHeight);
        this.game = game;
        this.playerNo = playerNo;
        this.controls = controls;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        var texture = game.ContentManager.Load<Texture2D>($"bat{playerNo}{frame}");
        spriteBatch.Draw(texture, Position - new Vector2(80,80), Color.White);
    }

    public override void Update()
    {
        Timer--;
        var yMovement = controls.Move(this);

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
    }


}

