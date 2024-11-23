using GameDevProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Input
{
    public class KeyboardReader : IInputReader
    {
        public (Vector2 direction, string directionString) ReadInput()
        {
            var direction = Vector2.Zero;
            string directionString = "down"; // Default direction

            KeyboardState state = Keyboard.GetState();

            // Check for left and right movement
            if (state.IsKeyDown(Keys.Left))
            {
                direction.X = -1;
                directionString = "left";
            }
            else if (state.IsKeyDown(Keys.Right))
            {
                direction.X = 1;
                directionString = "right";
            }

            // Check for up and down movement
            if (state.IsKeyDown(Keys.Up))
            {
                direction.Y = -1;
                directionString = "up"; // Override with "up" if both are pressed
            }
            else if (state.IsKeyDown(Keys.Down))
            {
                direction.Y = 1;
                directionString = "down"; // Override with "down" if both are pressed
            }

            return (direction, directionString);
        }
    }

}
