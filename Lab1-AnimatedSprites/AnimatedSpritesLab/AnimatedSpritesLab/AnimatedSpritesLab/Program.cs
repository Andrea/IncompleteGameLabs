using System;

namespace AnimatedSprites
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (AnimatedSpritesLab game = new AnimatedSpritesLab())
            {
                game.Run();
            }
        }
    }
#endif
}

