using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boing.Controls
{
    internal class AiControl : IControl
    {
        private readonly GameStuff game;

        public AiControl(GameStuff game)
        {
            this.game = game;
        }

        public int Move(Bat bat)
        {
            var xDistance = Math.Abs(game.Ball.Position.X - bat.Position.X);
            var targetY1 = GameConstants.HalfHeight;
            var targetY2 = game.Ball.Position.Y + game.AiOffSet;
            var weight1 = Math.Min(1, xDistance / GameConstants.HalfWidth);
            var weight2 = 1 - weight1;
            var targetY = (weight1 * targetY1) + (weight2 * targetY2);
            return (int)Math.Min(GameConstants.AiSpeed, Math.Max(-GameConstants.AiSpeed, targetY - bat.Position.Y));
        }
    }
}
