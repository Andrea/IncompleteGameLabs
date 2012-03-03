using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AnimatedSprites
{
	public class AnimatedSprite
	{
		private readonly int _framesAcross;
		private readonly int _framesDown;
		private readonly int _frameWidth;
		private readonly int _frameHeight;
		private readonly int _millisecondsPerFrame;
		private readonly Texture2D _texture;
		private int _currentFrameX;
		private int _currentFrameY;
		private int _timeSinceLastFrame;

		public AnimatedSprite(int framesAcross, int framesDown, int frameWidth, int frameHeight, int millisecondsPerFrame, Texture2D texture)
		{
			_framesAcross = framesAcross;
			_framesDown = framesDown;
			_frameWidth = frameWidth;
			_frameHeight = frameHeight;
			_millisecondsPerFrame = millisecondsPerFrame;
			_texture = texture;
		}

		public Vector2 Position { get; set; }

		public int FrameWidth
		{
			get { return _frameWidth; }
		}

		public int FrameHeight
		{
			get { return _frameHeight; }
		}

		public void Update(int elapsedTime)
		{
			_timeSinceLastFrame += elapsedTime;

			if (_timeSinceLastFrame > _millisecondsPerFrame)
			{
				_timeSinceLastFrame -= _millisecondsPerFrame;
				_currentFrameX += 1;
				if (_currentFrameX >= _framesAcross)
				{
					_currentFrameX = 0;
				}
			}
		}

		public void Reset()
		{
			_currentFrameX = 0;
		}

		public void SetAnimationRow(int row)
		{
			_currentFrameY = row;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			var destinationRectangle = new Rectangle(
				_currentFrameX * FrameWidth,
				_currentFrameY * FrameHeight,
				FrameWidth,
				FrameHeight);

			spriteBatch.Draw(
				_texture,
				Position,
				destinationRectangle,
				Color.White,
				0,
				Vector2.Zero,
				1,
				SpriteEffects.None,
				0);
		}
	}
}