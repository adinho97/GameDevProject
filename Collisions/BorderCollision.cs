using GameDevProject.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace GameDevProject.Collisions
{
    public class BorderCollision
    {
        private Rectangle border;

        public BorderCollision(Rectangle border)
        {
            this.border = border;
        }

 
        public void Constrain(ICollidable collidable)
        {
            var bounds = collidable.GetBorder();

            if (bounds.Left < border.Left)
                bounds.X = border.Left;
            if (bounds.Top < border.Top)
                bounds.Y = border.Top;
            if (bounds.Right > border.Right)
                bounds.X = border.Right - bounds.Width;
            if (bounds.Bottom > border.Bottom)
                bounds.Y = border.Bottom - bounds.Height;

            collidable.setBorder(bounds);
        }
    }
}
