using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Interfaces
{
    public interface IMovementBehaviour
    {
        Vector2 Update(Vector2 currentPosition, GameTime gameTime);
    }
}
