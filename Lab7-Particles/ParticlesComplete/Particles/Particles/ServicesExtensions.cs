using Microsoft.Xna.Framework;

namespace Particles
{
	public static class ServicesExtensions
	{
		public static T GetService<T>(this GameServiceContainer services)
		{
			return (T) services.GetService(typeof (T));
		}
	}
}
