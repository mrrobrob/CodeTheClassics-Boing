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
    internal class Menu
    {
        enum State { MENU, PLAY, GAMEOVER }

        int numPlayers = 1;
        bool spaceDown = false;
        State state = State.MENU;
        private readonly Game1 game;
        GameStuff gameStuff;

        public Menu(Game1 game)
        {
            this.game = game;
            gameStuff = new GameStuff(game.Content, "ai", "ai");
        }

        public void Update()
        {
            var spacePressed = false;
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Space) && spaceDown == false)
            {
                spacePressed = true;
            }

            spaceDown = keyboardState.IsKeyDown(Keys.Space);

            if (state == State.MENU)
            {
                if (spacePressed)
                {
                    state = State.PLAY;
                    gameStuff = new GameStuff(game.Content, "p1", numPlayers == 2 ? "p2" : "ai");
                }
                else
                {
                    if (numPlayers == 2 && keyboardState.IsKeyDown(Keys.Up))
                    {
                        numPlayers = 1;
                        gameStuff.PlaySound("Up", 0);
                    }
                    else if (numPlayers == 1 && keyboardState.IsKeyDown(Keys.Down))
                    {
                        numPlayers = 2;
                        gameStuff.PlaySound("Down", 0);
                    }
                }

                gameStuff.Update();

            }
            else if (state == State.PLAY)
            {
                if (gameStuff.Bats.Any(e => e.Score > 9))
                {
                    state = State.GAMEOVER;
                }
                else
                {
                    gameStuff.Update();
                }

            }
            else if (state == State.GAMEOVER)
            {
                if (spacePressed)
                {
                    state = State.MENU;
                    numPlayers = 1;
                    gameStuff = new GameStuff(game.Content, "ai", "ai");
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            gameStuff.Draw(spriteBatch);

            if (state == State.MENU)
            {
                var menuImage = game.Content.Load<Texture2D>($"menu{numPlayers - 1}");
                spriteBatch.Draw(menuImage, new Vector2(0, 0), Color.White);
            }
            else if (state == State.GAMEOVER)
            {
                var gameOverImage = game.Content.Load<Texture2D>("over");
                spriteBatch.Draw(gameOverImage, new Vector2(0, 0), Color.White);
            }

        }
    }
}
