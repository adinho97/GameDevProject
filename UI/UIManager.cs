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

namespace GameDevProject.Managers
{
    public class UIManager
    {
        private Texture2D startScreenTexture;
        private bool isStartScreenActive;

        private Song startScreenMusic;
        private Song inGameMusic;
        private bool isMusicPlaying;
        public UIManager()
        {
            isStartScreenActive = true;
            isMusicPlaying = false;
        }


        public void LoadContent(ContentManager content)
        {
            startScreenTexture = content.Load<Texture2D>("startScreen");
            startScreenMusic = content.Load<Song>("startScreenTrack");
            inGameMusic = content.Load<Song>("inGameTrack");
        }

        private void SwitchToInGameMusic()
        {
            MediaPlayer.Stop();
            MediaPlayer.Volume = 0.1f;
            MediaPlayer.Play(inGameMusic);
            isMusicPlaying = true;
        }

        public void Update(GameTime gameTime)
        {
            if(isStartScreenActive)
            {
                if (!isMusicPlaying)
                {
                    MediaPlayer.IsRepeating = true;
                    MediaPlayer.Volume = 0.1f;
                    MediaPlayer.Play(startScreenMusic);
                    isMusicPlaying = true;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    // Transition to the gameplay screen
                    isStartScreenActive = false;
                    SwitchToInGameMusic();

                }
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
