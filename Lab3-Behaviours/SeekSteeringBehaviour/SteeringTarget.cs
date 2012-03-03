using Microsoft.Xna.Framework;

namespace SeekSteeringBehaviour
{
	public class SteeringTarget
	{
		public SteeringTarget(Vector2 position)
		{
			Position = position;
		}

		public Vector2 Position { get; set; }
	}
}