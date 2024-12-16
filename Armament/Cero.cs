using GameDevProject.Animations;
using GameDevProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Armament
{
    public class Cero : IProjectile
    {
        public Vector2 Position { get; set; }
        public Vector2 Direction { get; set; }
        private readonly Texture2D texture;
        private float speed = 10f; // Speed of the projectile
        public bool IsActive { get; private set; } // Proper setter for activation stat    
        // Becomes inactive when out of bounds


        public Cero(Texture2D texture, Vector2 position, Vector2 direction)
        {
            this.texture = texture;
            Position = position;
            Direction = direction;
            IsActive = true; // Projectile starts as active
        }

        public void Update(GameTime gameTime)
        {
            if (!IsActive) return;

            // Move in the given direction
            Position += Direction * speed;

            // Deactivate when out of bounds
            if (Position.X < 0 || Position.Y < 0 ||
                Position.X > 1920 || Position.Y > 1080) // Adjust bounds as needed
            {
                IsActive = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsActive)
            {
                Vector2 scale = new Vector2(0.5f, 0.5f); 
                spriteBatch.Draw(texture, Position, null, Color.White, -0.2f, Vector2.Zero, scale, SpriteEffects.None, 0f);

            }
        }

        public Rectangle GetBorder()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
        }
    }


}
