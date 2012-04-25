using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectMercury;
using ProjectMercury.Renderers;

namespace Particles
{
	public class ParticlesGame : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		private ParticleEffect _sunParticleEffect;
		private SpriteBatchRenderer _particleRenderer;
		private InputState _input;
		private Ship _ship;
		private Vector2 _screenCenter;

		public ParticlesGame()
		{
			_graphics = new GraphicsDeviceManager(this){PreferredBackBufferWidth = 1280, PreferredBackBufferHeight = 720};
			Content.RootDirectory = "Content";
		}

		protected override void Initialize()
		{
			_screenCenter = new Vector2(GraphicsDevice.PresentationParameters.BackBufferWidth / 2, GraphicsDevice.PresentationParameters.BackBufferHeight / 2);

			_particleRenderer = new SpriteBatchRenderer {GraphicsDeviceService = _graphics};
			_particleRenderer.LoadContent(Content);

			_spriteBatch = new SpriteBatch(GraphicsDevice);
			_input = new InputState();

			Services.AddService(typeof(SpriteBatch), _spriteBatch);
			Services.AddService(typeof(InputState), _input);
			Services.AddService(typeof(SpriteBatchRenderer), _particleRenderer);

			_ship = new Ship(this) {Position = new Vector2(900, 360)};
			Components.Add(_ship);
			IsMouseVisible = true;

			base.Initialize();
		}

		protected override void LoadContent()
		{
			/* Todo:
			 * 
			 * Load the particle effect called "Sun" and assign it to the _sunParticleEffect field. 
			 * Load the texture called "TriangleParticle" and assign it to the ParticleTexture property of the first emitter in the _sunParticleEffect instance.
			 * Initialize _sunParticleEffect
			 */

			
			// Hint:
			// In Mercury, you can access the emitters of a particle system using array notation, i.e.
			//  emitter[0], emitter[1] etc.
			// This is just because of the way Mercury implements the ParticleEffect class. Its not something you need to know right now, but
			//  it inherits from a collection class which overloads the [] operator.

		}

		protected override void UnloadContent()
		{
		}

		protected override void Update(GameTime gameTime)
		{
			_input.Update();
			
			base.Update(gameTime);
			
			/* Todo: 
			 * Trigger the _sunParticleEffect particle effect so that it emits particles from the center of the screen. 
			 * Then update the effect passing the elapsed time in seconds.
			 */

		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			/* Todo:
			 * Use the SpriteBatchRenderer stored in the _particleRenderer field to render the _subParticleEffect.
			 * Question: Why this doesnt work if calling from between _spriteBatch.Begin and _spriteBatch.End()
			*/
			
			_spriteBatch.Begin();
			base.Draw(gameTime);
			
			_spriteBatch.End();
		}
	}
}
