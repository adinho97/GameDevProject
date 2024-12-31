using GameDevProject.ContentLoading;
using GameDevProject.Interfaces;
using GameDevProject.Managers;
using Microsoft.Xna.Framework;
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
    public class StartScreenState : GameState
    {

        private Texture2D _startScreenTexture;
        private Song _startScreenSong;

        public StartScreenState(GameStateManager gameStateManager) : base(gameStateManager)
        {
                
        }

        public override void Enter()
        {
            _startScreenTexture = ContentLoader.Instance.LoadTexture("startScreen");
            _startScreenSong = ContentLoader.Instance.LoadSong("startScreenTrack");

            // Play the start screen track
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.1f;
            MediaPlayer.Play(_startScreenSong);
        }

        public override void Exit()
        {
            // Stop the start screen track
            MediaPlayer.Stop();
        }

        public override void Update(GameTime gameTime)
        {
            // If the space bar is clicked we switch to the gameplay state
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                gameStateManager.SetState(new GameplayState(gameStateManager));
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(_startScreenTexture,
            new Rectangle(0, 0, spriteBatch.GraphicsDevice.Viewport.Width, spriteBatch.GraphicsDevice.Viewport.Height),
            Color.White);

            spriteBatch.End();
        }

    }
}
