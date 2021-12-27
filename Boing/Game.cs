using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boing
{
    internal static class Game
    {
        public static List<Bat> Bats = new();
        public static List<Impact> Impacts = new();
        public static int AiOffSet = 0;

        public static void PlaySound(string name, int speed) { }
    }
}
