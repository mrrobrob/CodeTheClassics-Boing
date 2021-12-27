using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boing
{
    internal class Impact : GameEntity
    {
        public int Timer = 0;

        public Impact(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override void Draw()
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            Timer++;
        }
    }
}
