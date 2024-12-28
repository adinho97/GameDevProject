using GameDevProject.Characters.Enemy;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Interfaces
{
    public interface IEnemyFactory
    {
        Enemy CreateEnemy(string enemyType, Vector2 position);
    }
}
