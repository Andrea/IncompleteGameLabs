using System;

namespace SeekSteeringBehaviour
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (SeekAndFleeSteeringBehaviours game = new SeekAndFleeSteeringBehaviours())
            {
                game.Run();
            }
        }
    }
#endif
}

