using GameDevProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Characters.Enemy
{
    public class EnemyManager
    {

        private List<Enemy> enemies;
        private Texture2D enemyTexture;
        private float spawnTimer;
        private float spawnInterval;
        private List<ICollidable> collidables;

        public EnemyManager(Texture2D enemyTexture, List<ICollidable> collidables)
        {
            this.enemyTexture = enemyTexture;
            collidables = new List<ICollidable>();
            this.collidables = collidables;
            enemies = new List<Enemy>();
            spawnInterval = 3000f;
            spawnTimer = 0f;
        }


        public void Update(GameTime gameTime, Vector2 playerPosition)
        {
            Console.WriteLine("Updating enemies...");


            foreach (Enemy enemy in enemies)
            {
                enemy.Update(gameTime, playerPosition);
            }

            //timer enemy spawn
            spawnTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (spawnTimer > spawnInterval)
            {
                SpawnEnemy();
                spawnTimer = 0f;
            }

        }

        public void Draw(SpriteBatch spriteBatch, Texture2D debugTexture)
        {
            foreach (Enemy enemy in enemies)
            {
                enemy.Draw(spriteBatch, debugTexture);
            }
        }

        public void SpawnEnemy()
        {
            Random random = new Random();
            int screenWidth = 1920; // Use actual screen width if needed
            int screenHeight = 1080; // Use actual screen height if needed

            int side = random.Next(0, 4);
            Vector2 spawnPosition = Vector2.Zero;

            switch (side)
            {
                case 0: spawnPosition = new Vector2(random.Next(0, screenWidth), -100); break; //tOP
                case 1: spawnPosition = new Vector2(screenWidth + 100, random.Next(0, screenHeight)); break; //RIGHT
                case 2: spawnPosition = new Vector2(random.Next(0, screenWidth), screenHeight + 100); break; // Bottom
                case 3: spawnPosition = new Vector2(-100, random.Next(0, screenHeight)); break; // Left
            }

            var newEnemy = EnemyFactory.CreateEnemy("Snake", enemyTexture, spawnPosition);
            enemies.Add(newEnemy);
            collidables.Add(newEnemy);

        }
        public List<Enemy> GetEnemies() { return enemies; }

    }
}
