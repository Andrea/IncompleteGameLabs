using System;

namespace SimpleCamera
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (SimpleCameraGame game = new SimpleCameraGame())
            {
                game.Run();
            }
        }
    }
#endif
}

