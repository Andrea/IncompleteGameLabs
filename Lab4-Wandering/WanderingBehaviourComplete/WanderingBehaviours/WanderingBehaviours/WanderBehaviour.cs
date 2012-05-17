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
            _currentWanderAngle += RateOfChangeOfDirection * (float) (_random.NextDouble() * 2 - 1);

            _lastDirectionChangeTime = (int) gameTime.TotalGameTime.TotalSeconds;

            var circlePosition = Vector2.Normalize(vehicle.Velocity)*CircleDistance +vehicle.Position;
            var circleOffset = new Vector2((float)(CircleRadius * Math.Cos(_currentWanderAngle)),
                                           (float)(CircleRadius * Math.Sin(_currentWanderAngle)));
            var steeringDirection = (circlePosition+ circleOffset) - vehicle.Position;
            
            return steeringDirection;
        }
    }
}