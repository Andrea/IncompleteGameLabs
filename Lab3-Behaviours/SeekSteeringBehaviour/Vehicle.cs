using System;
using Microsoft.Xna.Framework;

namespace SeekSteeringBehaviour
{
	public class Vehicle
	{
		public Vector2 Position;
		public Vector2 Velocity;
		public float MaxForce;
		public float MaxSpeed;
		public float Mass;
		public float Rotation;

		public void Update(Vector2 steeringDirection, GameTime gameTime)
		{
			// Todo:
			// Use the TruncateVector method to make sure the steeringDirection vector is less than MaxForce units long.

			
			// Todo:
			// Create an acceleration vector that represents an acceleration in the X and Y dimensions. The vector should
			//  take the vehicles mass into account. Apply the acceleration to the vehicles velocity. Remember that we're
			//  dealing with "pixels per second" units, so scale the acceleration value appropriately by elapsed time.

			// Hint:
			// The equation you need is acceleration = force / mass.


			// Todo:
			// Use the TruncateVector method to make sure the Velocity vector is less than MaxSpeed units long.


			SetRotation();
			SetPosition(gameTime);
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

		private void SetRotation()
		{
			Rotation = (float) Math.Atan2(Velocity.Y, Velocity.X);

			if (Rotation < 0)
				Rotation += MathHelper.TwoPi;
		}

		private void SetPosition(GameTime gameTime)
		{
			Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
		}
	}
}