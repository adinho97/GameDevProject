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
    public class MovementManager
    {
        private IMovementBehaviour movementBehaviour;

        public MovementManager(IMovementBehaviour initialBehaviour)
        {
            movementBehaviour = initialBehaviour;
        }

        public void SetMovementBehaviour(IMovementBehaviour newBehaviour)
        {
            movementBehaviour = newBehaviour;
        }

        public Vector2 UpdatePosition(Vector2 currentPosition, GameTime gameTime)
        {
            return movementBehaviour.Update(currentPosition, gameTime);
        }
    }
}
