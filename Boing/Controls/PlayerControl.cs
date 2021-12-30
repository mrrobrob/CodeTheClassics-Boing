using Microsoft.Xna.Framework.Input;

namespace Boing.Controls
{
    internal class PlayerControl : IControl
    {
        private readonly Keys upKey;
        private readonly Keys downKey;

        public PlayerControl(Keys upKey, Keys downKey)
        {
            this.upKey = upKey;
            this.downKey = downKey;
        }

        public int Move(Bat bat)
        {
            var move = 0;
            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(downKey))
            {
                move = GameConstants.PlayerSpeed;
            }
            else if (kstate.IsKeyDown(upKey))
            {
                move = -GameConstants.PlayerSpeed;
            }

            return move;
        }

    }
}