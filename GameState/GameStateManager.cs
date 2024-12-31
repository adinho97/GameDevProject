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

    }
}
