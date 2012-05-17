using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WanderingBehaviours
{
    public class Wandering : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;

        public Wandering()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            var random = new Random((int)DateTime.Now.Ticks);

            for (var i = 0; i < 50; i++)
            {
                var entity = new Entity(this, _spriteBatch, new Vehicle
                (
                    new Vector2(random.Next(GraphicsDevice.Viewport.Width), random.Next(GraphicsDevice.Viewport.Height)),
                    new Vector2(100), 
                     1f,
                     350,
                    750
                ));
                entity.SetSteeringBehaviour(new WanderBehaviour(random));
                Components.Add(entity);
            }

            base.Initialize();
        }


        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (var component in Components)
            {
                var entity = component as Entity;
                if (entity == null)
                    continue;

                if (entity.VehiclePosition.X < 0)
                    entity.VehiclePosition = new Vector2(GraphicsDevice.Viewport.Width, entity.VehiclePosition.Y);
                else if (entity.VehiclePosition.X > GraphicsDevice.Viewport.Width)
                    entity.VehiclePosition = new Vector2(0, entity.VehiclePosition.Y);

                if (entity.VehiclePosition.Y < 0)
                    entity.VehiclePosition = new Vector2(entity.VehiclePosition.X, GraphicsDevice.Viewport.Height);
                else if (entity.VehiclePosition.Y > GraphicsDevice.Viewport.Height)
                    entity.VehiclePosition = new Vector2(entity.VehiclePosition.X, 0);
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
