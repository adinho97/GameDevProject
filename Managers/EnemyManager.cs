using GameDevProject.Characters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Managers
{
    public class EnemyManager
    {

        private List<Enemy> enemies;
        private Texture2D enemyTexture;
        private float spawnTimer;
        private float spawnInterval;

        public EnemyManager(Texture2D enemyTexture) 
        {
            this.enemyTexture = enemyTexture;
            enemies = new List<Enemy>();
            spawnInterval = 3000f;
            spawnTimer = 0f;
        }
        
        public void SpawnEnemy()
        {
            Random random = new Random();
            int screenWidth = 1920; // Use actual screen width if needed
            int screenHeight = 1080; // Use actual screen height if needed

            // Choose a random position for the enemy to spawn outside the viewport
            Vector2 spawnPosition = new Vector2(random.Next(0, screenWidth), random.Next(0, screenHeight));
            var newEnemy = new Enemy(enemyTexture, spawnPosition);
            enemies.Add(newEnemy);

            // Debugging log
            Console.WriteLine("Enemy Spawned at: " + spawnPosition.ToString());

        }

        public List<Enemy> GetEnemies() { return enemies; } 

        public void Update(GameTime gameTime, Vector2 playerPosition)
        {
            Console.WriteLine("Updating enemies...");
            //timer enemy spawn
            spawnTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (spawnTimer > spawnInterval)
            {
                SpawnEnemy();
                spawnTimer = 0f;
            }

            foreach (Enemy enemy in enemies) 
            {
                enemy.Update(gameTime, playerPosition);
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Enemy enemy in enemies) 
            { 
                enemy.Draw(spriteBatch); 
            }
        }

    }
}
