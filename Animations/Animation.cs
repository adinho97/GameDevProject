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

            // Default to the first direction added
            if (currentFrames == null)
                currentFrames = framesByDirection[direction];
        }

        public void SetDirection(string directionString)
        {
            currentFrames.Clear(); // Clear previous direction frames

            int columnIndex = 0; // Default to "up"

            // Map directionString to column index
            switch (directionString)
            {
                case "up":
                    columnIndex = 0; // First column
                    break;
                case "right":
                    columnIndex = 1; // Second column
                    break;
                case "down":
                    columnIndex = 2; // Third column
                    break;
                case "left":
                    columnIndex = 3; // Fourth column
                    break;
            }

            // Assuming each sprite frame is uniform in size
            int frameWidth = 32;  // Width of one frame in pixels
            int frameHeight = 32; // Height of one frame in pixels

            // Add all frames for this direction (one column)
            for (int i = 0; i < 4; i++) // Assuming 4 rows of frames per column
            {
                currentFrames.Add(new AnimationFrame(new Rectangle(columnIndex * frameWidth, i * frameHeight, frameWidth, frameHeight)));
            }

            counter = 0; // Reset animation frame index
        }

        public void Update(GameTime gameTime)
        {
            
            if (currentFrames == null || currentFrames.Count == 0)
                return;

            CurrentFrame = currentFrames[counter];

            frameMovement += gameTime.ElapsedGameTime.TotalSeconds;
            if (frameMovement >= 0.2) // Adjust animation speed as needed
            {
                counter++;
                frameMovement = 0;
            }

            if (counter >= currentFrames.Count)
                counter = 0;
        }
    }
}
