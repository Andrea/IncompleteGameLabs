using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AnimatedSprites
{
	public class AnimatedSpritesLab : Game
	{
		private const int NumColumns = 7;
		private const int NumRows = 7;
		private const int GridCellWidth = 64;
		private const int GridCellHeight = 64;

		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		private bool[] _grid = new bool[NumColumns * NumRows];
		private Texture2D _tile;
		private int _gridIndexThatMarioIsStandingIn;
		private AnimatedSprite _marioSprite;


		public AnimatedSpritesLab()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;

		}

		protected override void Initialize()
		{
			base.Initialize();
			GenerateMaze();
		}

		private void GenerateMaze()
		{
			var tiles = new[]
			              	{
			              		0, 1, 0, 0, 0, 1, 0,
			              		0, 0, 0, 1, 0, 1, 0,
			              		0, 1, 0, 1, 0, 1, 0,
			              		0, 1, 1, 1, 0, 1, 0,
			              		0, 1, 0, 1, 0, 0, 0,
			              		0, 1, 0, 0, 1, 0, 1,
			              		0, 0, 0, 1, 0, 0, 0,

			              	};

			for (var i = 0; i < tiles.Length; i++)
			{
				if (tiles[i] == 1)
					_grid[i] = true;
			}
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);
			_tile = Content.Load<Texture2D>("StoneBlock");
			_marioSprite = new AnimatedSprite(4, 4, 48, 65, 60, Content.Load<Texture2D>("mario2"));
		}


		protected override void UnloadContent()
		{
		}

		protected override void Update(GameTime gameTime)
		{
			// Allows the game to exit
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				this.Exit();


			var keyboardState = Keyboard.GetState();
			var elapsedTime = (int)gameTime.ElapsedGameTime.TotalMilliseconds;
			if (keyboardState.IsKeyDown(Keys.Right))
			{
				MoveMario(new Vector2(5, 0), elapsedTime);
				_marioSprite.SetAnimationRow((int)FacingDirection.Right);
			}
			else if (keyboardState.IsKeyDown(Keys.Left))
			{
				MoveMario(new Vector2(-5, 0), elapsedTime);
				_marioSprite.SetAnimationRow((int)FacingDirection.Left);
			}
			else if (keyboardState.IsKeyDown(Keys.Up))
			{
				MoveMario(new Vector2(0, -5), elapsedTime);
				_marioSprite.SetAnimationRow((int)FacingDirection.Up);
			}
			else if (keyboardState.IsKeyDown(Keys.Down))
			{
				MoveMario(new Vector2(0, 5), elapsedTime);
				_marioSprite.SetAnimationRow((int)FacingDirection.Down);
			}

			if (keyboardState.IsKeyUp(Keys.Right) && keyboardState.IsKeyUp(Keys.Left) && keyboardState.IsKeyUp(Keys.Up) && keyboardState.IsKeyUp(Keys.Down))
			{
				_marioSprite.Reset();
			}

			base.Update(gameTime);
		}

		private void MoveMario(Vector2 positionDiff, int elapsedTime)
		{
			var marioCenter = GetMarioCenter();


			if (IsGridCellEmpty(GetGridIndex(marioCenter + positionDiff)))
			{
				if (_marioSprite.Position.X + positionDiff.X < NumRows * GridCellWidth &&
					_marioSprite.Position.Y + positionDiff.Y < NumColumns * GridCellHeight)
					_marioSprite.Position += positionDiff;
			}

			_marioSprite.Update(elapsedTime);
			_gridIndexThatMarioIsStandingIn = GetGridIndex(GetMarioCenter());

		}

		private Vector2 GetMarioCenter()
		{
			return new Vector2
					{
						X = _marioSprite.Position.X + _marioSprite.FrameWidth / 2,
						Y = _marioSprite.Position.Y + _marioSprite.FrameHeight / 2
					};
		}

		protected override void Draw(GameTime gameTime)
		{
			const int tileVerticalOffset = 19;
			GraphicsDevice.Clear(Color.Black);

			_spriteBatch.Begin();
			for (var i = 0; i < NumRows * NumColumns; i++)
			{
				if (i == _gridIndexThatMarioIsStandingIn)
				{
					_marioSprite.Draw(_spriteBatch);
				}

				if (_grid[i] == false)
					continue;

				var tilePosition = new Vector2
									{
										X = GridCellWidth * (i % NumColumns),
										Y = (GridCellHeight * (i / NumRows)) - tileVerticalOffset
									};
				_spriteBatch.Draw(_tile, tilePosition, Color.White);
			}

			_spriteBatch.End();
			base.Draw(gameTime);
		}

		private bool IsGridCellEmpty(int gridIndex)
		{
			if (gridIndex < 0 || gridIndex > _grid.Length - 1)
				return false;

			return _grid[gridIndex] == false;
		}

		private static int GetGridIndex(Vector2 position)
		{
			//			y * numOfThingsAcross + x
			return (int)(Math.Floor(position.Y / GridCellHeight) * NumRows + Math.Floor(position.X / GridCellWidth));
		}
	}
}
