using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boing
{
    internal class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch? _spriteBatch;
        private SpriteBatch spriteBatch => _spriteBatch ?? throw new Exception("Spritebatch not configured");
        public Menu menu;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);            
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            menu = new Menu(this);
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = GameConstants.ScreenWidth;
            graphics.PreferredBackBufferHeight = GameConstants.ScreenHeight;
            graphics.ApplyChanges();

            GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            base.Initialize();

            var theme = Content.Load<Song>("theme");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.3f;
            MediaPlayer.Play(theme);
        }

        protected override void Update(GameTime gameTime)
        {
            menu.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SkyBlue);

            spriteBatch.Begin();
            menu.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
