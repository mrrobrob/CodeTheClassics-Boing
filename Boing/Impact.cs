using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boing
{
    internal class Impact : GameEntity
    {
        private readonly GameStuff game;
        public int Timer = 0;

        public Impact(GameStuff game, Vector2 position)
        {
            Position = position;
            this.game = game;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var texture = game.ContentManager.Load<Texture2D>($"impact{Timer / 2}");
            spriteBatch.Draw(texture, Position - new Vector2(37.5f, 37.5f), Color.White);
        }

        public override void Update()
        {
            Timer++;
        }
    }
}
