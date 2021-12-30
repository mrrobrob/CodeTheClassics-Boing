using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boing.Controls
{
    internal class ControlFactory
    {
        public IControl Create(GameStuff game, string type)
        {
            switch(type)
            {
                case "ai":
                    return new AiControl(game);
                case "p1":
                    return new PlayerControl(Keys.A, Keys.Z);
                case "p2":
                    return new PlayerControl(Keys.K, Keys.M);
            }

            throw new Exception($"Invalid type - {type}");
        }
    }
}
