using GameDevProject.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Managers
{
    public class CollisionManager //: ICollidable
    {

        /*
        public static void HandleCollisions(ICollidable hero, List<ICollidable> collidables)
        {
            Rectangle heroBorder = hero.GetBorder();

            foreach (var collidable in collidables)
            {
                if (collidable == hero)
                    continue;

                Rectangle collidableBorder = collidable.GetBorder();

                if (heroBorder.Intersects(collidableBorder))
                {
                    ResolveCollisions(hero, collidableBorder);
                }
            }
        }

        private static void ResolveCollisions(ICollidable hero, Rectangle collidableBorder)
        {

            Rectangle heroBorder = hero.GetBorder();

            //direction of collision
            if (heroBorder.Right > collidableBorder.Left && heroBorder.Left < collidableBorder.Left)
            {
                //colliison from left
                heroBorder.X = collidableBorder.Left - heroBorder.Width;
            }
            else if (heroBorder.Left < collidableBorder.Right && heroBorder.Right > collidableBorder.Right)
            {
                // Hero is colliding from the right
                heroBorder.X = collidableBorder.Right;
            }

            if (heroBorder.Bottom > collidableBorder.Top && heroBorder.Top < collidableBorder.Top)
            {
                // Hero is colliding from the top
                heroBorder.Y = collidableBorder.Top - heroBorder.Height;
            }
            else if (heroBorder.Top < collidableBorder.Bottom && heroBorder.Bottom > collidableBorder.Bottom)
            {
                // Hero is colliding from the bottom
                heroBorder.Y = collidableBorder.Bottom;
            }

            hero.setBorder(heroBorder);
        }
        public Rectangle GetBorder()
        {
            throw new NotImplementedException();
        }

        public void setBorder(Rectangle border)
        {
            throw new NotImplementedException();
        }
        */

        public static void HandleCollisions(ICollidable hero, List<ICollidable> collidables)
        {
            Rectangle heroBorder = hero.GetBorder();

            foreach (var collidable in collidables)
            {
                if (collidable == hero)
                    continue;

                Rectangle collidableBorder = collidable.GetBorder();

                if (heroBorder.Intersects(collidableBorder))
                {
                    ResolveCollisions(hero, collidable, heroBorder, collidableBorder);
                }
            }
        }

        private static void ResolveCollisions(ICollidable hero, ICollidable collidable, Rectangle heroBorder, Rectangle collidableBorder)
        {
            // Handle collision resolution between the hero and another object
            // You can resolve the collision by adjusting the hero's position or other actions

            // Here is an example of resolving the collision by adjusting the hero's position based on the collision
            if (heroBorder.Right > collidableBorder.Left && heroBorder.Left < collidableBorder.Left)
            {
                // Collision from the left
                heroBorder.X = collidableBorder.Left - heroBorder.Width;
            }
            else if (heroBorder.Left < collidableBorder.Right && heroBorder.Right > collidableBorder.Right)
            {
                // Collision from the right
                heroBorder.X = collidableBorder.Right;
            }

            if (heroBorder.Bottom > collidableBorder.Top && heroBorder.Top < collidableBorder.Top)
            {
                // Collision from the top
                heroBorder.Y = collidableBorder.Top - heroBorder.Height;
            }
            else if (heroBorder.Top < collidableBorder.Bottom && heroBorder.Bottom > collidableBorder.Bottom)
            {
                // Collision from the bottom
                heroBorder.Y = collidableBorder.Bottom;
            }

            // Update hero's position after resolving the collision
            hero.setBorder(heroBorder);
        }

    }
      
}
