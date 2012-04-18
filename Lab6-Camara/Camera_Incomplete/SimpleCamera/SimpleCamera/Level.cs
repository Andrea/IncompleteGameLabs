using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SimpleCamera
{
	public class Level
	{
		private const int TileSize = 32;
		private List<Tile> _tiles = new List<Tile>();
		private int _columnCount;

		public void LoadLevel(string data, ContentManager content)
		{
			var tileRows = data.Split('\n');
			_columnCount = tileRows[0].ToCharArray().Length;

			foreach (var tileRow in tileRows)
			{
				foreach (var tileType in tileRow)
				{
					switch (tileType)
					{
						case '.':
							_tiles.Add(new Tile(content.Load<Texture2D>("concrete_tile"), TileType.Passable));
							break;

						case 'G':

							break;

						case '0':
							_tiles.Add(new Tile(content.Load<Texture2D>("grass_tile"), TileType.Impassable));
							break;
					}
				}
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			for (int i = 0; i < _tiles.Count; i++)
			{
				var xPosition = i % _columnCount;
				var yPosition = i / _columnCount;
				
				spriteBatch.Draw(_tiles[i].Texture, new Vector2(xPosition, yPosition) * TileSize, Color.White);
			}
		}
	}
}