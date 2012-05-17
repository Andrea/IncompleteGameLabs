using System;

namespace Particles
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (ParticlesGame game = new ParticlesGame())
            {
                game.Run();
            }
        }
    }
#endif
}

