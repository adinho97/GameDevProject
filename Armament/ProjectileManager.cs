using GameDevProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Armament
{
    public class ProjectileManager : IGameObject
    {
        private List<IProjectile> _activeProjectiles;

        public ProjectileManager()
        {
            _activeProjectiles = new List<IProjectile>();
        }

        public void Add(IProjectile projectile)
        {
            if (projectile != null)
            {
                _activeProjectiles.Add(projectile);
            }
        }

        public void Update(GameTime gameTime, List<ICollidable> enemy_collidables, ICollidable player_collidable) 
        {
            //update active projectiles
            for (int i = _activeProjectiles.Count - 1; i >= 0; i--)
            {
                var projectile = _activeProjectiles[i];
                projectile.Update(gameTime);

                if (projectile.ShouldHitPlayer)
                {
                    // Check for collisions with player
                    CheckCollisions(projectile, new List<ICollidable>() { player_collidable });
                }
                else
                {
                    // Check for collisions with enemies
                    CheckCollisions(projectile, enemy_collidables);
                }

                // Remove inactive projectiles
                if (!projectile.IsActive)
                {
                    _activeProjectiles.RemoveAt(i);
                }
            }
        }

        private void CheckCollisions(IProjectile projectile, List<ICollidable> collidables)
        {
            foreach (var collidable in collidables) 
            {
                if (projectile.GetBorder().Intersects(collidable.GetBorder()))
                {
                    collidable.Health.TakeDamage(projectile.Damage);
                    projectile.IsActive = false;
                    break;
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var projectile in _activeProjectiles) 
            {
                projectile.Draw(spriteBatch);
            }
        }

        public void Update(GameTime gameTime)
        {
            return;
        }
    }
}
