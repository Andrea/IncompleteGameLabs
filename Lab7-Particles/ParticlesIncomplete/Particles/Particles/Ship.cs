using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectMercury;
using ProjectMercury.Emitters;
using ProjectMercury.Renderers;

namespace Particles
{
	public class Ship : DrawableGameComponent
	{
		private const float Acceleration = 700;
		private const float GravityAcceleration = 400;
		private const float Damping = 0.99f;
		private const float MaxSpeed = 400;
		private const float RotationSpeed = 0.1f;

		private SpriteBatch _spriteBatch;
		private InputState _input;
		private Texture2D _texture;
		private ParticleEffect _thruster;
		private ParticleEffect _explosion;
		private ConeEmitter _thrusterEmitter;
		private Vector2 _velocity;
		private float _rotation;
		private Vector2 _origin;
		private SpriteBatchRenderer _particleRenderer;
		private Vector2 _thrusterAttachmentPoint;

		public Ship(Game game) : base(game)
		{
			
		}

		public Vector2 Position { get; set; }

		public override void Initialize()
		{
			base.Initialize();

			_spriteBatch = Game.Services.GetService<SpriteBatch>();
			_input = Game.Services.GetService<InputState>();
			_particleRenderer = Game.Services.GetService<SpriteBatchRenderer>();
		}

		protected override void LoadContent()
		{
			base.LoadContent();

			_texture = Game.Content.Load<Texture2D>("Ship");
			_origin = new Vector2(_texture.Width / 2, _texture.Height / 2);
			_thrusterAttachmentPoint = new Vector2(-_texture.Width / 2 + 4, -3);

			/* Todo_:			 
			 * Load the particle effect called "Thruster" and assign it to the _thruster field. 
			 * Call the InitializeEmitters method to initialize the emitters and particle textures. 
			 * Finally Do the same for the particle effect called "Explosion".
			 *  You ll need to create your own explosion particleEffect with a tool (inside tools) in the  code you downloaded
			 * 
			*/
			

			// Hint:
			// You can get access to the content manager through the Game.Content property.

			if(_thruster != null)
				_thrusterEmitter = (ConeEmitter) _thruster[0];
		}

		private void InitializeEmitters(ParticleEffect particleEffect)
		{
			// Todo:
			// Use a  loop to iterate over all of the emitters in the passed object called particleEffect. For each
			//  emitter, call Initialise on it and load the texture who's name is stored in the emitter.ParticleTextureAssetName
			//  property. Assign the texture to the emitter.ParticleTexture property.
		
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			UpdateInput(gameTime);
			UpdateVelocity(gameTime);
			
			Position += _velocity * (float) gameTime.ElapsedGameTime.TotalSeconds;

			ExplodeIfSunHit();
			UpdateThrusterEmitterDirection();
			TriggerThrusterParticles();
			UpdateParticles(gameTime);
		}

		private void UpdateParticles(GameTime gameTime)
		{
			// Todo:
			// Update the thruster and explosion particle effect.
			
		}

		private void TriggerThrusterParticles()
		{
			var rotationMatrix = Matrix.CreateRotationZ(_rotation);
			var thrusterAttachmentPoint = Vector2.Transform(_thrusterAttachmentPoint, rotationMatrix);

			// Todo:
			// Trigger the _thruster particle effect at position Position + thrusterAttachmentPoint.
			
			
		}

		private void UpdateThrusterEmitterDirection()
		{
			var thrusterDirection = _rotation - MathHelper.Pi;
			if (thrusterDirection < 0)
				thrusterDirection += MathHelper.TwoPi;
			
			if(_thrusterEmitter != null)
				_thrusterEmitter.Direction = thrusterDirection;
		}

		private void ExplodeIfSunHit()
		{
			var vectorToCenter = new Vector2(Game.GraphicsDevice.Viewport.Width / 2, Game.GraphicsDevice.Viewport.Height / 2) - Position;
			var distanceToCenter = vectorToCenter.Length();
			
			// Todo:
			// Trigger the _explosion particle effect when the distance between the ship and the center of the screen is less than 150 pixels.
			
		}

		private void UpdateVelocity(GameTime gameTime)
		{
			var vectorToCenter = new Vector2(Game.GraphicsDevice.Viewport.Width/2, Game.GraphicsDevice.Viewport.Height/2) - Position;
			vectorToCenter.Normalize();
			_velocity += vectorToCenter*GravityAcceleration*(float) gameTime.ElapsedGameTime.TotalSeconds;

			if (_velocity.Length() > 400)
			{
				_velocity.Normalize();
				_velocity *= MaxSpeed;
			}

			_velocity *= Damping;
		}

		private void UpdateInput(GameTime gameTime)
		{
			if (_input.IsKeyPressed(Keys.Up))
			{
				var vel = _velocity;
				vel.Normalize();

				var direction = new Vector2((float) Math.Cos(_rotation), (float) Math.Sin(_rotation));
				direction *= Acceleration;

				_velocity += direction*(float) gameTime.ElapsedGameTime.TotalSeconds;
			}

			if (_input.IsKeyPressed(Keys.Left))
				_rotation -= RotationSpeed;

			if (_input.IsKeyPressed(Keys.Right))
				_rotation += RotationSpeed;
		}

		public override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);

			_spriteBatch.Draw(_texture, Position, null, Color.White, _rotation, _origin, 1, SpriteEffects.None, 0);
			
			
			// Todo:
			// Use the SpriteBatchRenderer stored in the _particleRenderer field to render the _thruster and _explosion particle
			//  effects.

			
		}
	}
}