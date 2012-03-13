using Microsoft.Xna.Framework;

namespace WanderingBehaviours
{
    public interface IBehaviour
    {
        Vector2 Update(Vehicle vehicle, GameTime gameTime);
    }
}