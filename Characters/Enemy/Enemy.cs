using GameDevProject.Animations;
using GameDevProject.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDevProject.Health;

namespace GameDevProject.Characters.Enemy
{
    public abstract class Enemy : ICollidable
    {

        protected Texture2D EnemyTexture { get; private set; }
        protected Vector2 Position { get; set; }
        protected Rectangle FirstFrame { get; set; }

        protected bool ShouldFlip { get; set; }
        protected SpriteEffects SpriteEffects { get; set; }
        protected bool ShowDebug { get; set; } = true;
        private Vector2 PreviousPosition;

        // Abstract properties that derived classes must implement
        protected abstract float Speed { get; }
        protected abstract float Scale { get; }
        protected abstract int FrameWidth { get; }
        protected abstract int FrameHeight { get; }
        public abstract int MaxHealth { get; }
        public abstract int DamageToDealToPlayer { get; }
        public abstract int Score { get; }

        public IHealth Health { get; private set; }
        public Animation Animation { get; private set; }

        private bool shouldFlicker;

        //constructor w min req params
        protected Enemy(Texture2D texture, Vector2 initialPosition)
        {
            EnemyTexture = texture;
            Position = initialPosition;
            ShowDebug = false;
            ShouldFlip = true;
            SpriteEffects = SpriteEffects.None;
            Health = new HealthManager(MaxHealth, 0.2f);

            FirstFrame = new Rectangle(0, 0, FrameWidth, FrameHeight);
            shouldFlicker = false;

            Animation = SetupAnimation();
        }

        protected abstract Animation SetupAnimation();

        public virtual Rectangle GetBorder()
        {
            int scaledWidth = (int)(FrameWidth * Scale);
            int scaledHeight = (int)(FrameHeight * Scale);
            return new Rectangle(
                (int)(Position.X + (FrameWidth * Scale - scaledWidth) / 2),
                (int)(Position.Y + (FrameHeight * Scale - scaledHeight) / 2),
                scaledWidth,
                scaledHeight
            );
        }

        public virtual void SetBorder(Rectangle border)
        {
            Position = new Vector2(border.X, border.Y);
        }

        protected abstract void UpdateMovement(GameTime gameTime, Vector2 playerPosition);

        public void Update(GameTime gameTime, Hero hero)
        {
            if (Health.IsAlive)
            {
                PreviousPosition = Position;
                if(Animation != null)
                {
                    Animation.Update(gameTime);
                }
                UpdateMovement(gameTime, hero.Position);

                //handle flickering when invinc
                if (Health.IsInvincible)
                {
                    float flickerInterval = 0.1f; // Flicker every 0.1 seconds
                    float totalSeconds = (float)gameTime.TotalGameTime.TotalSeconds;

                    // Toggle visibility based on the interval
                    shouldFlicker = (int)(totalSeconds / flickerInterval) % 2 == 0;
                }
                else
                {
                    shouldFlicker = false;
                }

                if (GetBorder().Intersects(hero.GetBorder()))
                {
                    hero.Health.TakeDamage(DamageToDealToPlayer);
                }

                if (ShouldFlip)
                {
                    UpdateSpriteEffects();
                }
            }
        }

        protected virtual void UpdateSpriteEffects()
        {
            Vector2 movement = Position - PreviousPosition;
            if(movement.X > 0)
            {
                SpriteEffects = SpriteEffects.None;
            }
            else if (movement.X < 0)
            {
                SpriteEffects = SpriteEffects.FlipHorizontally;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, Texture2D debugTexture)
        {
            if (Health.IsAlive)
            {
                //draw enemy
                spriteBatch.Draw(
                    EnemyTexture,
                    Position,
                    Animation == null ? FirstFrame : Animation.CurrentFrame.SourceRectangle,
                    shouldFlicker ? Color.Black : Color.White,
                    0f,
                    Vector2.Zero,
                    Scale,
                    SpriteEffects,
                    0f
                    );

                //draw hp bar
                DrawHealthBar(spriteBatch, debugTexture);

                if (ShowDebug)
                {
                    var border = GetBorder();
                    // Draw border lines
                    spriteBatch.Draw(debugTexture, new Rectangle(border.X, border.Y, border.Width, 2), Color.Red);
                    spriteBatch.Draw(debugTexture, new Rectangle(border.X, border.Y + border.Height - 2, border.Width, 2), Color.Red);
                    spriteBatch.Draw(debugTexture, new Rectangle(border.X, border.Y, 2, border.Height), Color.Red);
                    spriteBatch.Draw(debugTexture, new Rectangle(border.X + border.Width - 2, border.Y, 2, border.Height), Color.Red);
                }
            }
        }

        protected virtual void DrawHealthBar(SpriteBatch spriteBatch, Texture2D debugTexture)
        {
            var border = GetBorder();

            int healthBarWidth = border.Width;
            int healthBarHeight = 5;
            int healthBarY = border.Y - healthBarHeight - 2;

            // Background (red)
            spriteBatch.Draw(debugTexture,
                new Rectangle(border.X, healthBarY, healthBarWidth, healthBarHeight),
                Color.Red);

            // Health (green)
            int currentHealthWidth = (int)((float)Health.CurrentHealth / Health.MaxHealth * healthBarWidth);
            spriteBatch.Draw(debugTexture,
                new Rectangle(border.X, healthBarY, currentHealthWidth, healthBarHeight),
                Color.Green);
        }

    }
}
