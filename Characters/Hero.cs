using GameDevProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace GameDevProject.Characters
{
    public class Hero : IGameObject
    {

        Texture2D heroTexture;
        private Rectangle partRectangle;
        private int slideX = 0;

        public Hero(Texture2D texture)
        {
            heroTexture = texture;
            partRectangle = new Rectangle(slideX, 0, 125, 131);
        }


        public void Update()
        {
            slideX += 125;
            if (slideX > 751)
                slideX = 0;

            partRectangle.X = slideX;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(heroTexture, new Vector2(20,20), partRectangle, Color.White);
        }
    }
}
