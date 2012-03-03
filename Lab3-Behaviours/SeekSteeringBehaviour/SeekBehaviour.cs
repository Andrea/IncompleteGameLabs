using Microsoft.Xna.Framework;

namespace SeekSteeringBehaviour
{
	public class SeekBehaviour : ISteeringBehaviour
	{
		private readonly SteeringTarget _target;

		public SeekBehaviour(SteeringTarget target)
		{
			_target = target;
		}
		
		public Vector2 Update(Vehicle vehicle, GameTime gameTime)
		{
			// Todo:
			// Create a Vector2 instance that represents a steering vector in the direction of the target. You can get the coordinates of
			//  the target from the _target.Position property. Return the new Vector2 instance.

			// Hint:
			// To calculate the desired velocity vector, create a vector pointing from the vehicle position to the target, normalize it, and 
			//  multiply by the vehicle's max speed.

			return Vector2.Zero;
		}
	}
}