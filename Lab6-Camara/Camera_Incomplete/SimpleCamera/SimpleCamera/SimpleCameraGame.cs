using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SimpleCamera
{
	public class SimpleCameraGame : Game
	{
		private const float TankVelocity = 400;

		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		private Camera _camera1;
		private Camera _camera2;
		private Level _level;
		private Texture2D _tankTexture;
		private float _rotation;
	    private Vector2 _tankPosition;

	    public SimpleCameraGame()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}
		
		protected override void Initialize()
		{
			int halfScreenWidth = GraphicsDevice.Viewport.Width/2;
			_camera1 = new Camera(new Viewport(0, 0, halfScreenWidth - 3, GraphicsDevice.Viewport.Height));
			_camera2 = new Camera(new Viewport(halfScreenWidth, 0, halfScreenWidth - 3, GraphicsDevice.Viewport.Height));

			base.Initialize();
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			_level  = new Level();
			_level.LoadLevel(LevelData.Data, Content);

			_tankTexture = Content.Load<Texture2D>("tank");
		}

		protected override void UnloadContent()
		{
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				Exit();

			var keyboardState = Keyboard.GetState();

			var direction = new Vector2((float)(Math.Sin(_rotation)), (float)(-Math.Cos(_rotation)));

			if (keyboardState.IsKeyDown(Keys.W))
			{
                _tankPosition += direction * TankVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
			}
			else if (keyboardState.IsKeyDown(Keys.S))
			{
				_tankPosition += -direction * TankVelocity * (float) gameTime.ElapsedGameTime.TotalSeconds;
			}

			if (keyboardState.IsKeyDown(Keys.A))
				_rotation -= 0.04f;
			else if (keyboardState.IsKeyDown(Keys.D))
				_rotation += 0.04f;

			if (_rotation < 0)
				_rotation = MathHelper.TwoPi - _rotation;
			else if (_rotation > MathHelper.TwoPi)
				_rotation = _rotation - MathHelper.TwoPi;

		    /* 
			 * TODO: Question 1) Why the rotation is sent as - the rotation?
			 */
		    _camera1.Update(-_rotation, _tankPosition, 0.7f);
			_camera2.Update(-_rotation, _tankPosition, 1.7f);

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);
			/*
			 * TODO: 
			 * 1) Each camera has a viewport, set the GraphicsDevice viewport to one of the camera you want to draw
			 * 2) Replace the value of transformMatric to use the Transform in the _camera you set the viewPort to
			 * 3) Make the tank rotate as well
			 */

			Matrix transformMatrix = Matrix.Identity;

			_spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, null, transformMatrix);
			_level.Draw(_spriteBatch);
			_spriteBatch.Draw(_tankTexture, _tankPosition, null, Color.White, 0, new Vector2(_tankTexture.Width / 2, _tankTexture.Height / 2), 1, SpriteEffects.None, 0);
			_spriteBatch.End();

			/*
			 * TODO: now set the GraphicsDevice viewport to the other camera do the same to the transformMatric
			 */

            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, null, transformMatrix);
			_level.Draw(_spriteBatch);
			_spriteBatch.Draw(_tankTexture, _tankPosition, null, Color.White, 0, new Vector2(_tankTexture.Width / 2, _tankTexture.Height / 2), 1, SpriteEffects.None, 0);
			_spriteBatch.End();
			
			base.Draw(gameTime);
		}
	}
}
