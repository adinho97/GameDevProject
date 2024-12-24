using GameDevProject.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace GameDevProject.Movement
{
    public class InputMovement : IMovementBehaviour
    {
        private readonly Vector2 direction;
        private readonly float speed;

        public InputMovement(Vector2 inputDirection, float movementSpeed) 
        {
            direction = inputDirection;
            speed = movementSpeed;
        }

        public Vector2 Update(Vector2 currentPosition, GameTime gameTime) 
        {
            if(direction == Vector2.Zero)
                return currentPosition;

            return currentPosition + direction * speed;
        }
    }
}
