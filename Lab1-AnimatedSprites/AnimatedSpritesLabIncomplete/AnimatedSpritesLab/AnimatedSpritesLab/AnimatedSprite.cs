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
		private int _timeSinceLastFrameChanged;

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
            // Todo:
            // Complete this method so that it cycles between the frames in the current row of the animation. The animation frame should
            //  only change after _millisecondsPerFrame milliseconds have elapsed. When the current frame is greater than or equal to the 
            //  number of frames across, it should be reset to frame 0.

            // The elapsedTime parameter contains the number of milliseconds that have passed since the last time this method was called.
            //  In the fixed-time update system that we're using, this will always be 16 milliseconds (1000 / 16 = 60, or 60 frames per second).
            
            // Some fields that you should use:
            
            //  _timeSinceLastFrameChanged
            //  Use this to accumulate time so that you can tell if _millisecondsPerFrame milliseconds have passed. Remember not to lose time
            //   i.e. if your _millisecondsPerFrame value was 100, and your accumulated time since the last frame change was 105, the frame
            //   should be changed, and the value in _timeSinceLastFrameChanged should be set to 5.

            //  _currentFrameX
            //  Use this to store the current frame X index.

            //  _framesAcross
            //  This value has been set up to contain the number of frames across, so you can tell when to loop back to the beginning.

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
            // Todo:
            // Complete this method so that it draws the correct frame of the texture that has been loaded into the _texture field.
            //  Use the SpriteBatch.Draw method overload that accepts a sourceRectangle parameter to draw the correct frame.

		}
	}
}