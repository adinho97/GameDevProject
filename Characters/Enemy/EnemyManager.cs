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
    public class EnemyManager : IGameObject
    {
        private readonly List<string> _enemyNames = new List<string>()
        {
            "snakeEnemy",
            "babyEnemy",
            "captainEnemy",
        };

        private List<Enemy> _activeEnemies;
        private readonly IEnemyFactory _enemyFactory;

        private Hero _hero;

        private Texture2D _debugTexture;

        private Random _random;
        private float _spawnTimer;
        private float SpawnInterval = 4.0f;
        private readonly int _screenWidth;
        private readonly int _screenHeight;

        public int Score { get; private set; }

        public EnemyManager(IEnemyFactory enemyFactory, Hero hero, Texture2D debugTexture, GraphicsDevice graphicsDevice)
        {
            _hero = hero;
            _activeEnemies = new List<Enemy>();
            _enemyFactory = enemyFactory;

            _random = new Random();
            _spawnTimer = SpawnInterval;
            _debugTexture = debugTexture;
            _screenWidth = graphicsDevice.Viewport.Width;
            _screenHeight = graphicsDevice.Viewport.Height;

            ResetScore();
        }

        public void ResetScore()
        {
            Score = 0;  
        }

        public void SpawnEnemy(string enemyType, Vector2 position)
        {
            var enemy = _enemyFactory.CreateEnemy(enemyType, position);
            _activeEnemies.Add(enemy);
        }

        private Vector2 GetRandomEdgePosition()
        {
            int edge = _random.Next(4);
            switch (edge)
            {
                case 0: // Top edge
                    return new Vector2(_random.Next(_screenWidth), 0);
                case 1: // Bottom edge
                    return new Vector2(_random.Next(_screenWidth), _screenHeight - 100);
                case 2: // Left edge
                    return new Vector2(0, _random.Next(_screenHeight));
                case 3: // Right edge
                    return new Vector2(_screenWidth, _random.Next(_screenHeight));
                default:
                    throw new InvalidOperationException("Unexpected edge value");
            }
        }

        private void SpawnRandomEnemyBasedOnScore()
        {
            // Determine which enemies can spawn based on the current score
            List<string> eligibleEnemyTypes;

            if (Score < 50)
            {
                eligibleEnemyTypes = new List<string> { "snakeEnemy" }; // Only snakes
            }
            else if (Score < 100)
            {
                eligibleEnemyTypes = new List<string> { "snakeEnemy", "babyEnemy" }; // Snakes and babies
                SpawnInterval = 3.5f;
            }
            else
            {
                eligibleEnemyTypes = new List<string> { "snakeEnemy", "babyEnemy", "captainEnemy" }; // All types
                SpawnInterval = 3.0f;
            }

            // Pick a random enemy from the eligible types
            string enemyType = eligibleEnemyTypes[_random.Next(eligibleEnemyTypes.Count)];
            Vector2 position = GetRandomEdgePosition();
            SpawnEnemy(enemyType, position);
        }
       /*
        private void SpawnRandomEnemy()
        {
            string enemyType = _enemyNames[_random.Next(_enemyNames.Count)];
            Vector2 position = GetRandomEdgePosition();
            SpawnEnemy(enemyType, position);
        }
       */

        public void Update(GameTime gameTime)
        {
            // Spawn enemies
            _spawnTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_spawnTimer <= 0)
            {
                SpawnRandomEnemyBasedOnScore();
                _spawnTimer = SpawnInterval;
            }

            // Update the enemies
            for (int i = _activeEnemies.Count - 1; i >= 0; i--)
            {
                var enemy = _activeEnemies[i];
                enemy.Update(gameTime, _hero);

                if (!enemy.Health.IsAlive)
                {
                    Score += enemy.Score;
                    _activeEnemies.RemoveAt(i);
                }
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            foreach(var enemy in _activeEnemies)
            {
                enemy.Draw(spritebatch, _debugTexture);
            }
        }

        public IReadOnlyList<Enemy> GetActiveEnemies() => _activeEnemies.AsReadOnly();
        
    }
}
