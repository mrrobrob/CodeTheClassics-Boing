using Boing.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boing
{
    internal class GameStuff
    {
        public ContentManager ContentManager;
        public Ball Ball;
        public List<Bat> Bats = new();
        public List<Impact> Impacts = new();
        public int AiOffSet = 0;

        public void PlaySound(string name, int count) {
            var index = Random.Shared.Next(0, count);

            var sound = ContentManager.Load<SoundEffect>($"{name}{index}");
            sound.Play();
        }

        public GameStuff(ContentManager contentManager, string player1Controls, string player2Controls)
        {
            ContentManager = contentManager;

            var controlFactory = new ControlFactory();

            Bats.Add(new Bat(this, 0, controlFactory.Create(this, player1Controls)));
            Bats.Add(new Bat(this, 1, controlFactory.Create(this, player2Controls)));
            Ball = new Ball(this, -1);
        }

        public List<GameEntity> GetAllGameEntities()
        {
            var result = new List<GameEntity>();
            result.AddRange(Bats);
            result.Add(Ball);
            result.AddRange(Impacts);

            return result;
        }

        public void Update()
        {
            foreach (var gameEntity in GetAllGameEntities())
            {
                gameEntity.Update();
            }

            Impacts.RemoveAll(e => e.Timer >= 10);

            if (Ball.Out())
            {
                var scoringPlayer = Ball.Position.X < GameConstants.HalfWidth ? 1 : 0;
                var losingPlayer = 1 - scoringPlayer;

                if (Bats[losingPlayer].Timer < 0)
                {
                    Bats[scoringPlayer].Score++;
                    PlaySound("score_goal", 1);
                    Bats[losingPlayer].Timer = 20;
                }
                else if (Bats[losingPlayer].Timer == 0)
                {
                    var direction = losingPlayer == 0 ? -1 : 1;
                    Ball = new Ball(this, direction);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var tableTexture = ContentManager.Load<Texture2D>(@"table");
            spriteBatch.Draw(tableTexture, new Vector2(0, 0), Color.White);

            if (Ball.Out())
            {
                foreach (var bat in Bats.Where(e => e.Timer > 0))
                {
                    var batTexture = ContentManager.Load<Texture2D>($"effect{bat.playerNo}");
                    spriteBatch.Draw(batTexture, new Vector2(0, 0), Color.White);
                }
            }

            foreach (var gameEntity in GetAllGameEntities())
            {
                gameEntity.Draw(spriteBatch);
            }
        }
    }
}
