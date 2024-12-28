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
            string directionString = "down"; 

            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.A))
            {
                direction.X = -1;
                directionString = "left";
            }
            else if (state.IsKeyDown(Keys.Right)|| state.IsKeyDown(Keys.D))
            {
                direction.X = 1;
                directionString = "right";
            }
            else if (state.IsKeyDown(Keys.Up) || state.IsKeyDown(Keys.W))
            {
                direction.Y = -1;
                directionString = "up"; 
            }
            else if (state.IsKeyDown(Keys.Down) || state.IsKeyDown(Keys.S))
            {
                direction.Y = 1;
                directionString = "down"; 
            }

            if (direction != Vector2.Zero) 
            {
                direction.Normalize();
            }

            return (direction, directionString);
        }

        public bool IsShooting()
        {
            KeyboardState state = Keyboard.GetState();
            return state.IsKeyDown(Keys.Space);
        }
    }

}
