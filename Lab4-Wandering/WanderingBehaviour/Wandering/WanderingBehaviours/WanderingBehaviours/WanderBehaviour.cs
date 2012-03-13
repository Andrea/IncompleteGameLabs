using System;
using Microsoft.Xna.Framework;

namespace WanderingBehaviours
{
    public class WanderBehaviour : IBehaviour
    {
        private readonly Random _random;
        private float _currentWanderAngle;
        private int _lastDirectionChangeTime;

        public WanderBehaviour(Random random)
        {
            _random = random;

            RateOfChangeOfDirection = 1.5f;
            CircleDistance = 222;
            CircleRadius = 350;
        }

        public float CircleRadius { get; set; }
        public float CircleDistance { get; set; }
        public float RateOfChangeOfDirection { get; set; }

        public Vector2 Update(Vehicle vehicle, GameTime gameTime)
        {
				// Todo:
					// Add a value of between -RateOfChangeOfDirection and RateOfChangeOfDirection to the _currentWanderAngle variable.
					//  Update the variable every  half second, so the changes in steering are more obvious 

					// Hint:
					// You can use the _random variable that was passed in to the constructor to get a random value between -1 and 1.
					
					// Todo:
					// Figure out the circle position by taking the vehicles velocity (vehicle is a parameter passed into this method),
					//  normalising it, multiplying by the circle distance, and finally adding the vehicles position.


					// Todo:
					// Calculate an offset on the steering circle based on the value in the _currentWanderAngle variable and the radius
					//  of the circle, whose value is stored in CircleRadius.

					


					// Todo:
					// Return a steering direction that represents a vector from the vehicles position to the point on the steering cirle
					//  you just calculated.
					
		
            return Vector2.Zero;
        }
    }
}