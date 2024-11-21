using GameDevProject.Animations;
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
        Animation animation;
       

        public Hero(Texture2D texture)
        {
            heroTexture = texture;
            animation = new Animation();
            animation.AddFrame(new AnimationFrame(new Rectangle(0,0, 125, 131)));
            animation.AddFrame(new AnimationFrame(new Rectangle(125,0, 125, 131)));
            animation.AddFrame(new AnimationFrame(new Rectangle(250,0, 125, 131)));
            animation.AddFrame(new AnimationFrame(new Rectangle(375,0, 125, 131)));
            animation.AddFrame(new AnimationFrame(new Rectangle(500,0, 125, 131)));
            animation.AddFrame(new AnimationFrame(new Rectangle(625,0, 125, 131)));
            
        }


        public void Update()
        {
            animation.Update();
           
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(heroTexture, new Vector2(20,20), animation.CurrentFrame.SourceRectangle, Color.White);
        }
    }
}
