using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Interfaces
{
    public interface IEnemy : ICollidable
    {
        void OnProjectileCollision(IProjectile projectile);
        void Update(GameTime gameTime); // Method to update the enemy's state
    }
}
