using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Interfaces
{
    public interface IShooter
    {
        void Shoot(List<IProjectile> projectiles, Texture2D projectileTexture);
    }
}
