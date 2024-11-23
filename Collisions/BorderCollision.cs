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

        /*
        public Vector2 KeepInsideBorder(Vector2 position, Rectangle playerCollider)
        {
            //check collision
            if (position.X < border.Left)
                position.X = border.Left;
            if (position.Y < border.Top)
                position.Y = border.Top;
            if(position.X + playerCollider.Width > border.Right)
                position.X = border.Right - playerCollider.Width;
            if(position.Y + playerCollider.Height > border.Bottom)
                position.Y = border.Bottom - playerCollider.Height;

            return position;

        }
        */
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
