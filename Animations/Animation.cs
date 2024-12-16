using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Animations
{
    public class Animation
    {
        private Dictionary<string, List<AnimationFrame>> framesByDirection; //store frame for each direction
        public AnimationFrame CurrentFrame { get; set; }
        private List<AnimationFrame> currentFrames; //active frames

        private int counter;

        private double frameMovement = 0;

        public Animation() 
        {
            
            framesByDirection = new Dictionary<string, List<AnimationFrame>>();
        }
        
        public void AddFrame(string direction, AnimationFrame animationFrame)
        {
          
            if (!framesByDirection.ContainsKey(direction))
                framesByDirection[direction] = new List<AnimationFrame>();

            framesByDirection[direction].Add(animationFrame);

           
            if (currentFrames == null)
                currentFrames = framesByDirection[direction];

            if (CurrentFrame == null)
                CurrentFrame = framesByDirection[direction][0];
        }

        public void SetDirection(string directionString)
        {
            currentFrames.Clear(); 

            int columnIndex = 0; 

            // Map directionString to column index
            switch (directionString)
            {
                case "up":
                    columnIndex = 0; 
                    break;
                case "right":
                    columnIndex = 1; 
                    break;
                case "down":
                    columnIndex = 2; 
                    break;
                case "left":
                    columnIndex = 3; 
                    break;
            }

           
            int frameWidth = 32;  
            int frameHeight = 32; 

          
            for (int i = 0; i < 4; i++)
            {
                currentFrames.Add(new AnimationFrame(new Rectangle(columnIndex * frameWidth, i * frameHeight, frameWidth, frameHeight)));
            }

            
        }

        public void Update(GameTime gameTime)
        {
            
            if (currentFrames == null || currentFrames.Count == 0)
                return;

            CurrentFrame = currentFrames[counter];

            frameMovement += gameTime.ElapsedGameTime.TotalSeconds;
            if (frameMovement >= 0.15) // Adjust animation speed as needed
            {
                counter++;
                frameMovement = 0;
            }

            if (counter >= currentFrames.Count - 1)
                counter = 0;
        }
    }
}
