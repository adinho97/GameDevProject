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
    public static class EnemyFactory
    {

        //createenemy
        public static Enemy CreateEnemy(string type, Texture2D texture, Vector2 position)
        {
            switch (type)
            {
                case "Snake":
                    return new Enemy(texture, position);

                default:
                    throw new ArgumentException("uknown type");
            }
        }
    }
}
