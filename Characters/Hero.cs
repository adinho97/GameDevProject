using GameDevProject.Animations;
using GameDevProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace GameDevProject.Characters
{

    public class Hero : IGameObject, ICollidable
    {
        Texture2D heroTexture;
        Animation animation;
        private Vector2 position;
        private Vector2 speed;
        private Vector2 acceleration;
        IInputReader inputReader;

        private string currentDirection = "right"; // Default direction set to "right"

        public Vector2 Position 
        {
            get { return position; }
            set { position = value; }
        }

        public Hero(Texture2D texture, IInputReader reader)
        {
            heroTexture = texture;
            animation = new Animation();

            // Assuming 32x32 is the size of each frame, map the correct frames from the sprite sheet
            animation.AddFrame("down", new AnimationFrame(new Rectangle(0, 0, 32, 32))); // down
            animation.AddFrame("down", new AnimationFrame(new Rectangle(32, 0, 32, 32)));
            animation.AddFrame("down", new AnimationFrame(new Rectangle(64, 0, 32, 32)));

            animation.AddFrame("up", new AnimationFrame(new Rectangle(0, 32, 32, 32))); // up
            animation.AddFrame("up", new AnimationFrame(new Rectangle(32, 32, 32, 32)));
            animation.AddFrame("up", new AnimationFrame(new Rectangle(64, 32, 32, 32)));

            animation.AddFrame("left", new AnimationFrame(new Rectangle(0, 64, 32, 32))); // left
            animation.AddFrame("left", new AnimationFrame(new Rectangle(32, 64, 32, 32)));
            animation.AddFrame("left", new AnimationFrame(new Rectangle(64, 64, 32, 32)));

            animation.AddFrame("right", new AnimationFrame(new Rectangle(0, 96, 32, 32))); // right
            animation.AddFrame("right", new AnimationFrame(new Rectangle(32, 96, 32, 32)));
            animation.AddFrame("right", new AnimationFrame(new Rectangle(64, 96, 32, 32)));

            position = new Vector2(10, 10);
            speed = new Vector2(1, 1);
            acceleration = new Vector2(0.1f, 0.1f);

            this.inputReader = reader;
        }

        public void Update(GameTime gameTime)
        {
            // Get input direction and string
            var (direction, directionString) = inputReader.ReadInput();

            // If there is movement, update direction and animation
            if (direction != Vector2.Zero)
            {
                currentDirection = directionString;  // Update the direction when moving
                animation.SetDirection(directionString); // Set animation to the correct direction
                position += direction * 4; // Adjust movement speed as needed
            }
            else
            {
                // When no keys are pressed, continue with the last direction
                animation.SetDirection(currentDirection);  // Stay in the last direction
            }

            animation.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                heroTexture,
                position,
                animation.CurrentFrame.SourceRectangle, // Use the current animation frame
                Color.White,
                0f,                    // No rotation
                Vector2.Zero,          // Origin at the top-left
                2.0f,                  // Scale (adjust as needed)
                SpriteEffects.None,    // No flipping
                0f                     // Layer depth
            );
        }

        public Rectangle GetBorder()
        {
            return new Rectangle(
            (int)position.X,
            (int)position.Y,
            animation.CurrentFrame.SourceRectangle.Width * 2, // Account for scaling
            animation.CurrentFrame.SourceRectangle.Height * 2
        );
        }

        public void setBorder(Rectangle border)
        {
            position = new Vector2(border.X, border.Y);
        }
    }

}
