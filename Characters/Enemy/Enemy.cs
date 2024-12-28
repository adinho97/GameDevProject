using GameDevProject.Animations;
using GameDevProject.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Characters.Enemy
{
    public class Enemy : IGameObject, ICollidable
    {

        private Texture2D enemyTexture;
        private Vector2 position;
        private Rectangle currentFrame;
        private float speed = 60f;
        private SpriteEffects spriteEffects; // Added for flipping the sprite

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public IHealth Health { get; private set; }

        public Enemy(Texture2D texture, Vector2 initialPosition)
        {
            enemyTexture = texture;
            position = initialPosition;
            currentFrame = new Rectangle(0, 0, 92, 90);
            spriteEffects = SpriteEffects.None;
        }


        //serves as hitbox, reduces to scale 0.5
        public Rectangle GetBorder()
        {
            int newWidth = currentFrame.Width / 2; // Half the original width
            int newHeight = currentFrame.Height / 2; // Half the original height

            return new Rectangle(
                (int)(position.X + (currentFrame.Width - newWidth) / 2), // Center horizontally
                (int)(position.Y + (currentFrame.Height - newHeight) / 2), // Center vertically
                newWidth,
                newHeight
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

        public void Draw(SpriteBatch spriteBatch, Texture2D debugTexture)
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

            // Teken de hitbox met een rode rechthoek
            var border = GetBorder();
            spriteBatch.Draw(debugTexture, new Rectangle(border.X, border.Y, border.Width, 2), Color.Red); // Bovenrand
            spriteBatch.Draw(debugTexture, new Rectangle(border.X, border.Y + border.Height - 2, border.Width, 2), Color.Red); // Onderkant
            spriteBatch.Draw(debugTexture, new Rectangle(border.X, border.Y, 2, border.Height), Color.Red); // Linkerkant
            spriteBatch.Draw(debugTexture, new Rectangle(border.X + border.Width - 2, border.Y, 2, border.Height), Color.Red); // Rechterkant
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }

}
