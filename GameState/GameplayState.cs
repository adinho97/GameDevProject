using GameDevProject.Characters;
using GameDevProject.Characters.Enemy;
using GameDevProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.GameState
{
    public class GameplayState : IGameState
    {
        private Hero hero;
        private List<Enemy> enemies;
        private GameStateManager stateManager;

        public GameplayState(Hero hero, List<Enemy> enemies, GameStateManager stateManager) 
        {
            this.hero = hero;
            this.enemies = enemies;
          this.stateManager = stateManager;
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            hero.Draw(spriteBatch);

            foreach (Enemy enemy in enemies) 
            { 
                enemy.Draw(spriteBatch);
            }


        }

        public void Update(GameTime gameTime)
        {
            hero.Update(gameTime);

            foreach (Enemy enemy in enemies)
            {
                enemy.Update(gameTime);

                if (enemy.GetBorder().Intersects(hero.GetBorder()))
                {
                    // Notify GameStateManager to transition to GameOver state
                    stateManager.SetState(GameStateManager.GameState.GameOver);
                    break;
                }
            }       
        }
    }
}
