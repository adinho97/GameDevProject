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
        private GameState currentState;
        private Game1 _game;
       
        public Game1 Game { get { return _game; } }

        public GameStateManager(Game1 game)
        {
            this._game = game;
        }

        public void SetState(GameState state)
        {
            if(currentState != null)
            {
                currentState.Exit();
            } 

            currentState = state;
            currentState.Enter();
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
