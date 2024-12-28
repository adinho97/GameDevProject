using GameDevProject.Armament;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Characters.Enemy
{
    public class EnemyFactory
    {

        private GraphicsDevice _device;
        private ProjectileManager _projectileManager;

        public EnemyFactory(GraphicsDevice graphicsDevice, ProjectileManager projectileManager)
        {
            _device = graphicsDevice;
            _projectileManager = projectileManager;
        }

        public static Enemy CreateEnemy(string enemyType, Vector2 position)
        {
            Texture2D enemyTexture = ContentLoader.Instance.LoadTexture(enemyType);

            return enemyType.ToLower() switch
            {
                "snakeenemy" => new SnakeEnemy(enemyTexture, position),
                "babyenemy" => new BabyEnemy(enemyTexture, position, _device.Viewport.Width, _device.Viewport.Height),
                "captainenemy" => new CaptainEnemy(enemyTexture, _projectileManager, position),
                _ => throw new ArgumentException($"Unknown enemy type: {enemyType}")
            };
        }
    }
}
