using GameDevProject.ContentLoading;
using GameDevProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.GameState
{
    public class GameOverState : GameState
    {

        private Texture2D gameOverScreen;
        private Song _gameOverScreenSong;

        public GameOverState(GameStateManager gameStateManager) :base(gameStateManager)
        {
          
        }

        public override void Enter()
        {
            gameOverScreen = ContentLoader.Instance.LoadTexture("gameOver");
            _gameOverScreenSong= ContentLoader.Instance.LoadSong("gameoverScreenTrack");

            // Play the start screen track
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.1f;
            MediaPlayer.Play(_gameOverScreenSong);
        }
        public override void Exit()
        {
            // Stop the start screen track
            MediaPlayer.Stop();
        }


        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                // Restart the game (or transition back to StartScreenState)
                gameStateManager.SetState(new GameplayState(gameStateManager));
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(gameOverScreen, new Rectangle(0, 0, spriteBatch.GraphicsDevice.Viewport.Width, spriteBatch.GraphicsDevice.Viewport.Height), Color.White);
            spriteBatch.End();
        }
    }
}
