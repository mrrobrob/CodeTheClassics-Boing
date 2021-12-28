using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        GameStuff gameStuff;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);            
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            gameStuff = new GameStuff(Content, p1Controls, p2Controls);
        }

        int p1Controls()
        {
            var move = 0;
            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.Z))
            {
                move = GameConstants.PlayerSpeed;
            }
            else if (kstate.IsKeyDown(Keys.A))
            {
                move = -GameConstants.PlayerSpeed;
            }

            return move;
        }

        int p2Controls()
        {
            var move = 0;
            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.M))
            {
                move = GameConstants.PlayerSpeed;
            }
            else if (kstate.IsKeyDown(Keys.K))
            {
                move = -GameConstants.PlayerSpeed;
            }

            return move;
        }


        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = GameConstants.ScreenWidth;
            graphics.PreferredBackBufferHeight = GameConstants.ScreenHeight;
            graphics.ApplyChanges();

            GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            gameStuff.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SkyBlue);

            spriteBatch.Begin();
            gameStuff.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
