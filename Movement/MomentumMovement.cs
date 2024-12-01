using GameDevProject.Collisions;
using GameDevProject.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Movement
{
    public class MomentumMovement : IMovementBehaviour
    {
        private Vector2 velocity;
        private Vector2 acceleration;
        private float friction;
        private float maxSpeed;

        // Constructor to initialize momentum movement parameters
        public MomentumMovement(float maxSpeed, float friction, Vector2 initialAcceleration)
        {
            this.maxSpeed = maxSpeed;
            this.friction = friction;
            this.acceleration = initialAcceleration;
            this.velocity = Vector2.Zero;
        }

        // Update movement by applying acceleration, velocity, and friction
        public Vector2 Update(Vector2 currentPosition, GameTime gameTime)
        {
            // Apply acceleration
            velocity += acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Apply friction (decelerate velocity over time)
            velocity *= 1 - friction * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Ensure we don't exceed max speed
            if (velocity.Length() > maxSpeed)
            {
                velocity = Vector2.Normalize(velocity) * maxSpeed;
            }

            // Update position based on velocity
            Vector2 newPosition = currentPosition + velocity;

            return newPosition;
        }

        // Optionally, you could have methods to adjust acceleration or speed at runtime
        public void SetAcceleration(Vector2 newAcceleration)
        {
            acceleration = newAcceleration;
        }

        public void SetFriction(float newFriction)
        {   
            friction = newFriction;
        }

        public void SetMaxSpeed(float newMaxSpeed)
        {
            maxSpeed = newMaxSpeed;
        }
    }
}
