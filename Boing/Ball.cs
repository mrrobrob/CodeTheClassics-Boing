using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boing
{
    internal class Ball : GameEntity
    {
        float dx;
        float dy = 0;
        int speed = 5;
        private GameStuff game;

        public Ball(GameStuff game, int dx)
        {
            this.game = game;
            Position = new(GameConstants.HalfWidth, GameConstants.HalfHeight);

            this.dx = dx;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var texture = game.ContentManager.Load<Texture2D>("ball");
            spriteBatch.Draw(texture, Position - new Vector2(12,12), Color.White);
        }

        public override void Update()
        {
            foreach (int i in Enumerable.Range(0, speed))
            {
                var origX = Position.X;

                Position = new Vector2(Position.X + dx, Position.Y + dy);

                if (Math.Abs(Position.X - GameConstants.HalfWidth) >= 344 && Math.Abs(origX - GameConstants.HalfWidth) < 344)
                {
                    int newDirX;
                    Bat bat;
                    if (Position.X < GameConstants.HalfWidth)
                    {
                        newDirX = 1;
                        bat = game.Bats[0];
                    }
                    else
                    {
                        newDirX = -1;
                        bat = game.Bats[1];
                    }

                    float differenceY = Position.Y - bat.Position.Y;

                    if (differenceY > -64 && differenceY < 64)
                    {
                        dx = -dx;
                        dy += differenceY / 128;
                        dy = Math.Min(Math.Max(dy, -1), 1);
                        (dx, dy) = MathHelper.Normalised(dx, dy);
                        game.Impacts.Add(new Impact(game, new Vector2(Position.X - newDirX * 10, Position.Y)));
                        speed++;
                        game.AiOffSet = Random.Shared.Next(-10, 10);
                        bat.Timer = 10;

                        game.PlaySound("hit", 5);

                        if (speed <= 10)
                        {
                            game.PlaySound("hit_slow", 1);
                        }
                        else if (speed <= 12)
                        {
                            game.PlaySound("hit_medium", 1);
                        }
                        else if (speed <= 16)
                        {
                            game.PlaySound("hit_fast", 1);
                        }
                        else
                        {
                            game.PlaySound("hit_veryfast", 1);
                        }
                    }
                }

                if (Math.Abs(Position.Y - GameConstants.HalfHeight) > 220)
                {
                    dy = -dy;
                    Position = new Vector2(Position.X, Position.Y + dy);

                    game.Impacts.Add(new Impact(game, Position));

                    game.PlaySound("bounce", 5);
                    game.PlaySound("bounce_synth", 1);
                }
            }
        }

        public bool Out()
        {
            return Position.X < 0 || Position.X > GameConstants.ScreenWidth;
        }
    }
}
