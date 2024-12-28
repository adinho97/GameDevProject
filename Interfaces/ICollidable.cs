using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Interfaces
{
    public interface ICollidable
    {
        Rectangle GetBorder();
        void setBorder(Rectangle border);

        IHealth Health { get; }
    }
}
