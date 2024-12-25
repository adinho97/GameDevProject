using GameDevProject.Interfaces;
using GameDevProject.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.GameState
{
    public class StartScreenState : IGameState
    {

        private UIManager uiManager;

        public StartScreenState(UIManager uiManager) 
        { 
            this.uiManager = uiManager;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            uiManager.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            uiManager.Update(gameTime);

            if (!uiManager.IsStartScreenActive())
            {
                //add gamestate manager
                GameStateManager.Instance.SetState(GameStateManager.GameState.Gameplay);
            }
        }
    }
}
