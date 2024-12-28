using GameDevProject.Animations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Characters.Enemy
{
    public class BabyEnemy : Enemy
    {

        private int screenWidth;
        private int screenHeight;
        private Vector2 velocity;

        public BabyEnemy(Texture2D texture, Vector2 initialPosition, int screenWidth, int screenHeight) : base(texture, initialPosition)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;

            // Determine movement direction based on initial position
            velocity = new Vector2(0, 0);
            if (initialPosition.X < screenWidth / 2)
            {
                velocity += new Vector2(Speed, 0);
            }
            else
            {
                velocity += new Vector2(-Speed, 0);
            }
            if (initialPosition.Y < screenHeight / 2)
            {
                velocity += new Vector2(0, Speed);
            }
            else
            {
                velocity += new Vector2(0, -Speed);
            }
        }
        public override int MaxHealth => 100;

        public override int DamageToDealToPlayer => 20;

        public override int Score => 20;

        protected override float Speed => 60;

        protected override float Scale => 1.5f;

        protected override int FrameWidth => 77;

        protected override int FrameHeight => 53;

        protected override Animation SetupAnimation()
        {
            var animation = new Animation();

            animation.AddFrame("down", new AnimationFrame(new Rectangle(0, 0, 77, 53)));
            animation.AddFrame("down", new AnimationFrame(new Rectangle(77, 0, 77, 53)));
            animation.AddFrame("down", new AnimationFrame(new Rectangle(154, 0, 77, 53)));
            animation.AddFrame("down", new AnimationFrame(new Rectangle(231, 0, 77, 53)));

            return animation;
        }

        protected override void UpdateMovement(GameTime gameTime, Vector2 playerPosition)
        {
            //move around the map
            // Update position based on velocity
            Position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Check bounds and reverse direction if needed
            if (velocity.X != 0)
            {
                if (Position.X <= 0 || Position.X + FrameWidth * Scale >= screenWidth)
                {
                    velocity.X = -velocity.X; // Reverse horizontal direction
                }
            }
            if (velocity.Y != 0)
            {
                if (Position.Y <= 0 || Position.Y + FrameHeight * Scale >= screenHeight)
                {
                    velocity.Y = -velocity.Y; // Reverse vertical direction
                }
            }
        }
    }
}
