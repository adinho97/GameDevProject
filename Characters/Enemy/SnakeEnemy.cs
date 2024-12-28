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
    public class SnakeEnemy : Enemy
    {
        public SnakeEnemy(Texture2D texture, Vector2 initialPosition) : base(texture, initialPosition)
        {

        }
        public override int MaxHealth => 40;

        public override int DamageToDealToPlayer => 5;

        public override int Score => 10;

        protected override float Speed => 60;

        protected override float Scale => 1.0f;

        protected override int FrameWidth => 92;

        protected override int FrameHeight => 90;

        protected override Animation SetupAnimation()
        {
            var animation = new Animation();

            animation.AddFrame("down", new AnimationFrame(new Rectangle(0, 0, 92, 90)));
            animation.AddFrame("down", new AnimationFrame(new Rectangle(92, 0, 92, 90)));
            animation.AddFrame("down", new AnimationFrame(new Rectangle(184, 0, 92, 90)));

            return animation;
        }

        protected override void UpdateMovement(GameTime gameTime, Vector2 playerPosition)
        {
            // Move towards the player
            Vector2 direction = playerPosition - Position;
            if (direction != Vector2.Zero)
            {
                direction.Normalize();
            }

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += direction * Speed * deltaTime;
        }
    }
}
