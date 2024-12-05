using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Interfaces
{
    public interface IProjectileCollidable : ICollidable
    {
        void OnProjectileCollision(IProjectile projectile);
    }
}
