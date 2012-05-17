using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WanderingBehaviours
{
    public class Entity : DrawableGameComponent
    {
        private IBehaviour _steeringBehaviour;
        private readonly SpriteBatch _spriteBatch;
        private Vehicle _vehicle;
        private Texture2D _texture;

        public Entity(Game game, SpriteBatch spriteBatch, Vehicle vehicle) : base(game)
        {
            _spriteBatch = spriteBatch;
            _vehicle = vehicle;
        }

        public Vector2 VehiclePosition
        {
            get {
                return _vehicle.Position;
            }
            set { _vehicle.Position = value; }
        }

        public override void Update(GameTime gameTime)
        {
            var steeringDirection = _steeringBehaviour.Update(_vehicle, gameTime);
            _vehicle.Update(gameTime, steeringDirection);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            _spriteBatch.Draw(_texture, _vehicle.Position, null, Color.White, _vehicle.Rotation, new Vector2(37, 32), 0.4f, SpriteEffects.None, 0);
        }

        public void SetSteeringBehaviour(IBehaviour behaviour)
        {
            _steeringBehaviour = behaviour;
        }

        protected override void LoadContent()
        {
            _texture = Game.Content.Load<Texture2D>("Arrow");

            base.LoadContent();
        }
    }
}