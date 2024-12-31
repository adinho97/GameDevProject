using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.GameState
{
    public abstract class GameState
    {
        protected GameStateManager gameStateManager;

        public GameState(GameStateManager gameStateManager)
        { 
            this.gameStateManager = gameStateManager;
        }

        public abstract void Enter();
        public abstract void Exit();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
