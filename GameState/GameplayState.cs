using GameDevProject.Armament;
using GameDevProject.Characters;
using GameDevProject.Characters.Enemy;
using GameDevProject.Collisions;
using GameDevProject.ContentLoading;
using GameDevProject.Input;
using GameDevProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Color = Microsoft.Xna.Framework.Color;

namespace GameDevProject.GameState
{
    public class GameplayState : GameState
    {
        private Hero hero;
        private EnemyManager enemyManager;
        private ProjectileManager projectileManager;
        private CollisionManager collisionManager;

        private BorderCollision borderCollision;

        private Texture2D heroTexture;
        private Texture2D backgroundTexture;
        private Texture2D projectileTexture;

        private SpriteFont spriteFont;

        private Texture2D debugTexture;

        private Song inGameSong;

        public GameplayState(GameStateManager gameStateManager) : base(gameStateManager)
        {
        }

        public override void Enter()
        {
            heroTexture = ContentLoader.Instance.LoadTexture("tinyIchigo");
            backgroundTexture = ContentLoader.Instance.LoadTexture("backgroundSand");
            projectileTexture = ContentLoader.Instance.LoadTexture("SinglehollowCero");

            collisionManager = new CollisionManager();

            debugTexture = new Texture2D(gameStateManager.Game.GraphicsDevice, 1, 1);
            debugTexture.SetData(new[] { Color.White });

            spriteFont = ContentLoader.Instance.Font;

            var border = new Rectangle(0, 0, gameStateManager.Game.GraphicsDevice.Viewport.Width,
               gameStateManager.Game.GraphicsDevice.Viewport.Height);

            borderCollision = new BorderCollision(border);

            projectileManager = new ProjectileManager();
            hero = new Hero(heroTexture, projectileManager, projectileTexture, debugTexture, new KeyboardReader());
            enemyManager = new EnemyManager(
                new EnemyFactory(gameStateManager.Game.GraphicsDevice, projectileManager),
                hero,
                debugTexture,
                gameStateManager.Game.GraphicsDevice);
            enemyManager.ResetScore();


            // Load the in game track
            inGameSong = ContentLoader.Instance.LoadSong("inGameTrack");
            // Play it
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.05f;
            MediaPlayer.Play(inGameSong);
        }

        public override void Exit()
        {
            // Stop the in game track
            MediaPlayer.Stop();
        }

        public override void Update(GameTime gameTime)
        {
            // If the hero dies, transition to the game over screen
            if (!hero.Health.IsAlive)
            {
                gameStateManager.SetState(new GameOverState(gameStateManager));
                return;
            }

            //game won transition
            if (enemyManager.Score >= 200)
            {
                gameStateManager.SetState(new GameWonState(gameStateManager));
                return;
            }

            hero.Update(gameTime);

            collisionManager.HandleCollisions(hero,
                 enemyManager.GetActiveEnemies().Cast<ICollidable>().ToList());

            borderCollision.Constrain(hero);

            // Update the projectiles
            projectileManager.Update(gameTime,
                enemyManager.GetActiveEnemies().Cast<ICollidable>().ToList(),
                hero);

            enemyManager.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            // Draw the background tetxture
            spriteBatch.Draw(backgroundTexture,
               new Rectangle(0, 0, spriteBatch.GraphicsDevice.Viewport.Width, spriteBatch.GraphicsDevice.Viewport.Height),
               Color.White);

            // Draw the hero
            hero.Draw(spriteBatch);

            // Draw the projectiles
            projectileManager.Draw(spriteBatch);

            // Draw the enemies
            enemyManager.Draw(spriteBatch);

            // Draw the score
            spriteBatch.DrawString(spriteFont, "SCORE: " + enemyManager.Score, new Vector2(20, 20), Color.Black);

            spriteBatch.End();
        }
    }
}
