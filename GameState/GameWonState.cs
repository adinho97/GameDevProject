using GameDevProject.ContentLoading;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.GameState
{
    public class GameWonState : GameState
    {
        private Texture2D gameWonScreen;
        private Song _gameWonScreenSong;

        public GameWonState(GameStateManager gameStateManager) : base(gameStateManager)
        {

        }

        public override void Enter()
        {
            gameWonScreen = ContentLoader.Instance.LoadTexture("gameWonScreen");
            _gameWonScreenSong = ContentLoader.Instance.LoadSong("gameWonTrack");

            // Play the start screen track
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.1f;
            MediaPlayer.Play(_gameWonScreenSong);
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
            spriteBatch.Draw(gameWonScreen, new Rectangle(0, 0, spriteBatch.GraphicsDevice.Viewport.Width, spriteBatch.GraphicsDevice.Viewport.Height), Color.White);
            spriteBatch.End();
        }
    }
}

