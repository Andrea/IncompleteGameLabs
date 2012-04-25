using Microsoft.Xna.Framework.Input;

namespace Particles
{
	public class InputState
	{
		private KeyboardState _currentKeyboardState;
		private KeyboardState _previousKeyboardState;

		public void Update()
		{
			_previousKeyboardState = _currentKeyboardState;
			_currentKeyboardState = Keyboard.GetState();
		}

		public bool IsKeyPressed(Keys key)
		{
			return _currentKeyboardState.IsKeyDown(key);
		}

		public bool IsNewKeyPress(Keys key)
		{
			return _currentKeyboardState.IsKeyDown(key) && _previousKeyboardState.IsKeyUp(key);
		}
	}
}
