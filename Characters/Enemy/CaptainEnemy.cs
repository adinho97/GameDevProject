using GameDevProject.Animations;
using GameDevProject.Armament;
using GameDevProject.ContentLoading;
using GameDevProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Characters.Enemy
{
    public class CaptainEnemy : Enemy, IShooter
    {
        public override int MaxHealth => 200;

        public override int DamageToDealToPlayer => 20;

        public override int Score => 50;
        protected override float Speed => 110;

        protected override float Scale => 1.5f;

        protected override int FrameWidth => 40;

        protected override int FrameHeight => 64;

        private string directionString = "down";

        //note to self: copy from hero
        private bool isOnCooldown;
        private float cooldownTimer; // Cooldown duration in milliseconds

        private ProjectileManager projectileManager;
        private Texture2D projectileTexture;

        private SoundEffect shootSound;

        private float dashCooldown = 3000f; // Cooldown in milliseconden voor dash
        private float dashDuration = 500f; // Dash duurtijd in milliseconden
        private float dashSpeedMultiplier = 3f; // Hoeveel sneller tijdens een dash
        private float dashCooldownTimer = 0f; // Aparte cooldown voor dash
        private float dashTimer = 0f;
        private bool isDashing = false;

        public CaptainEnemy(Texture2D texture, ProjectileManager projectileManager, Vector2 initialPosition) : base(texture, initialPosition)
        {
            // Initialize shooting mechanics
            isOnCooldown = false;
            cooldownTimer = 0f;

            this.projectileManager = projectileManager;
            //projectileTexture = ContentLoader.Instance.LoadTexture("SingleHollowCero"); //change kind of bullet later maybe to blast
            projectileTexture = ContentLoader.Instance.LoadTexture("captainBlast");
             shootSound = ContentLoader.Instance.LoadSoundEffect("ceroBlast");
        }

        protected override Animation SetupAnimation()
        {
            // false, because animation does the flip
            ShouldFlip = false;

            var animation = new Animation();

            // Down (first column)
            animation.AddFrame("down", new AnimationFrame(new Rectangle(0, 0, 48, 64)));
            animation.AddFrame("down", new AnimationFrame(new Rectangle(48, 0, 48, 64)));
            animation.AddFrame("down", new AnimationFrame(new Rectangle(96, 0, 48, 64)));
            animation.AddFrame("down", new AnimationFrame(new Rectangle(144, 0, 48, 64)));

            // Left (second column)
            animation.AddFrame("left", new AnimationFrame(new Rectangle(0, 64, 48, 64)));
            animation.AddFrame("left", new AnimationFrame(new Rectangle(48, 64, 48, 64)));
            animation.AddFrame("left", new AnimationFrame(new Rectangle(96, 64, 48, 64)));
            animation.AddFrame("left", new AnimationFrame(new Rectangle(144, 64, 48, 64)));

            // Right (third column)
            animation.AddFrame("right", new AnimationFrame(new Rectangle(0, 128, 48, 64)));
            animation.AddFrame("right", new AnimationFrame(new Rectangle(48, 128, 48, 64)));
            animation.AddFrame("right", new AnimationFrame(new Rectangle(96, 128, 48, 64)));
            animation.AddFrame("right", new AnimationFrame(new Rectangle(144, 128, 48, 64)));

            // Up (fourth column)
            animation.AddFrame("up", new AnimationFrame(new Rectangle(0, 192, 48, 64)));
            animation.AddFrame("up", new AnimationFrame(new Rectangle(48, 192, 48, 64)));
            animation.AddFrame("up", new AnimationFrame(new Rectangle(96, 192, 48, 64)));
            animation.AddFrame("up", new AnimationFrame(new Rectangle(144, 192, 48, 64)));

            return animation;


        }

        protected override void UpdateMovement(GameTime gameTime, Vector2 playerPosition)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Update cooldown timer voor het schieten (onafhankelijk van dashing)
            if (isOnCooldown)
            {
                cooldownTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (cooldownTimer <= 0f)
                {
                    isOnCooldown = false;
                    cooldownTimer = 0f;
                }
            }
            else
            {
                Shoot(projectileManager, projectileTexture);
            }

            // Bereken richting naar de speler
            Vector2 direction = playerPosition - Position;
            if (direction != Vector2.Zero)
            {
                direction.Normalize();
            }

            if (isDashing)
            {
                // Dash logica
                dashTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (dashTimer <= 0f)
                {
                    // Stop dashing en start cooldown
                    isDashing = false;
                    dashCooldownTimer = dashCooldown;
                }
                else
                {
                    // Beweeg sneller richting de speler
                    Position += direction * Speed * dashSpeedMultiplier * deltaTime;
                }
            }
            else
            {
                // Cooldown logica voor dash
                if (dashCooldownTimer > 0f)
                {
                    dashCooldownTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                }
                else
                {
                    // Start een nieuwe dash
                    isDashing = true;
                    dashTimer = dashDuration;
                }

                // Normale beweging (chase logica)
                Position += direction * Speed * deltaTime;
            }

            // Update animatie richting alleen gebaseerd op normale beweging
            if (!isDashing)
            {
                if (MathF.Abs(direction.X) > MathF.Abs(direction.Y))
                {
                    // Horizontale beweging
                    directionString = direction.X > 0 ? "right" : "left";
                }
                else if (direction.Y != 0)
                {
                    // Verticale beweging
                    directionString = direction.Y > 0 ? "down" : "up";
                }

                Animation.SetDirection(directionString);
            }
        }

        // old update w only chase movement
        /*
        protected override void UpdateMovement(GameTime gameTime, Vector2 playerPosition)
        {
            //Update cooldown timer for shooting
            if (isOnCooldown)
            {
                cooldownTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (cooldownTimer <= 0f)
                {
                    isOnCooldown = false;
                    cooldownTimer = 0f;
                }
            }
            else
            {
                Shoot(projectileManager, projectileTexture);
            }

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 direction = Vector2.Zero;
            // Chase the player => change this to run away later
            direction = playerPosition - Position;
            if (direction != Vector2.Zero)
            {
                direction.Normalize();
            }
            // Update position
            Position += direction * Speed * deltaTime;
            // Set animation direction
            if (MathF.Abs(direction.X) > MathF.Abs(direction.Y))
            {
                // Moving horizontally
                if (direction.X > 0)
                {
                    directionString = "right";
                }
                else
                {
                    directionString = "left";
                }
            }
            else if (direction.Y != 0)
            {
                // Moving vertically
                if (direction.Y > 0)
                {
                    directionString = "down";
                }
                else
                {
                    directionString = "up";
                }
            }

            Animation.SetDirection(directionString);
        }
        */

        // run away logic
        /*
           // Calculate distance to the player RUN AWAY
    Vector2 distanceToPlayer = playerPosition - Position;
    float distanceMagnitude = distanceToPlayer.Length();

    // Behavior logic
    if (distanceMagnitude < 150) // Run away when too close
    {
        direction = Position - playerPosition; // Move away from the player
    }
    else // Chase the player
    {
        direction = playerPosition - Position; // Move toward the player
    }

    if (direction != Vector2.Zero)
    {
        direction.Normalize();
    }
         * */
        public void Shoot(ProjectileManager projectileManager, Texture2D projectileTexture)
        {
            // Prevent firing if cooldown is active
            if (isOnCooldown)
                return;

            Vector2 projectileDirection;

            // Determine shooting direction based on currentDirection
            switch (directionString)
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
            var newProjectile = new CaptainProjectile(projectileTexture, Position, projectileDirection);
            projectileManager.Add(newProjectile);

            // Play shooting sound
            shootSound.Play(0.1f, 0f, 0f); // Volume, pitch, and pan

            // Activate cooldown
            isOnCooldown = true;
            cooldownTimer = 3000f; // Cooldown in milliseconds
        }
    }
}
