using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SimpleCamera
{
	public struct Tile
	{
		public Texture2D Texture;
		public TileType Type;

		public const int Width = 40;
		public const int Height = 32;

		public static readonly Vector2 Size = new Vector2(Width, Height);

		public Tile(Texture2D texture, TileType type)
		{
			Texture = texture;
			Type = type;
		}
	}

    public enum TileType
    {
        Impassable,
        Passable
    }
}