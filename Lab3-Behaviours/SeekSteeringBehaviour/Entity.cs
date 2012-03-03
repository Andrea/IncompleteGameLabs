using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SeekSteeringBehaviour
{
	public class Entity : DrawableGameComponent
	{
		private ISteeringBehaviour _steeringBehaviour;
		private readonly SpriteBatch _spriteBatch;
		private readonly Vehicle _vehicle;
		private Texture2D _texture;


		public Entity(Game game, SpriteBatch spriteBatch, Vehicle vehicle)
			: base(game)
		{
			_spriteBatch = spriteBatch;
			_vehicle = vehicle;
		}

		public void SetSteeringBehaviour(ISteeringBehaviour steeringBehaviour)
		{
			_steeringBehaviour = steeringBehaviour;
		}

		protected override void LoadContent()
		{
			_texture = Game.Content.Load<Texture2D>("Arrow");

			base.LoadContent();
		}

		public override void Update(GameTime gameTime)
		{
			var steeringDirection = _steeringBehaviour.Update(_vehicle, gameTime);
			_vehicle.Update(steeringDirection, gameTime);

			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);
			_spriteBatch.Draw(_texture, _vehicle.Position, null, Color.White,  _vehicle.Rotation, new Vector2(37, 32), 0.4f, SpriteEffects.None, 0);
		}
	}
}