using System;

namespace CollisionDetection
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (CollisionDetectionLab game = new CollisionDetectionLab())
            {
                game.Run();
            }
        }
    }
#endif
}

