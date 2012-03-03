using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CollisionDetection
{
	public class CollisionDetectionLab : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		private Texture2D _turretTexture;
		private Texture2D _bulletTexture;
		private Vector2 _turretPosition;
		private Rectangle _barRectangle;
		private float _barRotation;
		private Vector2 _barOrigin;
		private Texture2D _barTexture;
		private float _barRotationSpeed = 0.03f;
		private List<Vector2> _bullets = new List<Vector2>();
		private KeyboardState _previousKeyboardState;
		private Vector2 _screenCenter;
		private Rectangle _barBoundingBox;
		private Texture2D _whiteTexture;
		private Color[] _barTextureData;
		private Color[] _bulletTextureData;


		public CollisionDetectionLab()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		protected override void Initialize()
		{
			base.Initialize();

			_turretPosition = new Vector2((GraphicsDevice.Viewport.Width - _turretTexture.Width) / 2, GraphicsDevice.Viewport.Height - 50);
			_barRectangle = new Rectangle(0, 0, _barTexture.Width, _barTexture.Height);
			_barOrigin = new Vector2(_barTexture.Width / 2, _barTexture.Height / 2);
			_screenCenter = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			_turretTexture = Content.Load<Texture2D>("turret");
			_bulletTexture = Content.Load<Texture2D>("bullet");
			_barTexture = Content.Load<Texture2D>("bar");

			_barTextureData = new Color[_barTexture.Width * _barTexture.Height];
			_barTexture.GetData(_barTextureData);

			_bulletTextureData = new Color[_bulletTexture.Width * _bulletTexture.Height];
			_bulletTexture.GetData(_bulletTextureData);

			_whiteTexture = new Texture2D(GraphicsDevice, 1, 1);
			_whiteTexture.SetData(new[] { Color.White });
		}

		protected override void UnloadContent()
		{
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				Exit();

			var keyboardState = Keyboard.GetState();

			if (keyboardState.IsKeyDown(Keys.Left))
				_turretPosition.X -= 5;
			else if (keyboardState.IsKeyDown(Keys.Right))
				_turretPosition.X += 5;

			if (keyboardState.IsKeyDown(Keys.Space) && _previousKeyboardState.IsKeyDown(Keys.Space) == false)
				_bullets.Add(new Vector2(_turretPosition.X + _turretTexture.Width / 2 - _bulletTexture.Width / 2, _turretPosition.Y));

			UpdateBullets();
			CheckForCollisions();

			_previousKeyboardState = keyboardState;
			_barRotation += _barRotationSpeed;

			base.Update(gameTime);
		}

		private void UpdateBullets()
		{
			for (var i = _bullets.Count - 1; i >= 0; i--)
			{
				_bullets[i] = new Vector2(_bullets[i].X, _bullets[i].Y - 5);

				if (_bullets[i].Y < 0)
				{
					_bullets.RemoveAt(i);
				}
			}
		}

		private void CheckForCollisions()
		{
			var barTransform = Matrix.CreateTranslation(new Vector3(-_barOrigin, 0)) *
			                   Matrix.CreateRotationZ(_barRotation) *
			                   Matrix.CreateTranslation(new Vector3(_screenCenter, 0));

			_barBoundingBox = CalculateBoundingRectangle(_barRectangle, barTransform);

			for (var i = _bullets.Count - 1; i >= 0; i--)
			{
				var bulletBoundingBox = new Rectangle((int) _bullets[i].X, (int) _bullets[i].Y, 
					_bulletTexture.Width, _bulletTexture.Height);

				if (_barBoundingBox.Intersects(bulletBoundingBox))
				{
					var bulletTransform = Matrix.CreateTranslation(new Vector3(_bullets[i], 0));

					if (IntersectPixels(barTransform, _barTexture.Width, _barTexture.Height, _barTextureData,
					                    bulletTransform, _bulletTexture.Width, _bulletTexture.Height, _bulletTextureData))
						_bullets.RemoveAt(i);
				}
			}
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			_spriteBatch.Begin();

			_spriteBatch.Draw(_turretTexture, _turretPosition, Color.White);
			_spriteBatch.Draw(_barTexture,
							  _screenCenter,
							  null,
							  Color.White,
							  _barRotation,
							  _barOrigin,
							  1,
							  SpriteEffects.None,
							  0);

			foreach (var bullet in _bullets)
			{
				_spriteBatch.Draw(_bulletTexture, bullet, Color.White);
			}

			_spriteBatch.Draw(_whiteTexture, new Rectangle(_barBoundingBox.X, _barBoundingBox.Y, 1, _barBoundingBox.Height), Color.White);
			_spriteBatch.Draw(_whiteTexture, new Rectangle(_barBoundingBox.X, _barBoundingBox.Y, _barBoundingBox.Width, 1), Color.White);
			_spriteBatch.Draw(_whiteTexture, new Rectangle(_barBoundingBox.X + _barBoundingBox.Width, _barBoundingBox.Y, 1, _barBoundingBox.Height), Color.White);
			_spriteBatch.Draw(_whiteTexture, new Rectangle(_barBoundingBox.X, _barBoundingBox.Y + _barBoundingBox.Height, _barBoundingBox.Width, 1), Color.White);

			_spriteBatch.End();

			base.Draw(gameTime);
		}

		public static bool IntersectPixels(
							Matrix transformA, int widthA, int heightA, Color[] dataA,
							Matrix transformB, int widthB, int heightB, Color[] dataB)
		{
			var transformAToB = transformA * Matrix.Invert(transformB);

			var stepX = Vector2.TransformNormal(Vector2.UnitX, transformAToB);
			var stepY = Vector2.TransformNormal(Vector2.UnitY, transformAToB);

			var yPosInB = Vector2.Transform(Vector2.Zero, transformAToB);

			for (var yA = 0; yA < heightA; yA++)
			{
				var posInB = yPosInB;

				for (var xA = 0; xA < widthA; xA++)
				{
					var xB = (int)Math.Round(posInB.X);
					var yB = (int)Math.Round(posInB.Y);

					if (0 <= xB && xB < widthB && 0 <= yB && yB < heightB)
					{
						var colorA = dataA[xA + yA * widthA];
						var colorB = dataB[xB + yB * widthB];

						if (colorA.A != 0 && colorB.A != 0)
						{
							return true;
						}
					}

					posInB += stepX;
				}

				yPosInB += stepY;
			}

			return false;
		}

		public static bool IntersectPixels_Slow(
			Matrix transformA, int widthA, int heightA, Color[] dataA,
			Matrix transformB, int widthB, int heightB, Color[] dataB)
		{
			var transformAToB = transformA * Matrix.Invert(transformB);

			for (var yA = 0; yA < heightA; yA++)
			{
				for (var xA = 0; xA < widthA; xA++)
				{
					var positionInB = Vector2.Transform(new Vector2(xA, yA), transformAToB);

					var xB = (int)Math.Round(positionInB.X);
					var yB = (int)Math.Round(positionInB.Y);

					if (0 <= xB && xB < widthB && 0 <= yB && yB < heightB)
					{
						var colorA = dataA[xA + yA * widthA];
						var colorB = dataB[xB + yB * widthB];

						if (colorA.A != 0 && colorB.A != 0)
						{
							return true;
						}
					}
				}
			}

			return false;
		}

		public static Rectangle CalculateBoundingRectangle(Rectangle rectangle, Matrix transform)
		{
			var leftTop = new Vector2(rectangle.Left, rectangle.Top);
			var rightTop = new Vector2(rectangle.Right, rectangle.Top);
			var leftBottom = new Vector2(rectangle.Left, rectangle.Bottom);
			var rightBottom = new Vector2(rectangle.Right, rectangle.Bottom);

			Vector2.Transform(ref leftTop, ref transform, out leftTop);
			Vector2.Transform(ref rightTop, ref transform, out rightTop);
			Vector2.Transform(ref leftBottom, ref transform, out leftBottom);
			Vector2.Transform(ref rightBottom, ref transform, out rightBottom);

			var min = Vector2.Min(Vector2.Min(leftTop, rightTop), Vector2.Min(leftBottom, rightBottom));
			var max = Vector2.Max(Vector2.Max(leftTop, rightTop), Vector2.Max(leftBottom, rightBottom));

			return new Rectangle((int)min.X, (int)min.Y, (int)(max.X - min.X), (int)(max.Y - min.Y));
		}
	}
}
