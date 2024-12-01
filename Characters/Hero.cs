using GameDevProject.Animations;
using GameDevProject.Armament;
using GameDevProject.Input;
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

    public class Hero : IGameObject, ICollidable, IShooter
    {
        Texture2D heroTexture;
        Animation animation;

        private Vector2 position;
        private Vector2 speed;
        private Vector2 acceleration;
        IInputReader inputReader;

        private float cooldownTimer; // Cooldown in milliseconds
        private bool isOnCooldown;
        private bool spaceKeyPressedLastFrame; // To detect key press transitions

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

            cooldownTimer = 0f;
            isOnCooldown = false;
            spaceKeyPressedLastFrame = false;

            this.inputReader = reader;
        }

        public void Update(GameTime gameTime, List<IProjectile> projectiles, Texture2D projectileTexture)
        {

            // Update cooldown
            if (isOnCooldown)
            {
                cooldownTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (cooldownTimer <= 0f)
                {
                    isOnCooldown = false;
                    cooldownTimer = 0f;
                }
            }
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

            //add handle shooting
            /*
            if (((KeyboardReader)inputReader).IsShooting())
            {
                Shoot(projectiles, projectileTexture);
            }
            */

            // Detect key press for shooting
            var spaceKeyPressed = Keyboard.GetState().IsKeyDown(Keys.Space);

            // Check if space is pressed and not held, and hero is not on cooldown
            if (spaceKeyPressed && !spaceKeyPressedLastFrame && !isOnCooldown)
            {
                Shoot(projectiles, projectileTexture);
            }

            // Update the key press state for the next frame
            spaceKeyPressedLastFrame = spaceKeyPressed;
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

        public void Shoot(List<IProjectile> projectiles, Texture2D projectileTexture)
        {
            // Prevent shooting if already active projectile exists (optional)
            if (projectiles.Count > 0)
                return;

            Vector2 projectileDirection;

            // Determine shooting direction based on currentDirection
            switch (currentDirection)
            {
                case "up":
                    projectileDirection = new Vector2(0, -1);
                    break;
                case "down":
                    projectileDirection = new Vector2(0, 1);
                    break;
                case "left":
                    projectileDirection = new Vector2(-1, 0);
                    break;
                case "right":
                    projectileDirection = new Vector2(1, 0);
                    break;
                default:
                    projectileDirection = Vector2.Zero;
                    break;
            }

            // Create a new Cero projectile and add it to the list
            var newProjectile = new Cero(projectileTexture, Position, projectileDirection);
            projectiles.Add(newProjectile);

            // Set cooldown
            isOnCooldown = true;
            cooldownTimer = 500f; // Cooldown duration in milliseconds (adjust as needed)
        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }

}
