using System;
using Microsoft.Xna.Framework;

namespace WanderingBehaviours
{
    public class Vehicle
    {
        private readonly float _mass;
        private readonly float _maxSpeed;
        private readonly float _maxForce;
        private float _rotation;
        private Vector2 _velocity;

        public Vehicle(Vector2 position, Vector2  velocity, float mass, float maxSpeed, float maxForce)
        {
            Position = position;
            _velocity = velocity;
            _mass = mass;
            _maxSpeed = maxSpeed;
            _maxForce = maxForce;
        }

        public float Rotation
        {
            get {
                return _rotation;
            }
        }

        public Vector2 Velocity
        {
            get {
                return _velocity;
            }
        }

        public Vector2 Position { get; set; }

        public void Update(GameTime gameTime, Vector2 steeringDirection)
        {
            steeringDirection = TruncateVector(steeringDirection, _maxForce);

            var acceleration = steeringDirection / _mass;

            _velocity += (acceleration*(float) gameTime.ElapsedGameTime.TotalSeconds);
            _velocity = TruncateVector(_velocity, _maxSpeed);
            
            SetRotation();
            SetPosition(gameTime);
        }

        private void SetRotation()
        {
            _rotation = (float)Math.Atan2(_velocity.Y, _velocity.X);

            if (_rotation< 0)
                _rotation += MathHelper.TwoPi;
        }

        private void SetPosition(GameTime gameTime)
        {
            Position += _velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        private Vector2 TruncateVector(Vector2 vector, float maxLength)
        {
            if (vector.Length() > maxLength)
            {
                vector.Normalize();
                vector *= maxLength;
            }
            return vector;
        }
    }
}