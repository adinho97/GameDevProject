using GameDevProject.Interfaces;
using GameDevProject.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.GameState
{
    public class GameOverState : IGameState
    {

        private Texture2D gameOverScreen;
        private ContentManager content;

        public GameOverState(ContentManager content)
        {
            this.content = content;
            LoadContent();
        }

        private void LoadContent()
        {
            gameOverScreen = content.Load<Texture2D>("GameOverScreen");
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                // Restart the game (or transition back to StartScreenState)
                GameStateManager.Instance.SetState(GameStateManager.GameState.StartScreen);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(gameOverScreen, new Rectangle(0, 0, spriteBatch.GraphicsDevice.Viewport.Width, spriteBatch.GraphicsDevice.Viewport.Height), Color.White);
        }
    }
}
