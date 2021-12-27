using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boing
{
    public static class MathHelper
    {
        public static (int x, int y) Normalised (int x, int y)
        {
            var vector = new Vector2(x, y);
            vector.Normalize();

            return ((int)vector.X, (int)vector.Y);
        }
    }
}
