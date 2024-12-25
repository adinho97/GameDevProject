using GameDevProject.Characters;
using GameDevProject.Characters.Enemy;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.GameState
{
    public class GameWorld
    {
        private static GameWorld instance;
        public static GameWorld Instance
        {
            get
            {
                if (instance == null)
                    instance = new GameWorld();
                return instance;
            }
        }

        public Hero Hero { get; private set; }
        public List<Enemy> Enemies { get; private set; }
        public ContentManager ContentManager { get; private set; }

        private GameWorld()
        {
            Enemies = new List<Enemy>();
        }

        public void Initialize(ContentManager content, Hero hero, List<Enemy> enemies)
        {
            ContentManager = content;
            Hero = hero;
            Enemies = enemies;
        }
    }
}
