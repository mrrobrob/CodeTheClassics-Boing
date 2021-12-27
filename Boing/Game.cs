using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boing
{
    internal class Game
    {
        public static Ball Ball = null;
        public static List<Bat> Bats = new();
        public static List<Impact> Impacts = new();
        public static int AiOffSet = 0;

        public static void PlaySound(string name, int speed) { }

        public Game(Func<int> player1Controls, Func<int>? player2Controls)
        {
            Bats.Add(new Bat(0, player1Controls));
            Bats.Add(new Bat(1, player2Controls));
            Ball = new Ball(-1);
        }

        public void Update()
        {
            foreach (var bat in Bats)
            {
                bat.Update();
            }

            Ball.Update();

            foreach (var impact in Impacts)
            {
                impact.Update();
            }

            Impacts.RemoveAll(e => e.Timer >= 10);

            if (Ball.Out())
            {
                var scoringPlayer = Ball.X < GameConstants.HalfWidth ? 1 : 0;
                var losingPlayer = 1 - scoringPlayer;

                if (Bats[losingPlayer].Timer < 0)
                {
                    Bats[scoringPlayer].Score++;
                    Game.PlaySound("score_goal", 1);
                    Bats[losingPlayer].Timer = 20;
                }
                else if (Bats[losingPlayer].Timer == 0)
                {
                    var direction = losingPlayer == 0 ? -1 : 1;
                    Ball = new Ball(direction);
                }
            }
        }

        public void Draw()
        {

        }
    }
}
