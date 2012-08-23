using System;

namespace Atomic
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Atom game = new Atom())
            {
                game.Run();
            }
        }
    }
#endif
}

