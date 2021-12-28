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
        public static (float x, float y) Normalised (float x, float y)
        {
            var vector = new Vector2(x, y);
            vector.Normalize();

            return (vector.X, vector.Y);
        }
    }
}
