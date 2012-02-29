using Microsoft.Xna.Framework;

namespace SeekSteeringBehaviour
{
	public interface ISteeringBehaviour
	{
		Vector2 Update(Vehicle vehicle, GameTime gameTime);
	}
}