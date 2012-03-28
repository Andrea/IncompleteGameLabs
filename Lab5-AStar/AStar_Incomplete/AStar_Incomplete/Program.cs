using System;

namespace AStar_Incomplete
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (var game = new AStar())
            {
                game.Run();
            }
        }
    }
#endif
}

