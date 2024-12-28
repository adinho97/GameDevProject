using GameDevProject.Animations;
using GameDevProject.Armament;
using GameDevProject.Health;
using GameDevProject.Input;
using GameDevProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
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

        private Texture2D projectileTexture;
        private ProjectileManager projectileManager;

        private Texture2D _debugTexture;
        public IHealth Health { get; private set; }

        //natural movement/feel w speed up
        private Vector2 velocity = Vector2.Zero;
        private const float MaxSpeed = 3.0f;
        private const float Acceleration = 0.25f;
        private const float Deceleration = 0.4f;

        private bool shouldFlicker = false; 
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Hero(Texture2D texture,ProjectileManager projectileManager, Texture2D projectileTexture, Texture2D debugTexture, IInputReader reader)
        {
            heroTexture = texture;
            animation = new Animation();

            this.projectileTexture = projectileTexture;
            _debugTexture = debugTexture;
            shouldFlicker = false;

            this.projectileManager = projectileManager;

            Health = new HealthManager(100, 1.0f);

            // Add animation frames for all directions
            // Down (first column)
            animation.AddFrame("up", new AnimationFrame(new Rectangle(0, 0, 32, 32)));
            animation.AddFrame("up", new AnimationFrame(new Rectangle(0, 32, 32, 32)));
            animation.AddFrame("up", new AnimationFrame(new Rectangle(0, 64, 32, 32)));
            animation.AddFrame("up", new AnimationFrame(new Rectangle(0, 96, 32, 32)));

            // Up (second column)
            animation.AddFrame("right", new AnimationFrame(new Rectangle(32, 0, 32, 32)));
            animation.AddFrame("right", new AnimationFrame(new Rectangle(32, 32, 32, 32)));
            animation.AddFrame("right", new AnimationFrame(new Rectangle(32, 64, 32, 32)));
            animation.AddFrame("right", new AnimationFrame(new Rectangle(32, 96, 32, 32)));

            // Left (third column)
            animation.AddFrame("down", new AnimationFrame(new Rectangle(64, 0, 32, 32)));
            animation.AddFrame("down", new AnimationFrame(new Rectangle(64, 32, 32, 32)));
            animation.AddFrame("down", new AnimationFrame(new Rectangle(64, 64, 32, 32)));
            animation.AddFrame("down", new AnimationFrame(new Rectangle(64, 96, 32, 32)));

            // Right (fourth column)
            animation.AddFrame("left", new AnimationFrame(new Rectangle(96, 0, 32, 32)));
            animation.AddFrame("left", new AnimationFrame(new Rectangle(96, 32, 32, 32)));
            animation.AddFrame("left", new AnimationFrame(new Rectangle(96, 64, 32, 32)));
            animation.AddFrame("left", new AnimationFrame(new Rectangle(96, 96, 32, 32)));

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
           public void SetBorder(Rectangle border)
        {
            position = new Vector2(border.X, border.Y);
        }

        public void Update(GameTime gameTime)
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

            //handle flickering when invinc
            if (Health.IsInvincible)
            {
                float flickerInterval = 0.1f;
                float totalSeconds = (float)gameTime.TotalGameTime.TotalSeconds;

                //toggle based on interval
                shouldFlicker = (int)(totalSeconds / flickerInterval) % 2 == 0 ;
            }
            else
            {
                shouldFlicker = false;
            }

            // Get input direction and direction string
            var (direction, directionString) = inputReader.ReadInput();

            // Move the character if there's input
            if (direction != Vector2.Zero)
            {
                if (direction.X == 0) velocity.X = 0;
                if (direction.Y == 0) velocity.Y = 0;

                // Accelerate in the input direction
                velocity += direction * Acceleration;

                // Clamp the velocity to the maximum speed
                if (velocity.Length() > MaxSpeed)
                {
                    velocity = Vector2.Normalize(velocity) * MaxSpeed;
                }

                currentDirection = directionString;

                // Update the animation based on the current direction
                animation.SetDirection(currentDirection);
                animation.Update(gameTime);
            }
            else
            {
                // Decelerate gradually when no input is provided
                if (velocity.Length() > Deceleration)
                {
                    velocity -= Vector2.Normalize(velocity) * Deceleration;
                }
                else
                {
                    velocity = Vector2.Zero; // Stop completely when slow enough
                }
            }

            // Update position with velocity
            Vector2 newPosition = position + velocity;

            // Temporarily update position to calculate border for collision
            Rectangle newBorder = new Rectangle((int)newPosition.X, (int)newPosition.Y, GetBorder().Width, GetBorder().Height);
            SetBorder(newBorder); // Temporarily update the border to check collision


            // Handle shooting mechanics
            var leftMouseButtonPressed = Mouse.GetState().LeftButton == ButtonState.Pressed;

            if (leftMouseButtonPressed && !leftClickLastFrame && !isOnCooldown)
            {
                Shoot(projectileManager, projectileTexture);
            }

            // Update the previous frame state
            leftClickLastFrame = leftMouseButtonPressed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                heroTexture,
                position,
                animation.CurrentFrame.SourceRectangle,
                shouldFlicker? Color.Black : Color.White, // Use the current animation frame
                0f, // No rotation
                Vector2.Zero, // Origin at the top-left
                2f, // Scale (adjust if needed)
                SpriteEffects.None,
                0f // Layer depth
            );

            // Draw the health bar
            DrawHealthBar(spriteBatch, _debugTexture);
        }

        private void DrawHealthBar(SpriteBatch spriteBatch, Texture2D debugTexture)
        {
            var border = GetBorder();
            int healthBarWidth = border.Width + 25;
            int healthBarHeight = 5;
            int healthBarY = border.Y - healthBarHeight - 2;

            // Background 
            spriteBatch.Draw(debugTexture,
                new Rectangle(border.X, healthBarY, healthBarWidth, healthBarHeight),
                Color.White);

            // Health (green)
            int currentHealthWidth = (int)((float)Health.CurrentHealth / Health.MaxHealth * healthBarWidth);
            spriteBatch.Draw(debugTexture,
                new Rectangle(border.X, healthBarY, currentHealthWidth, healthBarHeight),
                Color.Green);
        }

        public void Shoot(ProjectileManager projectileManager, Texture2D projectileTexture)
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
            projectileManager.Add(newProjectile);

            // Activate cooldown
            isOnCooldown = true;
            cooldownTimer = 250f; // Cooldown in milliseconds
        }
    }
}
