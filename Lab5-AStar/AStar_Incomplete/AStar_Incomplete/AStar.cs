using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AStar_Incomplete
{
    public class AStar : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Map _map;
        private AStarPathFinder _aStar;
        private KeyboardState _previousKeyboardState;

        public AStar()
        {
            _graphics = new GraphicsDeviceManager(this) { PreferredBackBufferWidth = 1280, PreferredBackBufferHeight = 720 };
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(typeof(SpriteBatch), _spriteBatch);
         
            _map = new Map(7, 7, 90, this);
            _aStar = new AStarPathFinder(_map);
            Components.Add(_map);
            
            base.Initialize();
        }
        
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.P) && _previousKeyboardState.IsKeyDown(Keys.P) == false)
            {
                _aStar.CalculatePath();
            }

            if (keyboardState.IsKeyDown(Keys.R) && _previousKeyboardState.IsKeyDown(Keys.R) == false)
            {
                _map.Reset();
                _aStar = new AStarPathFinder(_map);
            }

            _previousKeyboardState = keyboardState;


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            base.Draw(gameTime);
            _spriteBatch.End();
        }
    }
}
