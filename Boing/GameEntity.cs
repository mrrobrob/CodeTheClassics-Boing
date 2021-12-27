using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boing
{
    internal class GameEntity
    {
        public string Image = "";
        public int X { get; set; }
        public int Y { get; set; }

        public virtual void Update()
        {

        }

        public virtual void Draw()
        {

        }

    }
}
