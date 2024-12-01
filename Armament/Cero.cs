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
        private float speed = 8f; // Speed of the projectile
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
                Position.X > 800 || Position.Y > 600) // Adjust bounds as needed
            {
                IsActive = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsActive)
            {
                spriteBatch.Draw(texture, Position, Color.White);
            }
        }

        public Rectangle GetBorder()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
        }
    }


}
