using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SeekSteeringBehaviour
{
	public class SeekAndFleeSteeringBehaviours : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		private SeekBehaviour _seekBehaviour;
		private FleeBehaviour _fleeBehaviour;
		private SteeringTarget _steeringTarget;
		private ISteeringBehaviour _currentSteeringBehaviour;
		private KeyboardState _previousKeyboardState;

		public SeekAndFleeSteeringBehaviours()
		{
			_graphics = new GraphicsDeviceManager(this){PreferredBackBufferWidth = 1280, PreferredBackBufferHeight = 720};
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		protected override void Initialize()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);
			var random = new Random((int)DateTime.Now.Ticks);
			_steeringTarget = new SteeringTarget(Vector2.Zero);
			_seekBehaviour = new SeekBehaviour(_steeringTarget);
			_fleeBehaviour = new FleeBehaviour(_steeringTarget);
			_currentSteeringBehaviour = _seekBehaviour;

			for (var i = 0; i < 3000; i++)
			{
				var entity = new Entity(this, _spriteBatch, new Vehicle
				{
					Position = new Vector2(random.Next(GraphicsDevice.Viewport.Width), random.Next(GraphicsDevice.Viewport.Height)),
					
					// Todo if you like:
					// Tweak these values to see how they affect the steering behaviours.

					MaxSpeed = 500,
					MaxForce = 350,
					Mass = 1f
				});
				entity.SetSteeringBehaviour(_seekBehaviour);
				Components.Add(entity);
			}

			base.Initialize();
		}

		protected override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			var mouseState = Mouse.GetState();

			_steeringTarget.Position = new Vector2(mouseState.X, mouseState.Y);
			var keyboardState = Keyboard.GetState();
			if(keyboardState.IsKeyDown(Keys.Space) && _previousKeyboardState.IsKeyDown(Keys.Space) == false)
			{
				ChangeSteeringBehaviour();
			}

			_previousKeyboardState = keyboardState;
		}

		private void ChangeSteeringBehaviour()
		{
			if (_currentSteeringBehaviour == _seekBehaviour)
				_currentSteeringBehaviour = _fleeBehaviour;
			else
				_currentSteeringBehaviour = _seekBehaviour;

			foreach (var component in Components)
			{
				var entity = component as Entity;
				if (entity == null)
					continue;

				entity.SetSteeringBehaviour(_currentSteeringBehaviour);
			}
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			_spriteBatch.Begin();
			base.Draw(gameTime);
			_spriteBatch.End();
		}
	}
}
