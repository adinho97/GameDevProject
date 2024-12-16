using GameDevProject.Animations;
using GameDevProject.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Characters
{
    public class Enemy : IGameObject, ICollidable
    {

        private Texture2D enemyTexture;
        private Vector2 position;
        private Rectangle currentFrame;
        private float speed = 100f;
        private SpriteEffects spriteEffects; // Added for flipping the sprite

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Enemy(Texture2D texture, Vector2 initialPosition)
        {
            enemyTexture = texture;
            position = initialPosition;
            currentFrame = new Rectangle(0,0, 92, 90);
            spriteEffects = SpriteEffects.None;
        }
   

        public Rectangle GetBorder()
        {
            return new Rectangle(
              (int)position.X,
              (int)position.Y,
              currentFrame.Width,
              currentFrame.Height
          );
        }

        public void setBorder(Rectangle border)
        {
            position = new Vector2(border.X, border.Y);
        }

        public void Update(GameTime gameTime, Vector2 playerPosition)
        {
            Vector2 direction = playerPosition - position;

            if (direction != Vector2.Zero)
                direction.Normalize();

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            position += direction * speed * deltaTime;

            // Flip the sprite based on movement direction
            if (direction.X > 0)
            {
                spriteEffects = SpriteEffects.None; // Facing right
            }
            else if (direction.X < 0)
            {
                spriteEffects = SpriteEffects.FlipHorizontally; // Facing left
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                enemyTexture,
                position,
                currentFrame,
                Color.White,
                0f, // No rotation
                Vector2.Zero, // Origin at the top-left
                0.8f, // Scale (adjust if needed)
                spriteEffects,
                0f // Layer depth
            );
        }

        public void Update(GameTime gameTime)
        {
           
        }
    }

}
