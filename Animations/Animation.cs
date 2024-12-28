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
            currentFrames = new List<AnimationFrame>();
            CurrentFrame = new AnimationFrame(Rectangle.Empty);
        }
        
        public void AddFrame(string direction, AnimationFrame animationFrame)
        {
            if (!framesByDirection.ContainsKey(direction))
                framesByDirection[direction] = new List<AnimationFrame>();

            framesByDirection[direction].Add(animationFrame);
            currentFrames = framesByDirection[direction];
            CurrentFrame = framesByDirection[direction][0];
        }

        public void SetDirection(string directionString)
        {

            if (directionString == null || !framesByDirection.ContainsKey(directionString))
                return;

            currentFrames = new List<AnimationFrame>(framesByDirection[directionString]);
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
