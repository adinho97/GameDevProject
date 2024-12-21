using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Managers
{
    public class UIManager
    {
        private Texture2D startScreenTexture;
        private bool isStartScreenActive;

        public UIManager()
        {
            isStartScreenActive = true; // Initially show the start screen
        }


        public void LoadContent(ContentManager content)
        {
            startScreenTexture = content.Load<Texture2D>("startScreen"); 
        }


        public void Update(GameTime gameTime)
        {
            if (isStartScreenActive && Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                // Transition to the gameplay screen
                isStartScreenActive = false;
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            if (isStartScreenActive)
            {
                spriteBatch.Draw(startScreenTexture,
                new Rectangle(0, 0, spriteBatch.GraphicsDevice.Viewport.Width, spriteBatch.GraphicsDevice.Viewport.Height),
                Color.White);
            }
        }

        public bool IsStartScreenActive() => isStartScreenActive;
    }
}
