using GameDevProject.Animations;
using GameDevProject.Armament;
using GameDevProject.Input;
using GameDevProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GameDevProject.Characters
{
    public class Hero : IGameObject, ICollidable, IShooter
    {
        private Texture2D heroTexture;
        private Animation animation;

        private Vector2 position;
        private IInputReader inputReader;

        private string currentDirection = "down"; // Default direction is down

        private bool isOnCooldown;
        private float cooldownTimer; // Cooldown duration in milliseconds
        private bool leftClickLastFrame; // To detect shooting key press transitions


        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Hero(Texture2D texture, IInputReader reader)
        {
            heroTexture = texture;
            animation = new Animation();

            // Add animation frames for all directions (ensure they are properly grouped)

            // Down (first column)
            animation.AddFrame("down", new AnimationFrame(new Rectangle(0, 0, 32, 32)));
            animation.AddFrame("down", new AnimationFrame(new Rectangle(0, 32, 32, 32)));
            animation.AddFrame("down", new AnimationFrame(new Rectangle(0, 64, 32, 32)));
            animation.AddFrame("down", new AnimationFrame(new Rectangle(0, 96, 32, 32)));

            // Up (second column)
            animation.AddFrame("up", new AnimationFrame(new Rectangle(32, 0, 32, 32)));
            animation.AddFrame("up", new AnimationFrame(new Rectangle(32, 32, 32, 32)));
            animation.AddFrame("up", new AnimationFrame(new Rectangle(32, 64, 32, 32)));
            animation.AddFrame("up", new AnimationFrame(new Rectangle(32, 96, 32, 32)));

            // Left (third column)
            animation.AddFrame("left", new AnimationFrame(new Rectangle(64, 0, 32, 32)));
            animation.AddFrame("left", new AnimationFrame(new Rectangle(64, 32, 32, 32)));
            animation.AddFrame("left", new AnimationFrame(new Rectangle(64, 64, 32, 32)));
            animation.AddFrame("left", new AnimationFrame(new Rectangle(64, 96, 32, 32)));

            // Right (fourth column)
            animation.AddFrame("right", new AnimationFrame(new Rectangle(96, 0, 32, 32)));
            animation.AddFrame("right", new AnimationFrame(new Rectangle(96, 32, 32, 32)));
            animation.AddFrame("right", new AnimationFrame(new Rectangle(96, 64, 32, 32)));
            animation.AddFrame("right", new AnimationFrame(new Rectangle(96, 96, 32, 32)));

            position = new Vector2(100, 100); // Initial position
            inputReader = reader; // Input reader for movement

            // Initialize shooting mechanics
            isOnCooldown = false;
            cooldownTimer = 0f;
            leftClickLastFrame = false;

        }

        public Rectangle GetBorder()
        {
            return new Rectangle(
                (int)position.X,
                (int)position.Y,
                animation.CurrentFrame.SourceRectangle.Width,
                animation.CurrentFrame.SourceRectangle.Height
            );
        }
           public void setBorder(Rectangle border)
        {
            position = new Vector2(border.X, border.Y);
        }

        public void Update(GameTime gameTime, List<IProjectile> projectiles, Texture2D projectileTexture)
        {
            // Update cooldown timer for shooting
            if (isOnCooldown)
            {
                cooldownTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (cooldownTimer <= 0f)
                {
                    isOnCooldown = false;
                    cooldownTimer = 0f;
                }
            }

            // Get input direction and direction string
            var (direction, directionString) = inputReader.ReadInput();

            // Move the character if there's input
            if (direction != Vector2.Zero)
            {
                Vector2 newPosition = position + direction * 3f; // Adjust speed as needed

                // Temporarily update position to calculate border for collision
                Rectangle newBorder = new Rectangle((int)newPosition.X, (int)newPosition.Y, GetBorder().Width, GetBorder().Height);
                setBorder(newBorder); // Temporarily update the border to check collision

                currentDirection = directionString;

                // Update the animation based on the current direction
                // We only do this when we have a movement directipon
                animation.SetDirection(currentDirection);
                animation.Update(gameTime);
            }


            // Handle shooting mechanics
            var leftMouseButtonPressed = Mouse.GetState().LeftButton == ButtonState.Pressed;

            if (leftMouseButtonPressed && !leftClickLastFrame && !isOnCooldown)
            {
                Shoot(projectiles, projectileTexture);
            }

            // Update the previous frame state
            leftClickLastFrame = leftMouseButtonPressed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                heroTexture,
                position,
                animation.CurrentFrame.SourceRectangle, // Use the current animation frame
                Color.White,
                0f, // No rotation
                Vector2.Zero, // Origin at the top-left
                2f, // Scale (adjust if needed)
                SpriteEffects.None,
                0f // Layer depth
            );
        }

      

     

        public void Shoot(List<IProjectile> projectiles, Texture2D projectileTexture)
        {
            // Prevent firing if cooldown is active
            if (isOnCooldown)
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

            // Create and add the projectile
            var newProjectile = new Cero(projectileTexture, Position, projectileDirection);
            projectiles.Add(newProjectile);

            // Activate cooldown
            isOnCooldown = true;
            cooldownTimer = 500f; // Cooldown in milliseconds
        }

        public void Update(GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }
    }
}
