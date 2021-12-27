using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boing
{
    internal class Ball : GameEntity
    {
        int dx;
        int dy = 0;
        int speed = 5;

        public Ball(int dx)
        {
            X = GameConstants.HalfWidth;
            Y = GameConstants.HalfHeight;

            this.dx = dx;
        }

        public override void Draw()
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            foreach (int i in Enumerable.Range(0, speed))
            {
                var origX = X;

                X += dx;
                Y += dy;

                if (Math.Abs(X - GameConstants.HalfWidth) >= 344 && Math.Abs(origX - GameConstants.HalfWidth) < 344)
                {
                    int newDirX;
                    Bat bat;
                    if (X < GameConstants.HalfWidth)
                    {
                        newDirX = 1;
                        bat = Game.Bats[0];
                    }
                    else
                    {
                        newDirX = -1;
                        bat = Game.Bats[1];
                    }

                    var differenceY = Y - bat.Y;

                    if (differenceY > -64 && differenceY < 64)
                    {
                        dx = -dx;
                        dy += differenceY / 128;
                        dy = Math.Min(Math.Max(dy, -1), 1);
                        (dx, dy) = MathHelper.Normalised(dx, dy);
                        Game.Impacts.Add(new Impact(X - newDirX * 10, Y));
                        speed++;
                        Game.AiOffSet = Random.Shared.Next(-10, 10);
                        bat.Timer = 10;

                        Game.PlaySound("hit", 5);

                        if (speed <= 10)
                        {
                            Game.PlaySound("hit_slow", 1);
                        }
                        else if (speed <= 12)
                        {
                            Game.PlaySound("hit_medium", 1);
                        }
                        else if (speed <= 16)
                        {
                            Game.PlaySound("hit_fast", 1);
                        }
                        else
                        {
                            Game.PlaySound("hit_veryfast", 1);
                        }
                    }
                }

                if (Math.Abs(Y - GameConstants.HalfHeight) > 220)
                {
                    dy = -dy;
                    Y += dy;

                    Game.Impacts.Add(new Impact(X, Y));

                    Game.PlaySound("bounce", 5);
                    Game.PlaySound("bounce_synth", 1);
                }
            }
        }

        public bool Out()
        {
            return X < 0 || X > GameConstants.ScreenWidth;
        }
    }
}
