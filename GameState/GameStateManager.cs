using GameDevProject.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.GameState
{
    public class GameStateManager
    {
        public enum GameState
        {
            StartScreen,
            Gameplay,
            GameOver
        }

        private static GameStateManager instance;
        private IGameState currentState;

        private GameStateManager() { }

        public static GameStateManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new GameStateManager();

                return instance;
            }
        }

        public void SetState(GameState state)
        {
            switch (state)
            {
                // case GameState.StartScreen:
                //    currentState = new StartScreenState(UIManager.Instance);
                //    break;

                case GameState.Gameplay:
                    currentState = new GameplayState(GameWorld.Instance.Hero, GameWorld.Instance.Enemies, this);
                    break;

                case GameState.GameOver:
                    currentState = new GameOverState(GameWorld.Instance.ContentManager);
                    break;
            }
        }

        public void Update(GameTime gameTime)
        {
            currentState?.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentState?.Draw(spriteBatch);
        }
    }
}
