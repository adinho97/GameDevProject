using GameDevProject.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Movement
{
    public class BasicMovement : IMovementBehaviour
    {
        private IInputReader inputReader;

        public BasicMovement(IInputReader reader)
        {
            inputReader = reader;
        }

        public Vector2 Update(Vector2 currentPosition, GameTime gameTime)
        {
            var (direction, _) = inputReader.ReadInput();
            return currentPosition + direction * 3f; // Update position based on input direction
        }
    }
}
