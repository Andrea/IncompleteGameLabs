using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SimpleCamera
{
	public class Camera
	{
		
		public Matrix Transform { get; private set; }
		public Viewport Viewport { get; private set; }

		public Camera(Viewport viewport)
		{
			
			Transform = Matrix.Identity;
			Viewport = viewport;
		}

		public void Update(float rotation, Vector2 position, float zoom)
		{

			/* TODO: calculate Transform, to that effect you will need multiply 
			 *   - the translation matrix of the position by
			 *   - the Rotation matrix of the rotation by 
			 *   - the scale Matrix of the zoom by
			 *   - the translation matrix of the half the vieport's width and Height
			 * 
			 * 
			 * HINT use Matrix methods http://msdn.microsoft.com/en-us/library/microsoft.xna.framework.matrix_methods.aspx
			 * Question: why is Matrix a struct? (answer by google groups)
			 *  
			 */
			Transform = /* your code here*/  Matrix.Identity;

		}
	}
}