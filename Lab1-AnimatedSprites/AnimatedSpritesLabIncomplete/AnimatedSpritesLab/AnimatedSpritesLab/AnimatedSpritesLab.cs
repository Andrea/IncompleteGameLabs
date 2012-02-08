using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AnimatedSprites
{
	public class AnimatedSpritesLab : Game
	{
		private const int NumGridColumns = 7;
		private const int NumGridRows = 7;
		private const int GridCellWidth = 64;
		private const int GridCellHeight = 64;

		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		private bool[] _grid = new bool[NumGridColumns * NumGridRows];
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

            // Todo:
            // Create a new AnimatedSprite instance, and assign it to the _marioSprite field. The sprite should have the
            //  following properties:
            //
            //  4 frames across
            //  4 frames down
            //  a frame width of 48
            //  a frame height of 65
            //  it should change frames 10 times a second
            //  it should use the mario2 texture

		}
		
		protected override void UnloadContent()
		{
		}

		protected override void Update(GameTime gameTime)
		{
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
				// Todo:
                // Call the Reset method of the _marioSprite instance so that the current frame resets back to 0 when no keys are pressed.
			}

			base.Update(gameTime);
		}

		private void MoveMario(Vector2 positionDiff, int elapsedTime)
		{
			var marioCenter = GetMarioCenter();
			
			if (IsGridCellEmpty(GetGridIndex(marioCenter + positionDiff)))
			{
				if (_marioSprite.Position.X + positionDiff.X < NumGridRows * GridCellWidth &&
					_marioSprite.Position.Y + positionDiff.Y < NumGridColumns * GridCellHeight)
					_marioSprite.Position += positionDiff;
			}

            // Todo:
            // Call the Update method of the _marioSprite instance, passing the elapsedTime value as its parameter.
            





            // Don't modify this method below this comment.

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
			for (var i = 0; i < NumGridRows * NumGridColumns; i++)
			{
				if (i == _gridIndexThatMarioIsStandingIn)
				{
                    // Todo:
                    // Call the Draw method of the _marioSprite instance, passing the _spriteBatch field as its only parameter.

				}

				if (_grid[i] == false)
					continue;

				var tilePosition = new Vector2
									{
										X = GridCellWidth * (i % NumGridColumns),
										Y = (GridCellHeight * (i / NumGridRows)) - tileVerticalOffset
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
			return (int)(Math.Floor(position.Y / GridCellHeight) * NumGridRows + Math.Floor(position.X / GridCellWidth));
		}
	}
}
