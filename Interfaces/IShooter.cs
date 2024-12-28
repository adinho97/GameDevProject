using GameDevProject.Armament;
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
        void Shoot(ProjectileManager projectileManager, Texture2D projectileTexture);
    }
}
