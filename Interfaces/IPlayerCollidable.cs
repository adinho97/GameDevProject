using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Interfaces
{
    public interface IPlayerCollidable : ICollidable
    {
        void OnPlayerCollision();
    }
}
