using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Interfaces
{
    public interface IProjectile : IGameObject
    {
       Vector2 Position { get; set; }
       Vector2 Direction { get; set; }
       bool IsActive { get; }
       Rectangle GetBorder();
    }
}
